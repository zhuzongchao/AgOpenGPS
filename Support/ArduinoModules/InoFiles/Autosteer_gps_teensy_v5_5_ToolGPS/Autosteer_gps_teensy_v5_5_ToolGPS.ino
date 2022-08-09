// Basic Dualheading for AGopenGPS
// Antennas cross to driveDirection on cabin symmetrical
// right Antenna is Rover (A) for position, left Antenna is MB (B) for heading
//
// connection plan:
// Teensy Serial 2 RX (28) to F9P Position receiver TX1 (Position data)
// Teensy Serial 2 TX (29) to F9P Position receiver RX1 (RTCM data for RTK)
// Teensy Serial 7 RX (7) to F9P Heading receiver TX1 (Relative position from left antenna to right antenna)
// Teensy Serial 7 TX (8) to F9P Heading receiver RX1 (not used)
// F9P Position receiver TX2 to F9P Heading receiver RX2 (RTCM data for Moving Base)
//
// Configuration of receiver
// Position F9P
// CFG-RATE-MEAS - 100 ms -> 10 Hz
// CFG-UART1-BAUDRATE 115200
// Serial 1 In - RTCM (Correction Data from AGO)
// Serial 1 Out - NMEA GGA/VTG
// CFG-UART2-BAUDRATE 460800 ??
// Serial 2 Out - RTCM 1074,1084,1094,1124,1230,4072.0 (Correction data for Heading F9P, Moving Base)
//
// Heading F9P
// CFG-RATE-MEAS - 100 ms -> 10 Hz
// CFG-UART1-BAUDRATE 115200
// Serial 1 Out - UBX-NAV-RELPOSNED
// CFG-UART2-BAUDRATE 460800 ??
// Serial 2 In RTCM

// libraries -------------------------------

#include "FlashTxx.h"   // TLC/T3x/T4x/TMM flash primitives
#include <NativeEthernet.h>
#include <NativeEthernetUdp.h>
#include <Oversampling.h>

/************************* User Settings *************************/
// Serial Ports
#define SerialGPS Serial2
#define SerialGPS2 Serial7
#define SerialAOG Serial

const int32_t baudAOG = 115200;
const int32_t baudGPS = 115200;

// IP address to send UDP data to
IPAddress Eth_myip(192, 168, 5, 111);
IPAddress ipDestination(192, 168, 5, 255);

#define ntripLED 13
#define GGAReceivedLED 13
#define wasInput 11
#define valveReadyLED 5        //Option for LED, CAN Valve Ready To Steer.
#define engageLED 24    //Option for LED, to see if Engage message is recived.

char nmeaBuffer[150];
int count = 0;
bool stringComplete = false;

//Septentrio or Dual F9P
bool useDualF9P = false;

// is the GGA the second sentence?
const bool isLastSentenceGGA = true;

/*****************************************************************/
//CAN Specific
uint8_t steeringValveReady = 0;   //Variable for Steering Valve State from CAN
boolean intendToSteer = 0;        //Do We Intend to Steer?
byte actualBlueYellow[8] = {0x04, 0x91, 0xB4, 0x0F, 0xB4, 0x0F, 0x00, 0x00} ;

#include <FlexCAN_T4.h>
FlexCAN_T4<CAN1, RX_SIZE_256, TX_SIZE_16> K_Bus;    //Tractor / Control Bus
FlexCAN_T4<CAN2, RX_SIZE_256, TX_SIZE_16> ISO_Bus;  //ISO Bus
FlexCAN_T4<CAN3, RX_SIZE_256, TX_SIZE_16> V_Bus;    //Steering Valve Bus
//END CAN Specific
/*****************************************************************/

Oversampling adcTeensy(12, 16, 2);

// Ntrip
unsigned long ntriptime_from_AgopenGPS = systick_millis_count;

// Ethernet
byte mac[] = {0x90, 0xA2, 0xDA, 0x10, 0xB3, 0x1B}; // original

unsigned int portMy = 5544;             //this is port of this module: Autosteer = 5577 IMU = 5566 GPS = // TODO: JvdH check port number assignments
unsigned int AOGNtripPort = 2233;       //port NTRIP data from AOG comes in
unsigned int AOGAutoSteerPort = 8888;       //port Autosteer data from AOG comes in
unsigned int portDestination = 9999;    //Port of AOG that listens
bool Ethernet_running = false;
char Eth_NTRIP_packetBuffer[512];// buffer for receiving and sending data

byte CK_A = 0;
byte CK_B = 0;
int relposnedByteCount = 0;

// From basic_autosteer
#include "zNMEAParser.h"

bool useDual = false;
bool dualReadyGGA = false;
bool dualReadyRelPos = false;

//Dual
double headingcorr = 900;  //90deg heading correction (90deg*10)

float baseline;
double baseline2;
float rollDual;
float rollDualRaw;
double relPosD;
double relPosDH;
double heading = 0;

byte ackPacket[72] = {0xB5, 0x62, 0x01, 0x3C, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00};

constexpr int serial_buffer_size = 512;
uint8_t GPSrxbuffer[serial_buffer_size];   //Extra serial rx buffer
uint8_t GPStxbuffer[serial_buffer_size];   //Extra serial tx buffer
uint8_t GPS2rxbuffer[serial_buffer_size];   //Extra serial rx buffer
uint8_t GPS2txbuffer[serial_buffer_size];   //Extra serial tx buffer

/* A parser is declared with 3 handlers at most */
NMEAParser<2> parser;

bool isTriggered = false;
bool blink = false;

uint32_t lastTime = 0;

EthernetUDP Eth_udpPAOGI;
EthernetUDP Eth_udpNtrip;
EthernetUDP Eth_udpAutoSteer;

//******************************************************************************
// hex_info_t struct for hex record and hex file info
//******************************************************************************
typedef struct {  //
  char *data;   // pointer to array allocated elsewhere
  unsigned int addr;  // address in intel hex record
  unsigned int code;  // intel hex record type (0=data, etc.)
  unsigned int num; // number of data bytes in intel hex record

  uint32_t base;  // base address to be added to intel hex 16-bit addr
  uint32_t min;   // min address in hex file
  uint32_t max;   // max address in hex file

  int eof;    // set true on intel hex EOF (code = 1)
  int lines;    // number of hex records received
} hex_info_t;

uint32_t buffer_addr, buffer_size;
bool updatemode = false;
static char data[16];// buffer for hex data

hex_info_t hex =
{ // intel hex info struct
  data, 0, 0, 0,          //   data,addr,num,code
  0, 0xFFFFFFFF, 0,           //   base,min,max,
  0, 0            //   eof,lines
};

byte OTAUpdate[9] = {0x4f, 0x54, 0x41, 0x55, 0x70, 0x64, 0x61, 0x74, 0x65};


// Setup procedure ------------------------
void setup()
{
  pinMode(ntripLED, OUTPUT);
  pinMode(GGAReceivedLED, OUTPUT);

  // the dash means wildcard
  parser.setErrorHandler(errorHandler);
  parser.addHandler("G-GGA", GGA_Handler);
  parser.addHandler("G-VTG", VTG_Handler);

  delay(10);
  Serial.begin(baudAOG);
  delay(10);
  Serial.println("Start setup");

  SerialGPS.begin(baudGPS);
  SerialGPS.addMemoryForRead(GPSrxbuffer, serial_buffer_size);
  SerialGPS.addMemoryForWrite(GPStxbuffer, serial_buffer_size);

  delay(10);
  SerialGPS2.begin(baudGPS);
  SerialGPS2.addMemoryForRead(GPS2rxbuffer, serial_buffer_size);
  SerialGPS2.addMemoryForWrite(GPS2txbuffer, serial_buffer_size);

  Serial.println("SerialGPS and SerialGPS2 initialized");


  Serial.println("Starting Ethernet...");
  EthernetStart();
  Serial.println("Started Ethernet...");

  
  Serial.println("Starting AutoSteer...");
  autosteerSetup();
  Serial.println("Started AutoSteer");

  //CAN Specific
  Serial.println("Starting CAN");
  pinMode(valveReadyLED, OUTPUT);    //CAN Valve Ready LED
  digitalWrite(valveReadyLED, LOW);

  pinMode(engageLED, OUTPUT); //CAN engage LED
  digitalWrite(engageLED, LOW);

  CAN_setup();   //Run the Setup void (CAN page)
  Serial.println("CAN started");
  //End CAN Specific

  Serial.println("End setup");
  Serial.println("");
}

void loop()
{
  // Serial.println("Dual loop");

  if (Serial.available())
  {
    // If RTCM3 comes in Serial (USB),
    // read a byte, then
    // send it out SerialGPS from 16 to simpleRTK RX 1. Antenna = RTCM
    uint8_t c = Serial.read();
    SerialGPS.write(c);

    if (c != '  ')
    {
      // if the byte is a newline character
      ntripcheck();
    }

    ntriptime_from_AgopenGPS = systick_millis_count;
  }
  else
  {
    ntripcheck();
  }

  if (useDualF9P)
  {
    if (SerialGPS.available())
    {
      // If anything comes in SerialGPS
      uint8_t inByte = SerialGPS.read(); // read it and send for NMEA_PAOGI
      //Serial.println("SerialGPS available");
      //Serial.write(inByte);
      //NMEA_read();
      parser << inByte;
    }
  }
  else //e.g. Septentrio Heading
  {
    while (SerialGPS.available())
    {
      char c = SerialGPS.read();
      nmeaBuffer[count++] = c;
      if (c == '\n')stringComplete = true;
      if (count == 150 || stringComplete == true)break;
    }

    if (count == 150 || stringComplete == true) {
      if (stringComplete == true) {
        Eth_udpPAOGI.beginPacket(ipDestination, portDestination);
        Eth_udpPAOGI.write(nmeaBuffer, count);
        Eth_udpPAOGI.endPacket();
        //Serial.print(nmeaBuffer);
      }
      clearBufferArray();
      count = 0;
    }
  }

  if (dualReadyGGA == true && dualReadyRelPos == true)
  {
    BuildNmea();
    dualReadyGGA = false;
    dualReadyRelPos = false;
  }

  if (SerialGPS2.available())
  {
    // If anything comes in SerialGPS2
    //Serial.println("SerialGPS2 available");
    uint8_t incoming_char = SerialGPS2.read();  // ESP32 read RELPOSNED from F9P
    //Serial.write(incoming_char);

    // Just increase the byte counter for the first 3 bytes
    if (relposnedByteCount < 4 && incoming_char == ackPacket[relposnedByteCount])
    {
      relposnedByteCount++;
    }
    else if (relposnedByteCount > 3)
    {
      // Real data, put the received bytes in the buffer
      ackPacket[relposnedByteCount] = incoming_char;
      relposnedByteCount++;
    }
    else
    {
      // Reset the counter, becaues the start sequence was broken
      relposnedByteCount = 0;
    }
  }

  // Check the message when the buffer is full
  if (relposnedByteCount > 71)
  {
    if (calcChecksum())
    {
      //Serial.println("ACK Received! ");
      useDual = true;
      relPosDecode();
    }
    // else
    // {
    //   Serial.println("ACK Checksum Failure: ");
    // }

    relposnedByteCount = 0;
  }

  //gpsCurrentTime = systick_millis_count;

  udpNtrip();
  autosteerLoop();
  //CAN Specific
  VBus_Receive();
  //End CAN Specific
}

// void checksum() { // clashes with variable named checksum
bool calcChecksum()
{
  CK_A = 0;
  CK_B = 0;

  for (int i = 2; i < 70; i++)
  {
    CK_A = CK_A + ackPacket[i];
    CK_B = CK_B + CK_A;
  }

  return (CK_A == ackPacket[70] && CK_B == ackPacket[71]);
}

void clearBufferArray()
{
  for (int i = 0; i < count; i++)
  {
    nmeaBuffer[i] = NULL;
    stringComplete = false;
  }
}

//******************************************************************************
// process_hex_record()    process record and return okay (0) or error (1)
//******************************************************************************
int process_hex_record( char *packetBuffer, int packetSize)
{
  if (packetSize < 5)
  {
    return 1;
  }
  else if (packetBuffer[0] != 0x3a)
  {
    //Serial.printf( "abort - invalid hex code %d\n", hex.code );
    return 0;
  }
  else
  {
    for (byte idx = 1; idx + 4 < packetSize;)
    {
      byte len = packetBuffer[idx];
      unsigned int addr = packetBuffer[idx + 1] << 8 | packetBuffer[idx + 2];

      byte type = packetBuffer[idx + 3];
      if (idx + 4 + len < packetSize)
      {
        unsigned sum = (len & 255) + ((addr >> 8) & 255) + (addr & 255) + (type & 255);

        for (byte j = 0; j < len; j++)
        {
          sum += packetBuffer[idx + 4 + j] & 255;
          hex.data[j] = packetBuffer[idx + 4 + j];
        }
        hex.num = len;
        hex.code = type;
        hex.addr = addr;

        byte Checksum = packetBuffer[idx + 4 + len];
        if (((sum & 255) + (Checksum & 255)) & 255)
        {
          Serial.println("abort - bad hex line");
          return 1;
        }
        else
        {
          if (hex.code == 0)// if data record
          {
            //Serial.println("Hex 0");
            // update min/max address so far
            if (hex.base + hex.addr + hex.num > hex.max)
              hex.max = hex.base + hex.addr + hex.num;
            if (hex.base + hex.addr < hex.min)
              hex.min = hex.base + hex.addr;

            uint32_t addr = buffer_addr + hex.base + hex.addr - FLASH_BASE_ADDR;
            if (hex.max > (FLASH_BASE_ADDR + buffer_size))
            {
              Serial.printf( "abort - max address %08lX too large\n", hex.max );
              return 1;
            }
            else if (!IN_FLASH(buffer_addr))
            {
              memcpy( (void*)addr, (void*)hex.data, hex.num );
            }
            else if (IN_FLASH(buffer_addr))
            {
              int error = flash_write_block( addr, hex.data, hex.num );
              if (error)
              {
                Serial.println();
                Serial.printf( "abort - error %02X in flash_write_block()\n", error );
                return 1;
              }
            }
          }
          else if (hex.code == 1)
          {
            // EOF (:flash command not received yet)
            Serial.println( "EOF");
            hex.eof = 1;
          }
          else if (hex.code == 2)
          {
            // extended segment address (top 16 of 24-bit addr)
            Serial.println("Hex 2");
            hex.base = ((hex.data[0] << 8) | hex.data[1]) << 4;
          }
          else if (hex.code == 4)
          { // extended linear address (top 16 of 32-bit addr)
            Serial.println("Hex 4");
            hex.base = ((hex.data[0] << 8) | hex.data[1]) << 16;
          }
          else if (hex.code == 5)
          { // start linear address (32-bit big endian addr)
            Serial.println("Hex 5");
            hex.base = (hex.data[0] << 24) | (hex.data[1] << 16)
                       | (hex.data[2] <<  8) | (hex.data[3] <<  0);
          }
          else if (hex.code == 6)
          {
            Serial.printf( "UPDATE!!!\n" );
            // move new program from buffer to flash, free buffer, and reboot
            flash_move( FLASH_BASE_ADDR, buffer_addr, hex.max - hex.min );

            // should not return from flash_move(), but put REBOOT here as reminder
            REBOOT;
            return 0;
          }
          else if (hex.code == 7)
          {
            Serial.printf( "LINES DONT Match\n" );
            return 1;
          }
          else
          {
            // error on bad hex code
            Serial.println();
            Serial.printf( "abort - invalid hex code %d\n", hex.code );
            return 1;
          }

          hex.lines++;
          //Serial.println(hex.lines);

          if (hex.eof)
          {
            Serial.println();
            Serial.printf( "\nhex file: %1d lines %1lu bytes (%08lX - %08lX)\n", hex.lines, hex.max - hex.min, hex.min, hex.max );

            // check FSEC value in new code -- abort if incorrect

#if defined(KINETISK) || defined(KINETISL)
            uint32_t value = *(uint32_t *)(0x40C + buffer_addr);
            if (value == 0xfffff9de)
            {
              Serial.println();
              Serial.printf( "new code contains correct FSEC value %08lX\n", value );
            }
            else
            {
              Serial.println();
              Serial.printf( "abort - FSEC value %08lX should be FFFFF9DE\n", value );
              return 1;
            }
#endif

            // check FLASH_ID in new code - abort if not found
            if (check_flash_id( buffer_addr, hex.max - hex.min ))
            {
              Serial.println();
              Serial.printf( "new code contains correct target ID %s\n", FLASH_ID );
              SENDCheckUdp();
            }
            else
            {
              Serial.println();
              Serial.printf( "abort - new code missing string %s\n", FLASH_ID );
              return 1;
            }
          }
          idx += 6 + len;//need for extra ::
        }
      }
      else
      {
        Serial.printf( "abort - invalid hex code %d\n", hex.code );
        return 1;
      }
    }
  }
  return 0;
}
