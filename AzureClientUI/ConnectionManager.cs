using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace AzureClientUI
{
    class ConnectionManager
    {
        public ClientConnection Connect(string ip, int port)
        {
            TcpClient client = null;
            try
            {
                client = new TcpClient();
                client.Connect(ip, port);

                return new ClientConnection(client);
            } catch (SocketException e)
            {
                Console.WriteLine("SocketException e: {0}", e);
            }
            return null;
        }
    }
}
