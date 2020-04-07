using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitTorrent.Net
{
    public class Stack<T>
    {
        private T[] items = new T[1024];

        public int StackCount => (Position+1);


        private int Position = -1;

        public T NowValue
        {
            get
            {
                return items[Position];
            }
            set
            {
                items[Position] = value;
            }
        }
        public T GetValue()
        {
            return items[Position];
        }

        public bool IsIgore()
        {
            return items.Length == 0;
        }

        public void Push(T value)
        {
            Position++;
            if(Position == items.Length)
                Array.Resize(ref items, items.Length + 1);
            items[Position] = value;
        }

        public T Pop()
        {
            T output = items[Position];
            Position--;
            return output;
        }
    }
}