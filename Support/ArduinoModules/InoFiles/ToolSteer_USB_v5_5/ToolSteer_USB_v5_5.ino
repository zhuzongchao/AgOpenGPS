  /*
  * USB Autosteer code For AgOpenGPS
  * 4 Feb 2021, Brian Tischler
  * Like all Arduino code - copied from somewhere else :)
  * So don't claim it as your own
  */
  
////////////////// User Settings /////////////////////////  

  //How many cm before decreasing Max PWM
  #define LOW_HIGH_DEGREES 5.0

  /*  PWM Frequency -> 
   *   490hz (default) = 0
   *   122hz = 1
   *   3921hz = 2
   */
  #define PWM_Frequency 0
  
/////////////////////////////////////////////

  // if not in eeprom, overwrite 
  #define EEP_Ident 5102 

  // BNO08x definitions
  #define REPORT_INTERVAL 90 //Report interval in ms (same as the delay at the bottom)

  //   ***********  Motor drive connections  **************888
  //Connect ground only for cytron, Connect Ground and +5v for IBT2
    
  //Dir1 for Cytron Dir, Both L and R enable for IBT2
  #define DIR1_RL_ENABLE  4  //PD4

  //PWM1 for Cytron PWM, Left PWM for IBT2
  #define PWM1_LPWM  3  //PD3

  //Not Connected for Cytron, Right PWM for IBT2
  #define PWM2_RPWM  9 //D9

  //--------------------------- Switch Input Pins ------------------------
  #define STEERSW_PIN 6 //PD6
  #define WORKSW_PIN 7  //PD7
  #define REMOTE_PIN 8  //PB0

  //Define sensor pin for current or pressure sensor
  #define ANALOG_SENSOR_PIN A0
  
  #define CONST_180_DIVIDED_BY_PI 57.2957795130823

  #include <Wire.h>
  #include <EEPROM.h> 
  #include "zADS1115.h"
  ADS1115_lite adc(ADS1115_DEFAULT_ADDRESS);     // Use this for the 16-bit version ADS1115
 
  //loop time variables in microseconds  
  const uint16_t LOOP_TIME = 20;  //50Hz    
  uint32_t lastTime = LOOP_TIME;
  uint32_t currentTime = LOOP_TIME;
  
  const uint16_t WATCHDOG_THRESHOLD = 100;
  const uint16_t WATCHDOG_FORCE_VALUE = WATCHDOG_THRESHOLD + 2; // Should be greater than WATCHDOG_THRESHOLD
  uint8_t watchdogTimer = WATCHDOG_FORCE_VALUE;
  
   //Parsing PGN
  bool isPGNFound = false, isHeaderFound = false;
  uint8_t pgn = 0, dataLength = 0, idx = 0;
  int16_t tempHeader = 0;

  //show life in AgIO
  uint8_t helloAgIO[] = {0x80,0x81, 0x7f, 0xC7, 1, 0, 0x47 };
  uint8_t helloCounter=0;

  bool invertWAS = false;

  //fromAutoSteerData FD 253 - ActualSteerAngle*100 -5,6, Heading-7,8, 
        //Roll-9,10, SwitchByte-11, pwmDisplay-12, CRC 13
  uint8_t PGN_232[] = {0x80,0x81, 0x7f, 232, 8, 0, 0, 0, 0, 0,0,0,0, 0xCC };
  int8_t PGN_232_Size = sizeof(PGN_232) - 1;
  
  //EEPROM
  int16_t EEread = 0;
 
  //On Off
  uint8_t guidanceStatus = 0;

  //steering variables
  float steerPositionActual = 0;
  float toolCrossTrackError = 0; //the tool cross track error from current line
  float vehicleCrossTrackError = 0; // the error of vehicle from current line
  int16_t steeringPositionSensor = 0; //from steering sensor
  float steerPositionError = 0; //setpoint - actual
  
  //pwm variables
  int16_t pwmDrive = 0, pwmDisplay = 0;
  float pValue = 0;
  float errorAbs = 0;
  float highLowPerDeg = 0; 
 
   //Variables for settings  
  struct Storage {
      uint8_t Kp = 40;  //proportional gain
      uint8_t Ki = 0;  //integral gain
      uint8_t minPWM = 9;
      uint8_t lowPWM = 10;  //band of no action
      uint8_t highPWM = 120;//max PWM value
      float steerSensorCounts = 10;
      int16_t wasOffset = 0;
  };  Storage steerSettings;  //14 bytes

  //reset function
  void(* resetFunc) (void) = 0;

  void setup()
  { 
    //PWM rate settings. Set them both the same!!!!
  
    if (PWM_Frequency == 1) 
    {
      TCCR2B = TCCR2B & (B11111000 | B00000110);    // set timer 2 to 256 for PWM frequency of   122.55 Hz
      TCCR1B = TCCR1B & (B11111000 | B00000100);    // set timer 1 to 256 for PWM frequency of   122.55 Hz
    }  
    else if (PWM_Frequency == 2)
    {
      TCCR1B = TCCR1B & (B11111000 | B00000010);    // set timer 1 to 8 for PWM frequency of  3921.16 Hz
      TCCR2B = TCCR2B & (B11111000 | B00000010);    // set timer 2 to 8 for PWM frequency of  3921.16 Hz  
    }
    
    //keep pulled high and drag low to activate, noise free safe   
    pinMode(DIR1_RL_ENABLE, OUTPUT);    
    pinMode(PWM2_RPWM, OUTPUT); 
    
    //set up communication
    Wire.begin();
    Serial.begin(38400);  
    
    EEPROM.get(0, EEread);              // read identifier
      
    if (EEread != EEP_Ident)   // check on first start and write EEPROM
    {           
      EEPROM.put(0, EEP_Ident);
      EEPROM.put(10, steerSettings);   
    }
    else 
    { 
      EEPROM.get(10, steerSettings);     // read the Settings
    }
    
    // for PWM High to Low interpolator
    highLowPerDeg = ((float)(steerSettings.highPWM - steerSettings.lowPWM)) / LOW_HIGH_DEGREES;

    adc.setSampleRate(ADS1115_REG_CONFIG_DR_128SPS); //128 samples per second
    adc.setGain(ADS1115_REG_CONFIG_PGA_6_144V);

  }// End of Setup

  void loop()
  {
      // Loop triggers every 100 msec and sends back steer angle etc   
      currentTime = millis();

      if (currentTime - lastTime >= LOOP_TIME)
      {
          lastTime = currentTime;

          //If connection lost to AgOpenGPS, the watchdog will count up and turn off steering
          if (watchdogTimer++ > 250) watchdogTimer = WATCHDOG_FORCE_VALUE;


          adc.setMux(ADS1115_REG_CONFIG_MUX_SINGLE_0);
          steeringPositionSensor = adc.getConversion();
          adc.triggerConversion();//ADS1115 Single Mode 

          steeringPositionSensor = (steeringPositionSensor >> 1); //bit shift by 2  0 to 13610 is 0 to 5v

       //DETERMINE ACTUAL STEERING POSITION

         //convert position to steer angle. 32 counts per degree of steer pot position in my case
         //  ***** make sure that negative steer angle makes a left turn and positive value is a right turn *****
          if (invertWAS)
          {
              steeringPositionSensor = (steeringPositionSensor - 6805 - steerSettings.wasOffset);   // 1/2 of full scale
              steerPositionActual = (float)(steeringPositionSensor) / -steerSettings.steerSensorCounts;
          }
          else
          {
              steeringPositionSensor = (steeringPositionSensor - 6805 + steerSettings.wasOffset);   // 1/2 of full scale
              steerPositionActual = (float)(steeringPositionSensor) / steerSettings.steerSensorCounts;
          }

          if (watchdogTimer < WATCHDOG_THRESHOLD)
          {

              steerPositionError = steerPositionActual - toolCrossTrackError;   //calculate the steering error
              //if (abs(steerAngleError)< steerSettings.lowPWM) steerAngleError = 0;

              calcSteeringPID();  //do the pid
              motorDrive();       //out to motors the pwm value
          }
          else
          {
              //we've lost the comm to AgOpenGPS, or just stop request

              pwmDrive = 0; //turn off steering motor
              motorDrive(); //out to motors the pwm value
          }

          //send empty pgn to AgIO to show activity
          if (++helloCounter > 10)
          {
              Serial.write(helloAgIO, sizeof(helloAgIO));
              helloCounter = 0;
          }
      } //end of timed loop

      //This runs continuously, not timed //// Serial Receive Data/Settings /////////////////

      // Serial Receive
      //Do we have a match with 0x8081?    
      if (Serial.available() > 1 && !isHeaderFound && !isPGNFound)
      {
          uint8_t temp = Serial.read();
          if (tempHeader == 0x80 && temp == 0x81)
          {
              isHeaderFound = true;
              tempHeader = 0;
          }
          else
          {
              tempHeader = temp;     //save for next time
              return;
          }
      }

      //Find Source, PGN, and Length
      if (Serial.available() > 2 && isHeaderFound && !isPGNFound)
      {
          Serial.read(); //The 7F or less
          pgn = Serial.read();
          dataLength = Serial.read();
          isPGNFound = true;
          idx = 0;
      }

      //The data package
      if (Serial.available() > dataLength && isHeaderFound && isPGNFound)
      {
          if (pgn == 235) //FE AutoSteerData
          {
              //bit 5,6
              toolCrossTrackError = ((float)(Serial.read() | Serial.read() << 8));
              toolCrossTrackError *= 0.1;


              //bit 7
              guidanceStatus = Serial.read();

              if (guidanceStatus == 0)
              {
                  watchdogTimer = WATCHDOG_FORCE_VALUE; //turn off steering motor
              }
              else          //valid conditions to turn on autosteer
              {
                  watchdogTimer = 0;  //reset watchdog
              }

              //Bit 8,9    set point steer angle * 100 is sent
              vehicleCrossTrackError = ((float)(Serial.read() | Serial.read() << 8)); //high low bytes

              //Bit 10  
              Serial.read();

              //Bit 11
              Serial.read();

              //Bit 12 
              Serial.read();

              //Bit 13 CRC
              Serial.read();

              //reset for next pgn sentence
              isHeaderFound = isPGNFound = false;
              pgn = dataLength = 0;

              //----------------------------------------------------------------------------
              //Serial Send to agopenGPS
              // Steer Data to AOG
              int16_t sa = (int16_t)(steerPositionActual*10);
              PGN_232[5] = (uint8_t)sa;
              PGN_232[6] = sa >> 8;

              sa = (int16_t)(steerPositionError*10);
              PGN_232[7] = (uint8_t)sa;
              PGN_232[8] = sa >> 8;


              //PGN_232[11] = switchByte;
              PGN_232[9] = (uint8_t)pwmDisplay;

              //add the checksum for AOG
              int16_t CK_A = 0;
              for (uint8_t i = 2; i < PGN_232_Size; i++)
              {
                  CK_A = (CK_A + PGN_232[i]);
              }

              PGN_232[PGN_232_Size] = CK_A;

              //send to AOG
              Serial.write(PGN_232, sizeof(PGN_232));

              // Stop sending the helloAgIO message
              if (helloCounter) helloCounter = 0;
              //--------------------------------------------------------------------------              
          }

          else if (pgn == 233) //FC AutoSteerSettings
          {
              //PID values
              steerSettings.Kp = Serial.read();   // read Kp from AgOpenGPS

              steerSettings.Ki = Serial.read();

              steerSettings.minPWM = Serial.read(); //read the minimum amount of PWM for instant on

              steerSettings.lowPWM = Serial.read();   // read lowPWM from AgOpenGPS
              
              steerSettings.highPWM = Serial.read();   // read highPWM from AgOpenGPS

              steerSettings.steerSensorCounts = Serial.read(); //sent as setting displayed in AOG

              steerSettings.wasOffset = (Serial.read() - 127) * 10;  //read was zero offset Hi

              Serial.read();

              //crc
              //udpData[13];        //crc
              Serial.read();

              //store in EEPROM
              EEPROM.put(10, steerSettings);

              // for PWM High to Low interpolator
              highLowPerDeg = ((float)(steerSettings.highPWM - steerSettings.lowPWM)) / LOW_HIGH_DEGREES;

              //reset for next pgn sentence
              isHeaderFound = isPGNFound = false;
              pgn = dataLength = 0;
          }


          //clean up strange pgns
          else
          {
              //reset for next pgn sentence
              isHeaderFound = isPGNFound = false;
              pgn = dataLength = 0;
          }

      }
  }//end if (Serial.available() > dataLength && isHeaderFound && isPGNFound)      
  

  //TCCR2B = TCCR2B & B11111000 | B00000001;    // set timer 2 divisor to     1 for PWM frequency of 31372.55 Hz
  //TCCR2B = TCCR2B & B11111000 | B00000010;    // set timer 2 divisor to     8 for PWM frequency of  3921.16 Hz
  //TCCR2B = TCCR2B & B11111000 | B00000011;    // set timer 2 divisor to    32 for PWM frequency of   980.39 Hz
  //TCCR2B = TCCR2B & B11111000 | B00000100;    // set timer 2 divisor to    64 for PWM frequency of   490.20 Hz (The DEFAULT)
  //TCCR2B = TCCR2B & B11111000 | B00000101;    // set timer 2 divisor to   128 for PWM frequency of   245.10 Hz
  //TCCR2B = TCCR2B & B11111000 | B00000110;    // set timer 2 divisor to   256 for PWM frequency of   122.55 Hz
  //TCCR2B = TCCR2B & B11111000 | B00000111;    // set timer 2 divisor to  1024 for PWM frequency of    30.64 Hz

  //TCCR1B = TCCR1B & B11111000 | B00000001;    // set timer 1 divisor to     1 for PWM frequency of 31372.55 Hz
  //TCCR1B = TCCR1B & B11111000 | B00000010;    // set timer 1 divisor to     8 for PWM frequency of  3921.16 Hz
  //TCCR1B = TCCR1B & B11111000 | B00000011;    // set timer 1 divisor to    64 for PWM frequency of   490.20 Hz (The DEFAULT)
  //TCCR1B = TCCR1B & B11111000 | B00000100;    // set timer 1 divisor to   256 for PWM frequency of   122.55 Hz
  //TCCR1B = TCCR1B & B11111000 | B00000101;    // set timer 1 divisor to  1024 for PWM frequency of    30.64 Hz
