using MsgPack.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureClientUI.DataObjects
{
    public enum OperatingSystem
    {
        Windows,
        iOS,
        macOS,
        Android,
        Linux
    }

    public class HandshakeObject : MetaObject
    {
        [MessagePackMember(2)]
        public string ACPVersion { get; set; } = "1.0.0";
        [MessagePackMember(3)]
        public OperatingSystem System { get; set; } = OperatingSystem.Windows;

        public HandshakeObject()
        {
            Type = ObjectType.Handshake;
        }
    }
}
