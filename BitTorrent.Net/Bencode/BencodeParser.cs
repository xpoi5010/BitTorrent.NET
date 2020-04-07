using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitTorrent.Net.Bencode
{
    public class BencodeParser
    {
        public IBencodeObject ParseString(byte[] Content)
        {
            RStack<IBencodeObject> outputStack = new RStack<IBencodeObject>();
            Stack<BencodeCommand> commandStack = new Stack<BencodeCommand>();
            commandStack.Push(BencodeCommand.None);
            byte[] bytes = Content;
            long numberTemp = 0;//
            List<byte> tempByteArr = new List<byte>();
            foreach (byte bt in bytes)
            {
                switch (commandStack.NowValue)
                {
                    case BencodeCommand.None:
                        if (bt == 0x69)//I
                        {
                            numberTemp = 0;
                            commandStack.Push(BencodeCommand.Integer);
                            continue;
                        }
                        else if (bt == 0x64)//D
                        {
                            commandStack.Push(BencodeCommand.Dictionary);
                            outputStack.SetReversePopStart();
                            continue;
                        }
                        else if (bt == 0x6C)//L
                        {
                            commandStack.Push(BencodeCommand.List);
                            outputStack.SetReversePopStart();
                            continue;
                        }
                        else if (bt >= 0x30 && bt <= 0x39)//S
                        {
                            commandStack.Push(BencodeCommand.StringBytesLength);
                            numberTemp = 0;
                            int firstNum = bt ^ 0x30;
                            numberTemp += firstNum;
                            tempByteArr.Clear();
                            continue;
                        }
                        else
                        {
                            throw new Exception("Format wrong.");
                        }
                    case BencodeCommand.Integer:
                        if (bt == 0x65)
                        {
                            commandStack.Pop();
                            BencodeInteger integer = new BencodeInteger(numberTemp);
                            outputStack.Push(integer);

                            continue;
                        }
                        else
                        {
                            numberTemp *= 10;
                            int Num = bt ^ 0x30;
                            numberTemp += Num;
                            continue;
                        }
                    case BencodeCommand.StringBytesLength:
                        if (bt == 0x3A)
                        {
                            commandStack.Pop();
                            commandStack.Push(BencodeCommand.StringBytesContent);
                            continue;
                        }
                        else
                        {
                            numberTemp *= 10;
                            int Num = bt ^ 0x30;
                            numberTemp += Num;
                            continue;
                        }
                    case BencodeCommand.StringBytesContent:
                        tempByteArr.Add(bt);
                        numberTemp--;
                        if (numberTemp == 0)
                        {
                            byte[] tempBytes = tempByteArr.ToArray();
                            BencodeBytes BBytes = new BencodeBytes(tempBytes);
                            outputStack.Push(BBytes);
                            commandStack.Pop();
                        }
                        continue;
                    case BencodeCommand.Dictionary:
                        if (bt == 0x65)
                        {
                            IBencodeObject[] dictItems = outputStack.ReversePop();
                            if ((dictItems.Length & 1) != 0)
                                throw new Exception("DICT ERR!!!");
                            int length = dictItems.Length >> 1;
                            BencodeDictionary dict = new BencodeDictionary();
                            for (int i = 0; i < length; i++)
                            {
                                BencodeBytes key = (BencodeBytes)dictItems[(i << 1)];
                                IBencodeObject content = dictItems[(i << 1) + 1];
                                dict.Add(Encoding.UTF8.GetString(key.ToArray()), content);
                            }
                            outputStack.Push(dict);
                            commandStack.Pop();
                            continue;
                        }
                        goto case BencodeCommand.None;
                    case BencodeCommand.List:
                        if (bt == 0x65)
                        {
                            IBencodeObject[] dictItems = outputStack.ReversePop();
                            BencodeList bl = new BencodeList();
                            bl.AddRange(dictItems);
                            outputStack.Push(bl);
                            commandStack.Pop();
                            continue;
                        }
                        goto case BencodeCommand.None;

                }
            }
            if (outputStack.StackCount != 1)
                throw new Exception("Exception::the parser has arrived the end of data stream. 例外狀況::剖析器已達檔案流結尾。");
            return outputStack.GetValue();
        }

        public IBencodeObject ParseStringTest(byte[] Content)
        {
            RStack<IBencodeObject> outputStack = new RStack<IBencodeObject>();
            Stack<BencodeCommand> commandStack = new Stack<BencodeCommand>();
            commandStack.Push(BencodeCommand.None);
            byte[] bytes = Content;
            long numberTemp = 0;//
            List<byte> tempByteArr = new List<byte>();
            foreach (byte bt in bytes)
            {
                switch (commandStack.NowValue)
                {
                    case BencodeCommand.None:
                        if (bt == 0x69)//I
                        {
                            numberTemp = 0;
                            commandStack.Push(BencodeCommand.Integer);
                            continue;
                        }
                        else if (bt == 0x64)//D
                        {
                            commandStack.Push(BencodeCommand.Dictionary);
                            outputStack.SetReversePopStart();
                            continue;
                        }
                        else if (bt == 0x6C)//L
                        {
                            commandStack.Push(BencodeCommand.List);
                            outputStack.SetReversePopStart();
                            continue;
                        }
                        else if (bt >= 0x30 && bt <= 0x39)//S
                        {
                            commandStack.Push(BencodeCommand.StringBytesLength);
                            numberTemp = 0;
                            int firstNum = bt ^ 0x30;
                            numberTemp += firstNum;
                            tempByteArr.Clear();
                            continue;
                        }
                        else
                        {
                            throw new Exception("Format wrong.");
                        }
                    case BencodeCommand.Integer:
                        if (bt == 0x65)
                        {
                            commandStack.Pop();
                            BencodeInteger integer = new BencodeInteger(numberTemp);
                            outputStack.Push(integer);

                            continue;
                        }
                        else
                        {
                            numberTemp *= 10;
                            int Num = bt ^ 0x30;
                            numberTemp += Num;
                            continue;
                        }
                    case BencodeCommand.StringBytesLength:
                        if (bt == 0x3A)
                        {
                            commandStack.Pop();
                            commandStack.Push(BencodeCommand.StringBytesContent);
                            continue;
                        }
                        else
                        {
                            numberTemp *= 10;
                            int Num = bt ^ 0x30;
                            numberTemp += Num;
                            continue;
                        }
                    case BencodeCommand.StringBytesContent:
                        tempByteArr.Add(bt);
                        numberTemp--;
                        if (numberTemp == 0)
                        {
                            byte[] tempBytes = tempByteArr.ToArray();
                            BencodeBytes BBytes = new BencodeBytes(tempBytes);
                            outputStack.Push(BBytes);
                            commandStack.Pop();
                        }
                        continue;
                    case BencodeCommand.Dictionary:
                        if (bt == 0x65)
                        {
                            IBencodeObject[] dictItems = outputStack.ReversePop();
                            if ((dictItems.Length & 1) != 0)
                               throw new Exception("DICT ERR!!!");
                            int length = dictItems.Length >> 1;
                            BencodeDictionary dict = new BencodeDictionary();
                            /*
                            for (int i = 0; i < length; i++)
                            {
                                BencodeBytes key = (BencodeBytes)dictItems[(i << 1)];
                                IBencodeObject content = dictItems[(i << 1) + 1];
                                dict.Add(Encoding.UTF8.GetString(key.ToArray()), content);
                            }
                            */
                            outputStack.Push(dict);
                            commandStack.Pop();
                            continue;
                        }
                        goto case BencodeCommand.None;
                    case BencodeCommand.List:
                        if (bt == 0x65)
                        {
                            IBencodeObject[] dictItems = outputStack.ReversePop();
                            BencodeList bl = new BencodeList();
                            //bl.AddRange(dictItems);
                            outputStack.Push(bl);
                            commandStack.Pop();
                            continue;
                        }
                        goto case BencodeCommand.None;

                }
            }
            if (outputStack.StackCount != 1)
                throw new Exception("Exception::the parser is arrive the end of data. 例外狀況::剖析器已達檔案結尾。");
            return outputStack.GetValue();
        }
    }

    public enum BencodeCommand
    {
        None = -1,
        Integer = 0,
        StringBytesLength = 1,
        List = 2,
        Dictionary=3,
        StringBytesContent = 4,
    }
}
