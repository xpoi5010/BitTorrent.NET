using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace BitTorrent.Net.Bencode.Serialization
{
    public class BencodeSerializationer
    {
        public BencodeSerializationer()
        {
            
            
        }

        public T CreateValue<T>()
        {
            Type type = typeof(T);
            return (T)CreateValue(type);
        }

        public object CreateValue(Type type)
        {
            ConstructorInfo ci = type.GetConstructor(new Type[0]);
            if (ci is null)
                throw new Exception($"Type:{ci.Name} can't find the public initization function with empty argument.");
            object newType = ci.Invoke(new object[0]);
            return newType;
        }

        public void SetValue(ref object value)
        {
            
        }
    }
}
