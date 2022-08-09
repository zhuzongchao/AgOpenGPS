using System;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace AgIO
{
    public class CTraffic
    {
        public int cntrPGNFromAOG = 0;
        public int cntrPGNToAOG = 0;
     
        //public int cntrUDPOut = 0;
        //public int cntrUDPIn = 0;

        public int cntrGPSOut = 0;
        public int cntrGPSInBytes = 0;
        public int cntrGPSIn = 0;

        public int cntrGPS2In = 0;
        public int cntrGPS2Out = 0;

        public int cntrSteerIn = 0;
        public int cntrSteerOut = 0;

        public int cntrMachineIn = 0;
        public int cntrMachineOut = 0;

        public int cntrModule3In = 0;
        public int cntrModule3Out = 0;

        public uint helloFromMachine = 0, helloFromAutoSteer = 0;
    }

    public partial class FormLoop
    {
        // loopback Socket
        private Socket loopBackSocket;
        private EndPoint endPointLoopBack = new IPEndPoint(IPAddress.Loopback, 0);

        // UDP Socket
        private Socket UDPSocket;
        private EndPoint endPointUDP = new IPEndPoint(IPAddress.Any, 0);

        private bool isUDPNetworkConnected;

        //2 endpoints for local and 2 udp
        private IPEndPoint epAgOpen = new IPEndPoint(IPAddress.Parse("127.255.255.255"), 15555);
        private IPEndPoint epAgVR = new IPEndPoint(IPAddress.Parse("127.255.255.255"), 16666);
        private IPEndPoint epModule = new IPEndPoint(IPAddress.Parse("192.168.5.255"), 8888);
        private IPEndPoint epNtrip;

        //class for counting bytes
        public CTraffic traffic = new CTraffic();

        // Data stream
        private byte[] buffer = new byte[1024];

        #region Load Network

        //initialize loopback and udp network
        private void LoadUDPNetwork()
        {
            bool isFound = false;

            try //udp network
            {
                foreach (IPAddress IPA in Dns.GetHostAddresses(Dns.GetHostName()))
                {
                    if (IPA.AddressFamily == AddressFamily.InterNetwork)
                    {
                        byte[] data = IPA.GetAddressBytes();
                        //  Split string by ".", check that array length is 3
                        if (data[0] == 192 && data[1] == 168 && data[2] == 5)
                        {
                            if (data[3] < 255 && data[3] > 1)
                            {
                                isFound = true;
                                break;
                            }
                        }
                    }
                }

                isFound = true; 

                if (isFound)
                {
                    // Initialise the socket
                    UDPSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                    UDPSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, true);
                    UDPSocket.Bind(new IPEndPoint(IPAddress.Any, 9999));
                    UDPSocket.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref endPointUDP,
                        new AsyncCallback(ReceiveDataUDPAsync), null);

                    isUDPNetworkConnected = true;
                    btnUDP.BackColor = Color.LightGreen;
                }

                else
                {
                    MessageBox.Show("Network Address -> 192.168.5.[2 - 254] May not exist. \r\n"
                    + "Are you sure ethernet is connected?\r\n\r\n", "Network Connection Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnUDP.BackColor = Color.Orange;
                }
            }
            catch (Exception e)
            {
                //WriteErrorLog("UDP Server" + e);
                MessageBox.Show(e.Message, "Network Connection Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnUDP.BackColor = Color.Red;
            }
        }

        private void LoadLoopback()
        {
            try //loopback
            {
                loopBackSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                loopBackSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, true);
                loopBackSocket.Bind(new IPEndPoint(IPAddress.Loopback, 17777));
                loopBackSocket.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref endPointLoopBack,
                    new AsyncCallback(ReceiveDataLoopAsync), null);
            }
            catch (Exception ex)
            {
                //lblStatus.Text = "Error";
                MessageBox.Show("Load Error: " + ex.Message, "Loopback Server", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        public byte[] nmeaHeader = new byte[] { 0x80, 0x81, 0x7F, 0xCF };

        #region Send LoopBack

        //local back to AOG - packaged into pgn
        private void SendToLoopBackMessageAOG(byte[] byteData)
        {
            traffic.cntrPGNToAOG += byteData.Length;
            SendDataToLoopBack(byteData, epAgOpen);
        }

        private void SendToLoopBackMessageVR(byte[] byteData)
        {
            SendDataToLoopBack(byteData, epAgVR);
        }

        private void SendDataToLoopBack(byte[] byteData, IPEndPoint endPoint)
        {
            try
            {
                if (byteData.Length != 0)
                {
                    // Send packet to AgVR
                    loopBackSocket.BeginSendTo(byteData, 0, byteData.Length, SocketFlags.None, endPoint,
                        new AsyncCallback(SendDataLoopAsync), null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Send Error: " + ex.Message, "UDP Client", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void SendDataLoopAsync(IAsyncResult asyncResult)
        {
            try
            {
                loopBackSocket.EndSend(asyncResult);
            }
            catch (Exception ex)
            {
                MessageBox.Show("SendData Error: " + ex.Message, "UDP Server", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Receive LoopBack

        private void ReceiveFromLoopBack(byte[] data)
        {
            traffic.cntrPGNFromAOG += data.Length;

            //Send out to udp network
            SendUDPMessage(data, epModule);

            //send out to VR Loopback
            if (isPluginUsed) SendToLoopBackMessageVR(data);

        }

        private void ReceiveDataLoopAsync(IAsyncResult asyncResult)
        {
            try
            {
                // Receive all data
                int msgLen = loopBackSocket.EndReceiveFrom(asyncResult, ref endPointLoopBack);

                byte[] localMsg = new byte[msgLen];
                Array.Copy(buffer, localMsg, msgLen);

                // Listen for more connections again...
                loopBackSocket.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref endPointLoopBack,
                    new AsyncCallback(ReceiveDataLoopAsync), null);

                BeginInvoke((MethodInvoker)(() => ReceiveFromLoopBack(localMsg)));
            }
            catch (Exception)
            {
                //MessageBox.Show("ReceiveData Error: " + ex.Message, "UDP Server", MessageBoxButtons.OK,
                //MessageBoxIcon.Error);
            }
        }

        #endregion

        #region SendUDP
        public void SendUDPMessage(byte[] byteData, IPEndPoint endPoint)
        {
            if (isUDPNetworkConnected)
            {
                try
                {
                    // Send packet to the zero
                    if (byteData.Length != 0)
                    {
                        UDPSocket.BeginSendTo(byteData, 0, byteData.Length, SocketFlags.None,
                           endPoint, new AsyncCallback(SendDataUDPAsync), null);

                        if (byteData[3] == 254)
                            traffic.cntrSteerIn += byteData.Length;

                        if (byteData[3] == 239)
                            traffic.cntrMachineIn += byteData.Length;
                    }
                }
                catch (Exception)
                {
                    //WriteErrorLog("Sending UDP Message" + e.ToString());
                    //MessageBox.Show("Send Error: " + e.Message, "UDP Client", MessageBoxButtons.OK,
                    //MessageBoxIcon.Error);
                }
            }
        }

        private void SendDataUDPAsync(IAsyncResult asyncResult)
        {
            try
            {
                UDPSocket.EndSend(asyncResult);
            }
            catch (Exception)
            {
                //WriteErrorLog(" UDP Send Data" + e.ToString());
                //MessageBox.Show("SendData Error: " + e.Message, "UDP Server", MessageBoxButtons.OK,
                //MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Receive UDP

        private void ReceiveFromUDP(int port, byte[] data)
        {
            if (port != 10000)
            {
                if (data[0] == 0x80 && data[1] == 0x81)
                {
                    //module return via udp sent to AOG
                    SendToLoopBackMessageAOG(data);

                    //module data also sent to VR
                    if (isPluginUsed) SendToLoopBackMessageVR(data);

                    if (data[3] == 253) traffic.cntrSteerOut += data.Length;
                    if (data[3] == 237) traffic.cntrMachineOut += data.Length;

                    //reset hello counters
                    if (data[3] == 199) traffic.helloFromAutoSteer = 0;
                    if (data[3] == 197) traffic.helloFromMachine = 0;
                }
                //$ = 36 G=71 P=80 K=75
                else if (data[0] == 36 && (data[1] == 71 || data[1] == 80 || data[1] == 75))
                {
                    //if (timerSim.Enabled) DisableSim();
                    traffic.cntrGPSOut += data.Length;
                    rawBuffer += Encoding.ASCII.GetString(data);
                    ParseNMEA(ref rawBuffer);
                }
            }
            
            else //is tool NMEA set to port 10,000
            {
                if (data[0] == 0x80 && data[1] == 0x81)
                {
                    //module return via udp sent to AOG
                    SendToLoopBackMessageAOG(data);

                    return;

                    //module data also sent to VR
                    //SendToLoopBackMessageVR(data);
                }

                //GGA or Panda or PAOGI
                if (data[0] == 36 && (data[1] == 71 || data[1] == 80))
                {
                    traffic.cntrGPS2In += data.Length;
                    rawBuffer2 += Encoding.ASCII.GetString(data);
                    ParseNMEA2(ref rawBuffer2);
                }
            }

            //traffic.cntrUDPIn += data.Length;
        }

        private void ReceiveDataUDPAsync(IAsyncResult asyncResult)
        {
            try
            {
                // Receive all data
                int msgLen = UDPSocket.EndReceiveFrom(asyncResult, ref endPointUDP);

                byte[] localMsg = new byte[msgLen];
                Array.Copy(buffer, localMsg, msgLen);

                // Listen for more connections again...
                UDPSocket.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref endPointUDP,
                    new AsyncCallback(ReceiveDataUDPAsync), null);
                int port = ((IPEndPoint)endPointUDP).Port;

                BeginInvoke((MethodInvoker)(() => ReceiveFromUDP(port, localMsg)));

            }
            catch (Exception)
            {
                //WriteErrorLog("UDP Recv data " + e.ToString());
                //MessageBox.Show("ReceiveData Error: " + e.Message, "UDP Server", MessageBoxButtons.OK,
                //MessageBoxIcon.Error);
            }
        }

        #endregion
    }
}
