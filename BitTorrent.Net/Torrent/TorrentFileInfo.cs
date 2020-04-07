using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BitTorrent.Net.Torrent
{
    public class TorrentFileInfo
    {
        public string Name { get; set; }

        public long Length { get; set; }

        public long StartBlock { get; set; }

        private FileMode Mode { get; set; }

        private object Location { get; set; }

        public long PieceCount(int PicecLength)
        {
            long a = 0;
            long b = Math.DivRem(Length, PicecLength, out a);
            if (a != 0)
                b++;
            return b;
        }

        public void ReadFromFile(string Path)
        {
            if (!File.Exists(Path))
                throw new FileNotFoundException($"File[{Path}] Not Fount");
            Location = Path;
            Mode = FileMode.Disk;
        }

        public void ReadFromTorrent(string Name,long BlockStart,long Length)
        {
            Mode = FileMode.Torrent;
            this.Name = Name;
            StartBlock = BlockStart;
            this.Length = Length;
        }

    }

    public enum FileMode
    {
        Memory,Disk,Torrent
    }
}
