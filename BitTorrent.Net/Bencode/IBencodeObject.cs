using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitTorrent.Net.Bencode
{
    public interface IBencodeObject
    {
        byte[] GetBytes();

        BencodeType BencodeType { get; }
    }

    public enum BencodeType
    {
        Bytes,Dictionary,Integer,List
    }
}
