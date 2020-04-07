using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitTorrent.Net.Bencode
{
    public class BencodeDictionary : IBencodeObject, IDictionary<string, IBencodeObject>
    {
        private Dictionary<string, IBencodeObject> baseDictionarys = new Dictionary<string, IBencodeObject>();

        public IBencodeObject this[string key] { get => baseDictionarys[key]; set => baseDictionarys[key]=value; }

        public ICollection<string> Keys => baseDictionarys.Keys;

        public ICollection<IBencodeObject> Values => baseDictionarys.Values;

        public int Count => baseDictionarys.Count;

        public bool IsReadOnly => false;

        public BencodeType BencodeType => BencodeType.Dictionary;

        public void Add(string key, IBencodeObject value)
        {
            baseDictionarys.Add(key, value);
        }

        public void Add(KeyValuePair<string, IBencodeObject> item)
        {
            baseDictionarys.Add(item.Key, item.Value);
        }


        public void Clear()
        {
            baseDictionarys.Clear();
        }

        public bool Contains(KeyValuePair<string, IBencodeObject> item)
        {
            return baseDictionarys.Contains(item);
        }

        public bool ContainsKey(string key)
        {
            return baseDictionarys.ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<string, IBencodeObject>[] array, int arrayIndex)
        {
            baseDictionarys.ToArray().CopyTo(array, arrayIndex);
        }

        public byte[] GetBytes()
        {
            List<byte> output = new List<byte>();
            output.Add(0x64);
            foreach(KeyValuePair<string,IBencodeObject> item in this)
            {
                BencodeBytes bb = new BencodeBytes(Encoding.UTF8.GetBytes(item.Key));
                IBencodeObject ibo = item.Value;
                output.AddRange(bb.GetBytes());
                output.AddRange(ibo.GetBytes());
            }
            output.Add(0x65);
            return output.ToArray();
        }

        public IEnumerator<KeyValuePair<string, IBencodeObject>> GetEnumerator()
        {
            return baseDictionarys.GetEnumerator();
        }

        public bool Remove(string key)
        {
            return baseDictionarys.Remove(key);
        }

        public bool Remove(KeyValuePair<string, IBencodeObject> item)
        {
            return baseDictionarys.Remove(item.Key);
        }

        public bool TryGetValue(string key, out IBencodeObject value)
        {
            return baseDictionarys.TryGetValue(key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return baseDictionarys.GetEnumerator();
        }
    }
}
