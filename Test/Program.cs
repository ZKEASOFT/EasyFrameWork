using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Easy;
using Easy.IOCAdapter;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Type tt= typeof (string);
            Container.Register(typeof(iTT), typeof(TT));
             Loader.CreateInstance<iTT>().Write();
            Console.ReadKey();
        }
    }

    public interface iTT
    {
        void Write();
    }

    public class TT : iTT
    {
        public TT(char s)
        {
            
        }
        public void Write()
        {
            Console.Write("dd");
        }
    }
}
