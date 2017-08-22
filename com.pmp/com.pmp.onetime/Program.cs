using CLAP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.pmp.onetime
{
    class Program
    {
        static void Main(string[] args)
        {
            var cmd = Console.ReadLine();

            while (true)
            {
                args = cmd.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                Parser.RunConsole<MyApp>(args);

                cmd = Console.ReadLine();
            }
        }
    }



    class MyApp
    {
        [Verb]
        public static void Foo(int count)
        {
            for (int i = 0; i < count; i++)
            {
                Console.WriteLine($"Hello this is the {i}");
            }
        }
    }
}
