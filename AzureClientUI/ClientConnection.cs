using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using MsgPack.Serialization;
using AzureClientUI.DataObjects;
using System.Threading;
using System.IO;

namespace AzureClientUI
{

    public class MessageArgs : EventArgs
    {
        public MessageArgs(MetaObject obj)
        {
            this.Message = obj;
        }

        public MetaObject Message { get; }
    }

    internal class ReadState
    {
        // Socket in use
        public Socket socket = null;
        // size of initial buffer (buffer gets resized if read amount is above this limit)
        public const int BufferSize = 1024;
        // byte array to hold read bytes
        public byte[] buffer = null;
        // memory stream holds read data
        public MemoryStream stream = new MemoryStream();
        // binary writer
        public BinaryWriter writer = null;

        public ReadState()
        {
            writer = new BinaryWriter(stream);
        }
    }


    public class ClientConnection
    {
        private Socket socket;

        public EventHandler<MessageArgs> OnNewMessage;

        private static ManualResetEvent sendDone = new ManualResetEvent(false);
        private static ManualResetEvent receiveDone = new ManualResetEvent(false);

        public ClientConnection(Socket socket)
        {
            this.socket = socket;
        }

        private static string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }

        public void Run()
        {
            try
            {
                // Create the state object.  
                ReadState state = new ReadState();
                state.socket = socket;

                // Begin receiving the data from the remote device.  
                socket.BeginReceive(state.buffer, 0, ReadState.BufferSize, 0,
                    new AsyncCallback(ReceiveCallback), state);
            }
            catch (Exception e)
            {
                Console.WriteLine("Could not receive from connection {0} [error: {1}]",
                    socket.RemoteEndPoint.ToString(),
                    e.ToString());
            }

            // On a new connection we want to immidiately send a HandshakeObject 
            // this lets the server know info about our client
            HandshakeObject obj = new HandshakeObject();

            var context = new SerializationContext();
            context.EnumSerializationOptions.SerializationMethod = EnumSerializationMethod.ByUnderlyingValue;

            var serializer = MessagePackSerializer.Get<HandshakeObject>(context);

            var bytes = serializer.PackSingleObject(obj);
            Console.WriteLine(ByteArrayToString(bytes));

            MemoryStream stream = new MemoryStream();

            serializer.Pack(stream, obj);

            Console.WriteLine("Meta object has magic: {0}", obj.Magic);
            Send(stream.ToArray());
        }

        public void Send(byte[] data)
        {
            socket.BeginSend(data, 0, data.Length, 0, new AsyncCallback(SendCallback), socket);
        }

        private static void ReceiveCallback(IAsyncResult result)
        {
            ReadState state = (ReadState)result.AsyncState;

            MemoryStream stream = state.stream;
            BinaryWriter writer = state.writer;
            Socket client = state.socket;

            try
            {
                int bytesRead = client.EndReceive(result);

                // More data available
                if (bytesRead > 0)
                {
                    writer.Write(state.buffer);

                    client.BeginReceive(state.buffer, 0, ReadState.BufferSize, 0,
                        new AsyncCallback(ReceiveCallback), state);
                }
                else
                {
                    // deserialize data in stream to a messagepack object 
                    // then emit callback event
                    receiveDone.Set();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Could not receive from connection {0} [error: {1}]",
                    client.RemoteEndPoint.ToString(),
                    e.ToString());
            }
        }

        private static void SendCallback(IAsyncResult result)
        {
            try
            {
                // Retrieve the socket from the state object.  
                Socket client = (Socket)result.AsyncState;

                // Complete sending the data to the remote device.  
                int bytesSent = client.EndSend(result);
                Console.WriteLine("Sent {0} bytes to server.", bytesSent);

                // Signal that all bytes have been sent.  
                sendDone.Set();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
