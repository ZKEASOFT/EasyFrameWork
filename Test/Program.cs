using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Easy;
using Easy.Extend;
using Easy.IOCAdapter;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {

            

            Loader.ResolveAll<IOOt>().Each(m=>m.Wirte());


            Loader.CreateInstance<IOOt>().Wirte();
        }
    }


    public interface IOOt
    {
        void Wirte();
    }

    public class OOt : IOOt
    {
        public void Wirte()
        {
            Console.WriteLine("dddd");
        }
    }


}
