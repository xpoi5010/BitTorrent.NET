using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitTorrent.Net.Torrent
{
    public class TorrentFile
    {
        public string Name { get; set; }

        public TorrentFile()
        {
            Name = "test";
        }

        public void ReadFromFile()
        {

        }
        ~TorrentFile()
        {
            Name = "test";
        }
    }
}
