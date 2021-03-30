using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SendenUndEmpfangen
{
    class Server
    {
        //Eine Multicast Gruppenadresse zwischen: 224.0.0.0 - 239.255.255.255
        IPAddress address = IPAddress.Parse("224.0.0.100");
        // Multicast port
        int port = 8080;
        // Multicast Socket
        Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        Client client = new Client();
        public string nachricht { get; set; }


        public Server()
        {   
            // Adresse wiederverwenden
            sock.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, 1);
            // Generiere Endpunkt
            var endPoint = new IPEndPoint(IPAddress.Any, port);
            sock.Bind(endPoint);

            // Mitgliedschaft in der Multicast Gruppe
            sock.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, new MulticastOption(address, IPAddress.Any));
            var receivePoint = new IPEndPoint(IPAddress.Any, 0);
            var tempReceivePoint = (EndPoint)receivePoint;
        }
    
        public void starteServer()
        {
            var receivePoint = new IPEndPoint(IPAddress.Any, 0);
            var tempReceivePoint = (EndPoint)receivePoint;

            try
            {
                while (true)
                {                  
                    Console.WriteLine("Warte auf Nachrichten...");

                    // Generiere und empfange das Datagramm
                    var packet = new byte[1024];
                    var length = sock.ReceiveFrom(packet, ref tempReceivePoint);
                    // Paket erhalten von
                    var host = (tempReceivePoint as IPEndPoint)?.Address?.ToString();
                    // Textdekodierer
                    var receivedText = Encoding.ASCII.GetString(packet, 0, length);
                    // Nachricht ausgeben
                    Console.WriteLine($"Neue Nachricht von {host}: {receivedText}");
                }
            }
            finally
            {
                // Kündige Mitgliedschaft in der Multicast Gruppe
                sock.SetSocketOption(SocketOptionLevel.IP,
                                     SocketOptionName.DropMembership,
                                     new MulticastOption(address, IPAddress.Any));
                sock.Close();
            }
        }


        

    }
}
