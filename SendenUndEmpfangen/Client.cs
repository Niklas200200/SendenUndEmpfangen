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
    class Client
    {
        //Eine Multicast Gruppenadresse zwischen: 224.0.0.0 - 239.255.255.255
        IPAddress destAddr = IPAddress.Parse("224.0.0.100");
        // Multicast port
        int destPort = 8080;
        // Time-to-live für das Datagramm, Standard TTL = 1
        int TTL = 1;
        // Multicast Socket
        Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

        public Client()
        {
            // Setze TTL
            socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.MulticastTimeToLive, TTL);
        }

        public void starteClient()
        {
            try
            {
                while (true)
                {

                    // Eingabe der Konsole lesen
                    Console.Write("Schreibe eine Nachricht: ");
                    var text = Console.ReadLine();
                    // Kodiere den Text zu einer Bytesequenz
                    var textBytes = Encoding.ASCII.GetBytes(text);
                    // Generiere Zielendpunkt
                    var endPoint = new IPEndPoint(destAddr, destPort);
                    // Sende die kodierte Nachricht als UDP-Paket
                    socket.SendTo(textBytes, 0, textBytes.Length, SocketFlags.None, endPoint);
                }
            }
            finally
            {
                socket.Close();
            }
        }

    }
}
