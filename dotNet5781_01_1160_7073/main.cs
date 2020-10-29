
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
            List<Bus> busList = new List<Bus>();
            bool isValid = false;
            string liceseNumber = string.Empty;
            DateTime busStartDate = new DateTime();

            Console.WriteLine("Please, choose one of the following:");
            Console.WriteLine("A: Introducing a bus to the list of buses in the company.");
            Console.WriteLine("B: Choosing a bus to travel.");
            Console.WriteLine("C: Refueling or handling a bus.");
            Console.WriteLine("D: Presentation of the passenger since the last treatment for all vehicles in the company.");
            Console.WriteLine("E: Exit.");
            string choose = Console.ReadLine();
            while (choose != "E")
            {
                switch (choose)
                {
                    case "A":
                        while (!isValid)
                        {
                            liceseNumber = inputLicenseNumber();
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
                        busList.Add(b);
                        break;
                    case "B":
                        liceseNumber = inputLicenseNumber();
                        foreach (Bus bus in busList)
                        {
                            Random randKilometrage = new Random(DateTime.Now.Millisecond);
                            double KilometrageForRide;
                            bool isKilometrage = double.TryParse(randKilometrage.ToString(), out KilometrageForRide);
                            bool isProperBusForTravel = bus.IsProperBusForTravel(liceseNumber, KilometrageForRide);
                            if (isProperBusForTravel)
                            {
                                bus.Kilometrage += KilometrageForRide;
                                bus.Fuel += KilometrageForRide;
                            }

                        }

                        break;
                    case "C":
                        break;
                    case "D":
                        break;
                    case "E":

                    default:
                        Console.WriteLine("ERROR");
                        break;
                }
            }

            
        }

        private static string inputLicenseNumber()
        {
            string liceseNumber;
            Console.WriteLine("Please enter the bus license number");
            liceseNumber = Console.ReadLine();
            return liceseNumber;
        }

        private static bool InputValidityCheck(string liceseNumber, DateTime busStartDate)
        {
            if (liceseNumber.Length < 7 || liceseNumber.Length > 8)
            {
                Console.WriteLine("Number of digits incorrect. Please Enter data again");
                return false;
            }
            if (busStartDate.Year < 2018 && liceseNumber.Length != 7 ||
                busStartDate.Year >= 2018 && liceseNumber.Length != 8)
            {
                Console.WriteLine("Number of digits doesn't match date. Please Enter data again");
                return false;
            }

            return true;
        }
    }
}


