using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitTorrent.Net.Bencode
{
    public class BencodeBytes : IEnumerable<byte>, IBencodeObject
    {
        public BencodeBytes(byte[] bytes)
        {
            BaseBytes = bytes;
        }

        public byte[] BaseBytes { get; set; }

        public BencodeType BencodeType => BencodeType.Bytes;

        public IEnumerator<byte> GetEnumerator()
        {
            return new BencodeBytesEnumerator(BaseBytes);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new BencodeBytesEnumerator(BaseBytes);
        }

        public byte[] GetBytes()
        {
            string Length = Convert.ToString(BaseBytes.Length, 10) + ':';
            byte[] lengthBytes = Encoding.ASCII.GetBytes(Length);
            byte[] outputBytes = new byte[BaseBytes.Length + lengthBytes.Length];
            Array.Copy(lengthBytes, 0, outputBytes, 0, lengthBytes.Length);
            Array.Copy(BaseBytes, 0, outputBytes, lengthBytes.Length, BaseBytes.Length);
            return outputBytes;
        }

        public override string ToString()
        {
             return BaseBytes.Length>=262144?typeof(Byte).ToString(): Encoding.UTF8.GetString(BaseBytes);
        }

        public static implicit operator BencodeBytes(string strings)
        {
            return new BencodeBytes(Encoding.UTF8.GetBytes(strings));
        }

        public static implicit operator BencodeBytes(byte[] bytes)
        {
            return new BencodeBytes(bytes);
        }

        public static implicit operator byte[](BencodeBytes bytes)
        {
            return bytes.BaseBytes;
        }

        public static explicit operator string(BencodeBytes bytes)
        {

            return Encoding.UTF8.GetString(bytes.BaseBytes);
        }
    }

    public class BencodeBytesEnumerator : IEnumerator<byte>
    {
        private byte[] baseArray = new byte[0];

        private int position = -1;

        public byte Current => baseArray[position];

        object IEnumerator.Current => baseArray[position];

        public BencodeBytesEnumerator(byte[] bytes)
        {
            baseArray = bytes;
        }

        public void Dispose()
        {
            baseArray = new byte[0];
        }

        public bool MoveNext()
        {
            position++;
            return (position < baseArray.Length);
        }

        public void Reset()
        {
            position = -1;
        }
    }
}
