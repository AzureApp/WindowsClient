using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using MsgPack.Serialization;
using AzureClientUI.DataObjects;

namespace AzureClientUI
{
    class ClientConnection
    {
        private TcpClient client;

        public ClientConnection(TcpClient client)
        {
            this.client = client;
        }

        public static string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }

        public void Run()
        {
            NetworkStream stream = client.GetStream();

            HandshakeObject obj = new HandshakeObject();

            var context = new SerializationContext();
            context.EnumSerializationOptions.SerializationMethod = EnumSerializationMethod.ByUnderlyingValue;

            var serializer = MessagePackSerializer.Get<HandshakeObject>(context);

            var bytes = serializer.PackSingleObject(obj);
            Console.WriteLine(ByteArrayToString(bytes));

            serializer.Pack(stream, obj);

            Console.WriteLine("Meta object has magic: {0}", obj.Magic);
        }
    }
}
