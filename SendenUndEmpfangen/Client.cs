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
        public Task StarteClient()
        {
            return Task.Run(async () => { 
                try
                {

                    int tries = 5;
                    while (tries > 0)
                    {
                        // Eingabe der Konsole lesen
                        Console.Write("Schreibe eine Nachricht: ");
                        // Kodiere den Text zu einer Bytesequenz
                        var textBytes = Encoding.ASCII.GetBytes("HALLO, JEMAND DA?");
                        // Generiere Zielendpunkt
                        var endPoint = new IPEndPoint(destAddr, destPort);
                        // Sende die kodierte Nachricht als UDP-Paket
                        socket.SendTo(textBytes, 0, textBytes.Length, SocketFlags.None, endPoint);
                        //socket.ReceiveFrom(); // Hier wird die Antwort vom Server gesendet
                        await Task.Delay(1000);
                        tries--;
                    }
                }
                finally
                {
                    socket.Close();
                }
            });
        }

    }
}
