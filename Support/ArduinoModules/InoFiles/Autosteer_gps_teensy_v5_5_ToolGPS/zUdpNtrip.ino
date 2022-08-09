void udpNtrip()
{
  // When ethernet is not running, return directly. parsePacket() will block when we don't
  if (!Ethernet_running)
  {
    return;
  }

  unsigned int packetLength = Eth_udpNtrip.parsePacket();

  if (packetLength > 0)
  {
    ////Serial.print("udpNtrip: ");
    ////Serial.println(packetLength);
    Eth_udpNtrip.read(Eth_NTRIP_packetBuffer, packetLength);

    for (unsigned int i = 0; i < packetLength; i++)
    {
      SerialGPS.write(Eth_NTRIP_packetBuffer[i]);
    }

    ntriptime_from_AgopenGPS = systick_millis_count;
  }
}

void ntripcheck()
{
  if (ntriptime_from_AgopenGPS > (systick_millis_count - 4000))
  {
   // digitalWrite(ntripLED, HIGH);
  }
  else
  {
   // digitalWrite(ntripLED, LOW);
  }
}
