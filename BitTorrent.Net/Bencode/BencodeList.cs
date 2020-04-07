using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitTorrent.Net.Bencode
{
    public class BencodeList : ICollection<IBencodeObject>, IBencodeObject
    {
        private List<IBencodeObject> baseList = new List<IBencodeObject>();

        public int Count => baseList.Count;

        public bool IsReadOnly => false;

        public BencodeType BencodeType => BencodeType.List;

        public void Add(IBencodeObject item)
        {
            baseList.Add(item);
        }

        public void AddRange(IBencodeObject[] item)
        {
            baseList.AddRange(item);
        }

        public void Clear()
        {
            baseList.Clear();
        }

        public bool Contains(IBencodeObject item)
        {
            return baseList.Contains(item);
        }

        public void CopyTo(IBencodeObject[] array, int arrayIndex)
        {
            baseList.CopyTo(array, arrayIndex);
        }

        public byte[] GetBytes()
        {
            List<byte> output = new List<byte>();
            output.Add(0x6C);
            foreach(IBencodeObject item in this)
            {
                output.AddRange(item.GetBytes());
            }
            output.Add(0x65);
            return output.ToArray();
        }

        public IEnumerator<IBencodeObject> GetEnumerator()
        {
            return baseList.GetEnumerator();
        }

        public bool Remove(IBencodeObject item)
        {
            return baseList.Remove(item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return baseList.GetEnumerator();
        }
    }
}
