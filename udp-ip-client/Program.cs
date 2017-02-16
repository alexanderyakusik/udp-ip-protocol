using System;
using System.Text;
using System.Net.Sockets;
using System.Net;

namespace udp_ip_client
{
    class Program
    {
        static void Main(string[] args)
        {
            IPAddress[] ipv4Addresses = Array.FindAll( Dns.GetHostEntry(String.Empty).AddressList, 
                                                       a => a.AddressFamily == AddressFamily.InterNetwork);
            IPAddress ip = ipv4Addresses[0];
            const int port = 23000;

            var clientSocket = new UdpClientSocket(ip, port);
            try
            {
                Console.WriteLine( Encoding.UTF8.GetString(clientSocket.Receive()) );
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                clientSocket.Close();
            }
        }
    }

    public class UdpClientSocket
    {
        private Socket socket;
        public IPEndPoint localEndPoint { get; }

        public UdpClientSocket(IPAddress ip, int port)
        {
            localEndPoint = new IPEndPoint(ip, port);
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            socket.Bind(localEndPoint);
        }

        public byte[] Receive()
        {
            var buffer = new byte[256];
            var bytesAmount = socket.Receive(buffer);
            var resultBytes = new byte[bytesAmount];
            Array.Copy(buffer, resultBytes, bytesAmount);

            return resultBytes;
        }

        public void Close()
        {
            socket.Close();
        }
    }
}
