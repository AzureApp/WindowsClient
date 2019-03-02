using MsgPack.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureClientUI
{
    public enum ObjectType
    {
        Meta = 0,
        Handshake,
        Search
    };

    public class MetaObject
    {
        [MessagePackMember(0)]
        public uint Magic { get; set; } = 0xABAD1DEA;
        [MessagePackMember(1)]
        public ObjectType Type { get; set; }
    }
}
