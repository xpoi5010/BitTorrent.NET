using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using BitTorrent.Net;
using System.Diagnostics;
using BitTorrent.Net.Bencode.Serialization;
using BitTorrent.Net.Bencode;
using BitTorrent.Net.Torrent;

namespace Bittorrent.Demo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            BitTorrent.Net.Bencode.BencodeParser Bencode = new BitTorrent.Net.Bencode.BencodeParser();
            FileStream fs = new FileStream(@"D:\Untitled2.torrent", System.IO.FileMode.Open);
            byte[] data = new byte[fs.Length];
            fs.Read(data, 0, data.Length);
            DateTime dt = DateTime.Now;
            BitTorrent.Net.Bencode.IBencodeObject f = Bencode.ParseString(data);
            TimeSpan ts = DateTime.Now - dt;
            Debug.Print(ts.TotalMilliseconds.ToString());
            fs.Close();
            GC.Collect();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            BencodeSerializationer serializationer = new BencodeSerializationer();
        }
    }
}
