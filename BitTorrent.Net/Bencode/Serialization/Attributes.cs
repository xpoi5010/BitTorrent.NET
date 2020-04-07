using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitTorrent.Net.Bencode.Serialization
{
    public class BencodeNameAttribute: Attribute
    {
        public string Name { get; set; }
        public BencodeNameAttribute(string Name)
        {
            this.Name = Name;
        }
    }

    public class BencodeStringEncodingAttribute : Attribute
    {
        public Encoding Encoding { get; set; }

        public BencodeStringEncodingAttribute(Encoding encoding)
        {
            Encoding = encoding;
        }
    }
}
