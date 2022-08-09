// TODO: Put ethernet related stuff in here?

void EthernetStart()
{

  // start the Ethernet connection:
  Serial.println("Initialize Ethernet");

  // Check for Ethernet hardware present
  if (Ethernet.hardwareStatus() == EthernetNoHardware)
  {
    Serial.println("Ethernet hardware was not found.");
  }

  Ethernet.begin(mac, Eth_myip);
  Serial.println("Ethernet status OK");
  if (Ethernet.linkStatus() == LinkOFF)
  {
    Serial.println("Ethernet cable is not connected.");
  }

  Serial.print("Ethernet IP of module: "); Serial.println(Ethernet.localIP());
  Serial.print("Ethernet sending to IP: "); Serial.println(ipDestination);

  // init UPD Port sending to AOG
  if (Eth_udpPAOGI.begin(portMy))
  {
    Serial.print("Ethernet UDP sending from port: ");
    Serial.println(portMy);
  }

  // init UPD Port getting NTRIP from AOG
  if (Eth_udpNtrip.begin(AOGNtripPort)) // AOGNtripPort
  {
    Serial.print("Ethernet NTRIP UDP listening to port: ");
    Serial.println(AOGNtripPort);
  }

  // init UPD Port getting AutoSteer data from AOG
  if (Eth_udpAutoSteer.begin(AOGAutoSteerPort)) // AOGAutoSteerPortipPort
  {
    Serial.print("Ethernet AutoSteer UDP listening to port: ");
    Serial.println(AOGAutoSteerPort);
  }

  Ethernet_running = true;
}
