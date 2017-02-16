using System;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace udp_ip_server
{
    class Program
    {
        static void Main(string[] args)
        {
            IPAddress ip = IPAddress.Parse("192.168.25.255");
            const int port = 23000;
            const int sendingInterval = 1000;

            var serverSocket = new UdpServerSocket(ip, port);
            try
            { 
                while(true)
                {
                    string currentTime = DateTime.Now.ToString("HH:mm:ss");
                    byte[] sendingData = Encoding.UTF8.GetBytes(currentTime);
                    serverSocket.Send(sendingData);
                    Thread.Sleep(sendingInterval);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                serverSocket.Close();
            }
        }
    }

    public class UdpServerSocket
    {
        public IPEndPoint remoteEndPoint { get; }
        private Socket socket;

        public UdpServerSocket(IPAddress ip, int port)
        {
            remoteEndPoint = new IPEndPoint(ip, port);
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        }

        public int Send(byte[] data)
        {
            int bytesSent = -1;
            try
            {
                bytesSent = socket.SendTo(data, remoteEndPoint);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                socket.Close();
            }
            return bytesSent;
        }

        public void Close()
        {
            socket.Close();
        }
    }
}
