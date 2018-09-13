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
        Search
    };

    public class MetaObject
    {
        public uint Magic { get; set; } = 0xABAD1DEA;
        public ObjectType Type { get; set; }
    }
}
