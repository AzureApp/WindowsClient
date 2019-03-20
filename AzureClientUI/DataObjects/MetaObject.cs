using MsgPack.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AzureClientUI.Helpers;

namespace AzureClientUI.DataObjects
{
    public class MetaObject
    {
        [MessagePackMember(0)]
        public uint Magic { get; set; } = 0xABAD1DEA;
        [MessagePackMember(1)]
        public ObjectType Type { get; set; }
    }
}
