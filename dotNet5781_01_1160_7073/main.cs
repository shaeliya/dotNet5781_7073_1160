using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
namespace dotNet5781_01_1160_7073
{
    class Program
    {

        static void Main(string[] args)
        {

            List<Bus> bussList = new List<Bus>();
            string liceseNumber = string.Empty;
            DateTime busStartDate = new DateTime();

            bool isValid = false;
            while (!isValid)
            {
                Console.WriteLine("Please enter the bus license number");
                liceseNumber = Console.ReadLine();
                bool isDateTime = false;
                while (!isDateTime)
                {
                    Console.WriteLine("Please enter the start date of the bus activity in fotmat dd/MM/yyyy");
                    string busStartDateStr = Console.ReadLine();
                    isDateTime = DateTime.TryParseExact(busStartDateStr, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out busStartDate);
                }
                isValid = InputValidityCheck(liceseNumber, busStartDate);
            }

            Bus b = new Bus(liceseNumber, busStartDate);
            bussList.Add(b);
        }

        private static bool InputValidityCheck(string liceseNumber, DateTime busStartDate)
        {
            //     liceseNumber = liceseNumber.Substring(0, 2) + "-" + liceseNumber.Substring(2, 3) + "-" + liceseNumber.Substring(5, 2);
            // liceseNumber = liceseNumber.Substring(0, 3) + "-" + liceseNumber.Substring(3, 2) + "-" + liceseNumber.Substring(5, 3);

            if (liceseNumber.Length < 7 || liceseNumber.Length>8)
            {
                Console.WriteLine("Number of digits incorrect. Please Enter data again");
                return false;
            }
            if (busStartDate.Year < 2018 && liceseNumber.Length != 7||
                busStartDate.Year >= 2018 && liceseNumber.Length != 8)
            {
                Console.WriteLine("Number of digits doesn't match date. Please Enter data again");
                return false;
            }
           
            return true;
        }
    }
}
