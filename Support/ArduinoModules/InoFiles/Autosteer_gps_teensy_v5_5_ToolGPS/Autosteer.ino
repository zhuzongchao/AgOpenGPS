/*
   UDP Autosteer code for Teensy 4.1
   For AgOpenGPS
   01 Feb 2022
   Like all Arduino code - copied from somewhere else :)
   So don't claim it as your own
*/

float u_p;
float u_i;
////////////////// User Settings /////////////////////////

// if not in eeprom, overwrite
#define EEP_Ident 5100

//--------------------------- Switch Input Pins ------------------------
#define STEERSW_PIN 6

//Define sensor pin for current or pressure sensor
#define ANALOG_SENSOR_PIN A0

#define CONST_180_DIVIDED_BY_PI 57.2957795130823

#include <EEPROM.h>
#include <IPAddress.h>

// ethernet
#include <NativeEthernet.h>
#include <NativeEthernetUdp.h>

uint8_t autoSteerUdpData[500];  // Buffer For Receiving UDP Data

//loop time variables in microseconds
const uint16_t LOOP_TIME = 25;  //40Hz
uint32_t autsteerLastTime = LOOP_TIME;
uint32_t currentTime = LOOP_TIME;

const uint16_t WATCHDOG_THRESHOLD = 100;
const uint16_t WATCHDOG_FORCE_VALUE = WATCHDOG_THRESHOLD + 2; // Should be greater than WATCHDOG_THRESHOLD
uint8_t watchdogTimer = WATCHDOG_FORCE_VALUE;

//show life in AgIO
uint8_t helloAgIO[] = {0x80, 0x81, 0x7f, 0xC7, 1, 0, 0x47 };
uint8_t helloCounter = 0;

uint8_t AOG1[] = {0x80, 0x81, 0x7f, 0xE6, 8, 0, 0, 0, 0, 0, 0, 0, 0, 0xCC };
int16_t AOG1Size = sizeof(AOG1);

//EEPROM
int16_t EEread = 0;

//Switches
uint8_t steerSwitch = 1;

uint8_t guidanceStatus = 0;
float toolCrossTrackError = 0; //the tool cross track error from current line
float vehicleCrossTrackError = 0; // the error of vehicle from current line

//speed sent as *10
float gpsSpeed = 0;

//steering variables
float steerAngleActual = 0;
float steerAngleSetPoint = 0; //the desired angle from AgOpen
int16_t steeringPosition = 0; //from steering sensor
float steerAngleError = 0; //setpoint - actual
float xteActual = 0;

//pwm variables
int16_t pwmDrive = 0, pwmDisplay = 0;
float pValue = 0;

//Steer switch button  ***********************************************************************************************************
uint8_t currentState = 1, reading, previous = 0;

//Variables for settings
struct Storage {
  uint8_t Kp = 40;  //propGainortional gain
  uint8_t lowPWM = 10;  //band of no action
  int16_t wasOffset = 0;
  uint8_t minPWM = 9;
  uint8_t highPWM = 60;//max PWM value
  float steerSensorCounts = 30;
  float AckermanFix = 1;     //sent as percent
  uint8_t KpXTE = 40;
  uint8_t KiXTE = 0;
  uint8_t maxSteer = 50;
  uint8_t maxIntegral = 20;
};
Storage steerSettings;  //12 bytes

//Variables for settings - 0 is false
struct Setup {
  uint8_t InvertWAS = 0;
  uint8_t IsRelayActiveHigh = 0; //if zero, active low (default)
  uint8_t MotorDriveDirection = 0;
  uint8_t SingleInputWAS = 1;
  uint8_t CytronDriver = 1;
  uint8_t SteerSwitch = 0;  //1 if switch selected
  uint8_t SteerButton = 0;  //1 if button selected
  uint8_t IsDanfoss = 0;
};
Setup steerConfig;          //9 bytes

//reset function
void(* resetFunc) (void) = 0;

void autosteerSetup()
{
  Serial.println("-- AutoSteer Setup");

  //keep pulled high and drag low to activate, noise free safe
  pinMode(STEERSW_PIN, INPUT_PULLUP);

  EEPROM.get(0, EEread);              // read identifier

  if (EEread != EEP_Ident)   // check on first start and write EEPROM
  {
    EEPROM.put(0, EEP_Ident);
    EEPROM.put(10, steerSettings);
    EEPROM.put(40, steerConfig);
  }
  else
  {
    EEPROM.get(10, steerSettings);     // read the Settings
    EEPROM.get(40, steerConfig);
  }

  Serial.println("Setup complete, waiting for AgOpenGPS");

}// End of Setup

void autosteerLoop()
{
  //Serial.println("AutoSteer loop");
  // Loop triggers every 100 msec and sends back gyro heading, and roll, steer angle etc
  currentTime = systick_millis_count;

  ReceiveUdp();

  //CANBus
  if (steeringValveReady == 20 || steeringValveReady == 16)
  {
    digitalWrite(valveReadyLED, HIGH);
  }
  else
  {
    digitalWrite(valveReadyLED, LOW);
  }

  if (currentTime - autsteerLastTime >= LOOP_TIME)
  {
    autsteerLastTime = currentTime;

    //If connection lost to AgOpenGPS, the watchdog will count up and turn off steering
    if (watchdogTimer++ > 250) watchdogTimer = WATCHDOG_FORCE_VALUE;

    if (steerConfig.SteerSwitch == 1)         //steer switch on - off
    {
      steerSwitch = !digitalRead(STEERSW_PIN); //read auto steer enable switch open = 0n closed = Off
    }
    else if (steerConfig.SteerButton == 1)    //steer Button momentary
    {
      reading = digitalRead(STEERSW_PIN);
      if (reading == LOW && previous == HIGH)
      {
        if (currentState == 1)
        {
          currentState = 0;
          steerSwitch = 0;
        }
        else
        {
          currentState = 1;
          steerSwitch = 1;
        }
      }
      previous = reading;
    }
    else                                      // No steer switch and no steer button, use GUI button data
    {
      if (guidanceStatus == 1) steerSwitch = 1;
      if (guidanceStatus == 0) steerSwitch = 0;
    }
    //Serial.println(steerSwitch);

    steeringPosition = adcTeensy.read(wasInput);// >> 1;
    //    Serial.print(steeringPosition);
    //    Serial.print("\t");
    //    Serial.print(steerSettings.wasOffset);
    //    Serial.print("\t");
    //    Serial.print(steerSettings.steerSensorCounts);
    //    Serial.print("\t");
    //DETERMINE ACTUAL STEERING POSITION

    //convert position to steer angle. 4,5 V = 7410, 0,5 V = 754, Range 6656, Middle = 6656/2 + 754 = 4082
    //  ***** make sure that negative steer angle makes a left turn and positive value is a right turn *****
    if (steerConfig.InvertWAS)
    {
      steeringPosition = (steeringPosition - 4082  + steerSettings.wasOffset);   // 1/2 of full scale
      steerAngleActual = (float)(steeringPosition) / -steerSettings.steerSensorCounts;
    }
    else
    {
      steeringPosition = (steeringPosition - 4082  + steerSettings.wasOffset);   // 1/2 of full scale
      steerAngleActual = (float)(steeringPosition) / steerSettings.steerSensorCounts;
    }
    //    Serial.print(steeringPosition);
    //    Serial.print("\t");
    //    Serial.print(steerAngleActual);
    //    Serial.print("\t");
    //Serial.println(steeringPosition);
    if (watchdogTimer < WATCHDOG_THRESHOLD)
    {

      //steerAngleActual = 0; //sideshift specific

      u_p = xteActual * steerSettings.KpXTE * 0.002;
      if (steerSwitch && gpsSpeed > 0.1) //only integrate if moving and tool is used (steerSwitch)
      {
        u_i = u_i + xteActual * steerSettings.KiXTE * 0.00001;
      }

      if (u_i > steerSettings.maxIntegral) u_i = steerSettings.maxIntegral;
      if (u_i < -steerSettings.maxIntegral) u_i = -steerSettings.maxIntegral;

      steerAngleError = steerAngleActual - u_p - u_i;

      if (steerAngleActual > (steerSettings.maxSteer * 0.1) && steerAngleError < 0) steerAngleError = 0;
      if (steerAngleActual < -(steerSettings.maxSteer * 0.1)  && steerAngleError > 0) steerAngleError = 0;

      //                  Serial.print(" xteActual: ");
      //                  Serial.print(xteActual);
      //                  Serial.print(" Prop: ");
      //                  Serial.print(u_p);
      //                  Serial.print(" integralSum: ");
      //                  Serial.print(u_i);
      //                  Serial.print(" ActualSteerAngle: ");
      //                  Serial.print(steerAngleActual);
      //                  Serial.print(" steerAngleError: ");
      //                  Serial.print(steerAngleError);

      calcSteeringPID();  //do the pid
      //CAN specific
      intendToSteer = 1; //CAN Curve Inteeded for Steeringt
      //CAN specific END
      motorDrive();       //out to motors the pwm value
    }
    else
    {
      //we've lost the comm to AgOpenGPS, or just stop request

      pwmDrive = 0; //turn off steering motor
      pwmDisplay = 0;
      //CAN specific
      intendToSteer = 0; //CAN Curve Inteeded for Steeringt
      //VBus_Send();
      //CAN specific END
      motorDrive(); //out to motors the pwm value
    }


    //send empty pgn to AgIO to show activity
    if (++helloCounter > 10)
    {
      SendUdp(helloAgIO, sizeof(helloAgIO), ipDestination, portDestination);
      helloCounter = 0;
    }
    //Serial.println("");
  } //end of timed loop

} // end of main autosteer loop

// UDP Receive
void ReceiveUdp()
{
  // When ethernet is not running, return directly. parsePacket() will block when we don't
  if (!Ethernet_running)
  {
    return;
  }

  uint16_t len = Eth_udpAutoSteer.parsePacket();

  if (len)
  {
    Eth_udpAutoSteer.read(autoSteerUdpData, 500);

    if (updatemode)
    {
      // read and process intel hex lines until EOF or error
      if (process_hex_record( autoSteerUdpData, len ))
      {
        // return error or user abort, so clean up and
        // reboot to ensure that static vars get boot-up initialized before retry
        Serial.println();
        Serial.printf( "erase FLASH buffer / free RAM buffer...\n" );
        delay(5000);
        firmware_buffer_free( buffer_addr, buffer_size );
        REBOOT;
      }
    }
    else if (len > 13)
    {
      if (autoSteerUdpData[0] == 0x80 && autoSteerUdpData[1] == 0x81 && autoSteerUdpData[2] == 0x7F) //Data
      {
        if (autoSteerUdpData[3] == 0xFE)  //254
        {
          //gpsSpeed = ((float)(autoSteerUdpData[5] | autoSteerUdpData[6] << 8)) * 0.1;
        }
        else if (autoSteerUdpData[3] == 0xE7)  //231 FB - Tool Steer Board Config
        {
          Serial.println("Got Implement Board Config");
          uint8_t sett = autoSteerUdpData[5]; //setting0

          if (bitRead(sett, 0)) steerConfig.InvertWAS = 1; else steerConfig.InvertWAS = 0;
          if (bitRead(sett, 1)) steerConfig.IsRelayActiveHigh = 1; else steerConfig.IsRelayActiveHigh = 0;
          if (bitRead(sett, 2)) steerConfig.MotorDriveDirection = 1; else steerConfig.MotorDriveDirection = 0;
          if (bitRead(sett, 3)) steerConfig.SingleInputWAS = 1; else steerConfig.SingleInputWAS = 0;
          if (bitRead(sett, 4)) steerConfig.CytronDriver = 1; else steerConfig.CytronDriver = 0;
          if (bitRead(sett, 5)) steerConfig.SteerSwitch = 1; else steerConfig.SteerSwitch = 0;
          if (bitRead(sett, 6)) steerConfig.SteerButton = 1; else steerConfig.SteerButton = 0;
          if (bitRead(sett, 7)) steerConfig.IsDanfoss = 1; else steerConfig.IsDanfoss = 0;

          //crc
          //autoSteerUdpData[13];

          EEPROM.put(40, steerConfig);

          //reset the arduino
          //resetFunc(); //Why reset?
        }
        else if (autoSteerUdpData[3] == 0xE8)  //232 Implement Settings
        {
          Serial.println("Got Implement Settings");
          //PID values
          steerSettings.KpXTE = ((float)autoSteerUdpData[5]); // read Kp from AgOpenGPS
          steerSettings.KiXTE = ((float)autoSteerUdpData[6]);   // read Kp from AgOpenGPS
          steerSettings.minPWM = autoSteerUdpData[7]; //read the minimum amount of PWM for instant on
          steerSettings.highPWM = autoSteerUdpData[8]; // read high pwm
          steerSettings.maxIntegral = autoSteerUdpData[9];
          steerSettings.steerSensorCounts = autoSteerUdpData[10]; //sent as setting displayed in AOG
          steerSettings.wasOffset = autoSteerUdpData[11];
          steerSettings.wasOffset |= (autoSteerUdpData[12] << 8);
          steerSettings.maxSteer = autoSteerUdpData[13];
          steerSettings.Kp = ((float)autoSteerUdpData[14]); // read Kp from AgOpenGPS

          //crc
          //autoSteerUdpData[13];

          //store in EEPROM
          EEPROM.put(10, steerSettings);

        }
        else if (autoSteerUdpData[3] == 0xE9)  //233 ImplementSteerData
        {
          //Serial.println("Got Implement SteerData");
          //Bit 5,6
          xteActual = ((float)(autoSteerUdpData[5] | ((int8_t)autoSteerUdpData[6]) << 8)); //high low bytes

          guidanceStatus = autoSteerUdpData[7];

          if (guidanceStatus == 0)
          {
            watchdogTimer = WATCHDOG_FORCE_VALUE; //turn off steering motor
            u_i = 0;
          }
          else          //valid conditions to turn on autosteer
          {
            watchdogTimer = 0;  //reset watchdog
          }

          //Bit 8,9    set point steer angle * 100 is sent
          vehicleCrossTrackError = ((float)(autoSteerUdpData[8] | autoSteerUdpData[9] << 8)); //high low bytes

          gpsSpeed = autoSteerUdpData[10] * 0.1;

          //----------------------------------------------------------------------------
          //Serial Send to agopenGPS

          int16_t sa = (int16_t)(steerAngleActual * 10);

          AOG1[5] = (uint8_t)sa;
          AOG1[6] = sa >> 8;

          // SteerAngle
          sa = (int16_t)(steerAngleError * 10);
          AOG1[7] = (uint8_t)sa;
          AOG1[8] = sa >> 8;

          // PWM
          AOG1[9] = (uint8_t)abs(pwmDisplay);

          //AOG1[10] = (uint8_t)guidanceStatus;
          AOG1[10] = (uint8_t)steerSwitch;

          //checksum
          int16_t CK_A = 0;
          for (uint8_t i = 2; i < AOG1Size - 1; i++)
            CK_A = (CK_A + AOG1[i]);

          AOG1[AOG1Size - 1] = CK_A;

          //off to AOG
          SendUdp(AOG1, sizeof(AOG1), ipDestination, portDestination);

          // Stop sending the helloAgIO message
          helloCounter = 0;

        }
        //end FB
      } //end if 80 81 7F
    }
    else if (len == 9)
    {
      bool startOTA = false;
      for (byte idx = 0; idx < len && idx < 9;)
      {
        if (autoSteerUdpData[idx] == OTAUpdate[idx])
        {
          startOTA = true;
          idx += 1;
        }
        else
        {
          startOTA = false;
          break;
        }
      }

      if (startOTA)
      {
        if (firmware_buffer_init( &buffer_addr, &buffer_size ) == 0)
        {
          Serial.println();
          Serial.printf( "unable to create buffer\n" );
          Serial.flush();
          for (;;) {}
        }

        Serial.printf( "target = %s (%dK flash in %dK sectors)\n", FLASH_ID, FLASH_SIZE / 1024, FLASH_SECTOR_SIZE / 1024);
        Serial.printf( "buffer = %1luK %s (%08lX - %08lX)\n", buffer_size / 1024, IN_FLASH(buffer_addr) ? "FLASH" : "RAM", buffer_addr, buffer_addr + buffer_size );
        Serial.println( "waiting for hex lines...\n" );
        updatemode = true;

        //SendUdp(failOTA, sizeof(failOTA), ipDestination, portDestination);

        //delay(5000);
        //firmware_buffer_free( FLASH_BASE_ADDR, FLASH_SIZE - FLASH_RESERVE);
        //REBOOT;
      }
    }
  }
}

void SENDCheckUdp()
{
  byte checkOTA[9] = {0x4f, 0x54, 0x41, 0x55, 0x70, hex.lines & 255, hex.lines >> 8 & 255, hex.lines >> 16 & 255, hex.lines >> 24 & 255};
  SendUdp(checkOTA, sizeof(checkOTA), ipDestination, portDestination);
}


void SendUdp(uint8_t *data, uint8_t datalen, IPAddress dip, uint16_t dport)
{
  Eth_udpAutoSteer.beginPacket(dip, dport);
  Eth_udpAutoSteer.write(data, datalen);
  Eth_udpAutoSteer.endPacket();
}
