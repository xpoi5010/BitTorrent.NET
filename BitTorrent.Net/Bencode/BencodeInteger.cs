using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitTorrent.Net.Bencode
{
    public class BencodeInteger : IBencodeObject
    {
        public BencodeInteger(long BaseInteger)
        {
            this.baseInteger = BaseInteger;
        }

        public long baseInteger { get; set; }

        public BencodeType BencodeType => BencodeType.Integer;

        public static implicit operator BencodeInteger(byte Integer)
        {
            return new BencodeInteger(Integer);
        }

        public static implicit operator BencodeInteger(sbyte Integer)
        {
            return new BencodeInteger(Integer);
        }

        public static implicit operator BencodeInteger(short Integer)
        {
            return new BencodeInteger(Integer);
        }

        public static implicit operator BencodeInteger(ushort Integer)
        {
            return new BencodeInteger(Integer);
        }

        public static implicit operator BencodeInteger(int Integer)
        {
            return new BencodeInteger(Integer);
        }

        public static implicit operator BencodeInteger(uint Integer)
        {
            return new BencodeInteger(Integer);
        }

        public static implicit operator BencodeInteger(long Integer)
        {
            return new BencodeInteger(Integer);
        }

        public static implicit operator byte(BencodeInteger Integer)
        {
            return (byte)Integer.baseInteger;
        }

        public static implicit operator short(BencodeInteger Integer)
        {
            return (short)Integer.baseInteger;
        }

        public static implicit operator int(BencodeInteger Integer)
        {
            return (int)Integer.baseInteger;
        }

        public static implicit operator sbyte(BencodeInteger Integer)
        {
            return (sbyte)Integer.baseInteger;
        }

        public static implicit operator ushort(BencodeInteger Integer)
        {
            return (ushort)Integer.baseInteger;
        }

        public static implicit operator uint(BencodeInteger Integer)
        {
            return (uint)Integer.baseInteger;
        }

        public static implicit operator long(BencodeInteger Integer)
        {
            return (long)Integer.baseInteger;
        }

        public override string ToString()
        {
            return string.Format("i{0}e", this.baseInteger);
        }

        public byte[] GetBytes()
        {
            string integer = 'i' + Convert.ToString(baseInteger, 10) + 'e';
            return Encoding.ASCII.GetBytes(integer);
        }
    }
}
