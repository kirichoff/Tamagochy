using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TamagochyCsharpc
{
    class Stat
    {
        int count;
        string bar;

        public Stat(int val)
        {
            count = val;

            try
            {
                bar = new string('-', val);
            }
            catch
            {
                bar = new string('-', 1);
            }
        }

        public string print()
        {
            return bar;
        }

        public int val()
        {
            return count;
        }


        public void pop(int val)
        {
            if (count > 0 && count-val >= 0 )
            {
                count-=val;
                bar = bar.Remove(count);
            }
            else if (count - 1 >= 0)
            {
                count--;
                bar = bar.Remove(count);
            }
        }

        public void pusch(int val)
        {
            count+=val;
            string str = new string('-', val);
            bar +=str;
        }


    }
}
