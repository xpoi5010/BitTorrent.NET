using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitTorrent.Net.Torrent.Base
{
    public struct Torrent
    {
        
    }
   
    public struct Info
    {
        public long PieceLength ;

        public byte[] Pieces ;

        public bool Private ;

        public string Name ;

        public long Length ;

        public string MD5SUM ;

        public File[] Files;
    }

    public struct File
    {
        public long Length ;

        public string MD5SUM ;

        public string[] Path ;
    }
}
