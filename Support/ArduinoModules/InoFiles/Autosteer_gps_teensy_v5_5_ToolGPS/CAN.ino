void CAN_setup (void) {

  //V_Bus is CAN-3 and is the Steering BUS
  V_Bus.begin();
  V_Bus.setBaudRate(250000);
  V_Bus.enableFIFO();
  V_Bus.setFIFOFilter(REJECT_ALL);
  V_Bus.setFIFOFilter(0, 0x1CEBFF80, EXT);
  V_Bus.setFIFOFilter(1, 0xCFE1080, EXT); //needed for Steering valve ok message 

  //K_Bus is CAN-1 and is the Main Tractor Bus
  K_Bus.begin();
  K_Bus.setBaudRate(100000);
  K_Bus.enableFIFO();
  K_Bus.setFIFOFilter(REJECT_ALL);
  //K_Bus.setFIFOFilter(0, 0x613, STD);  //Fendt Arm Rest Buttons Neue Armlehne
  K_Bus.setFIFOFilter(1, 0x61F, STD);  //Fendt Arm Rest Buttons Alte Armlehne
  K_Bus.setFIFOFilter(2, 0x3AF, STD);  //Fendt Arm Rest Buttons Alte Armlehne

}

void VBus_Send() {
  CAN_message_t VBusSendData;

  if (pwmDrive > 250) pwmDrive = 250;
  if (pwmDrive < -250) pwmDrive = -250;

  VBusSendData.id = 0x0CFE3016;
  VBusSendData.flags.extended = true;
  VBusSendData.len = 8;
  VBusSendData.buf[1] = 0;
  VBusSendData.buf[3] = 0;
  VBusSendData.buf[4] = 0;
  VBusSendData.buf[5] = 0;
  VBusSendData.buf[6] = 0;
  VBusSendData.buf[7] = 0;
  if (pwmDrive > 0)
  {
    VBusSendData.buf[0] = pwmDrive;
    VBusSendData.buf[2] = 1; //Extend
  }
  else if (pwmDrive < 0)
  {
    VBusSendData.buf[0] = -1 * pwmDrive;
    VBusSendData.buf[2] = 2; //Retract
  }
  else
  {
    VBusSendData.buf[0] = 0;
    VBusSendData.buf[2] = 0; //Retract
  }
  if (intendToSteer == 0) {
    VBusSendData.buf[0] = 0; //0,4% pro bit
    VBusSendData.buf[2] = 0; //Blocked
  }
  //  Serial.println("Out: ");
  //  for ( uint8_t i = 0; i < VBusSendData.len; i++ ) {
  //    Serial.print(VBusSendData.buf[i], HEX); Serial.print(" ");
  //  } Serial.println();
  V_Bus.write(VBusSendData);
}

void VBus_Receive() {
  CAN_message_t VBusReceiveData;
  if (V_Bus.read(VBusReceiveData)) {
    if (VBusReceiveData.id == 0xCFE1080) {
      //      Serial.println("In: ");
      //      for ( uint8_t i = 0; i < VBusReceiveData.len; i++ ) {
      //        Serial.print(VBusReceiveData.buf[i], HEX); Serial.print(" ");
      //      } Serial.println();
      if (VBusReceiveData.buf[2] != 14) steeringValveReady = 16;
      else steeringValveReady = 0;
    }
  }
}
