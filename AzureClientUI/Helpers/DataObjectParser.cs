using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MsgPack.Serialization;
using AzureClientUI.DataObjects;

namespace AzureClientUI.Helpers
{
    // This enum has a specific naming scheme for each value
    // e.g an object of type "SomeObject" will have an enum name "Some"
    // this is to allow for reflection code to automatically create new objects
    // from an enum name
    public enum ObjectType
    {
        Meta = 0,
        Handshake,
        Search
    };

    class DataObjectHelper
    {
        private SerializationContext serializationContext;

        public DataObjectHelper()
        {
            this.serializationContext = new SerializationContext();
            this.serializationContext.EnumSerializationOptions.SerializationMethod =
                EnumSerializationMethod.ByUnderlyingValue;
        }

        public byte[] Pack<T>(T obj) where T : MetaObject
        {
            var serializer = MessagePackSerializer.Get<T>(serializationContext);
            return serializer.PackSingleObject(obj);
        }

        public T Unpack<T>(byte[] bytes) where T : MetaObject
        {
            var serializer = MessagePackSerializer.Get<MetaObject>(serializationContext);

            var tempObject = serializer.UnpackSingleObject(bytes);
            var type = TypeForEnum(tempObject.Type);

            var objectSerializer = MessagePackSerializer.Get(type, serializationContext);

            return (T)objectSerializer.UnpackSingleObject(bytes);
        }

        private Type TypeForEnum(ObjectType objectType)
        {
            var metaType = typeof(MetaObject);
            string name = metaType.Namespace + "." + objectType.ToString() + "Object";

            Type type = Type.GetType(name);

            return type;
        }
    }
}
