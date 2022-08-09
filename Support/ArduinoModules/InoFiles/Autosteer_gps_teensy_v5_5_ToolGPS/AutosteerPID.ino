void calcSteeringPID(void)
{
  //propGainortional only
  pValue = steerSettings.Kp * steerAngleError * 0.1;

  pwmDrive = (int16_t)pValue;

  //add min throttle factor so no delay from motor resistance.
  if (pwmDrive < 0 ) pwmDrive -= steerSettings.minPWM;
  else if (pwmDrive > 0 ) pwmDrive += steerSettings.minPWM;

  pwmDrive = constrain(pwmDrive, -steerSettings.highPWM, steerSettings.highPWM);
  pwmDrive = constrain(pwmDrive, -250, 250); //PVED-CC only accept 250

  if (steerConfig.MotorDriveDirection) pwmDrive *= -1;
  
  pwmDisplay = pwmDrive;
//  Serial.print(" PWM: ");
//  Serial.println(pwmDrive);
}

//#########################################################################################

void motorDrive(void)
{
  //Danfoss PVED-CC valve
  CAN_message_t VBusSendData;

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
    VBusSendData.buf[0] = pwmDrive; //0,4% pro bit (250 * 0,4 = 100 %)
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



  //Fendt experimental (simulating the Armrest blue/yellow valve)
  CAN_message_t buttonData;
  buttonData.id = 0X3AF;
  buttonData.len = 8;

  //Serial.print("i: " );
  //Serial.println(i);
  unsigned int val;
  val = map (pwmDrive, -250, 250, 7012, 1012);

  //Serial.println(val, DEC);
  actualBlueYellow[5] = highByte(val);
  actualBlueYellow[4] = lowByte(val);

  //Serial.println("bytes");
  // Serial.println(actualBlueYellow[5], HEX);

  //Serial.println(actualBlueYellow[4] , HEX);
  for ( uint8_t j = 0; j < sizeof(actualBlueYellow); j++ )
  {
    buttonData.buf[j] = actualBlueYellow[j];
    //Serial.print(buttonData.buf[j], HEX);
    //Serial.print(" ");
  }
  //Serial.println("");
  //val = buttonData.buf[5] * 254 + buttonData.buf[4];
  //val = map(val, 7012, 1012, -100, 100);
  // Serial.println(val, DEC);
  // Serial.println("");

  K_Bus.write(buttonData);
}
