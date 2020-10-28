using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_01_1160_7073
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Bus> bussList = new List<Bus>();
            Console.WriteLine("Please enter the bus license number");
            string liceseNumber=Console.ReadLine();
            Console.WriteLine("Please enter the start date of the bus activity");
            string busStartDate = Console.ReadLine();
            Bus b = new Bus(liceseNumber, busStartDate);
            bussList.Add(b);
        }
    }
}
