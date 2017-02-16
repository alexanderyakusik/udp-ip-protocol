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
