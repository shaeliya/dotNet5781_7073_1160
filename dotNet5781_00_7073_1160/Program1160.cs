using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_00_7073_1160
{
   partial class Program
    {
        static void Main(string[] args)
        {
            Wellcome1160();
            Wellcome7073();

            Console.ReadKey();
        }
        static partial void Wellcome7073();
        private static void Wellcome1160()
        {
            Console.Write("Enter your name: ");
            string name = Console.ReadLine();
            Console.WriteLine("{0}, welcome to my first console application", name);
        }
    }
}
//We did it!