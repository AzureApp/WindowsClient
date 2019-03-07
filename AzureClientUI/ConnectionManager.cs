using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace AzureClientUI
{
    public class ConnectionManager
    {
        private List<ClientConnection> connections = new List<ClientConnection>();

        private static ManualResetEvent connectDone = new ManualResetEvent(false);

        public ClientConnection Connect(string ip, int port)
        {
            Socket socket = null;
            try
            {
                IPAddress ipAddress = IPAddress.Parse(ip);
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, port);

                socket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                socket.BeginConnect(remoteEP, new AsyncCallback(ConnectCallback), socket);
                if (connectDone.WaitOne(1000))
                {
                    var cc = new ClientConnection(socket);
                    connections.Add(cc);
                    return cc;
                }
                return null;
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException e: {0}", e);
            }
            return null;
        }

        private static void ConnectCallback(IAsyncResult result)
        {
            Socket socket = (Socket)result.AsyncState;
            try
            {
                socket.EndConnect(result);

                Console.WriteLine("New connection at {0}", socket.RemoteEndPoint.ToString());

                connectDone.Set();
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to connect to target at {0} [error {1}]",
                    socket.RemoteEndPoint.ToString(),
                    e.ToString());
            }
        }
    }
}
