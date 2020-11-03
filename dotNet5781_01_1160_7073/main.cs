//לבדוק בשלוש מה קורה כשמכניסים מספר רישוי לא תקין
// באחת אם אני מכניסה ברישוי מספר ואות הוא כותב שהמשפט לא נכון ואז הוא כותב שהוא הכניס את זה למערכת 
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
        static Random randKilometrage = new Random(DateTime.Now.Millisecond);
        static void Main(string[] args)
        {
            List<Bus> busList = new List<Bus>();
            string liceseNumber = string.Empty;
            DateTime busStartDate = new DateTime();
            bool IsBusExists = false;
            bool isBusExists = false;
            bool isBusFound = false;
            string choose =string.Empty;
            while (choose != "5") 
            {
                Console.WriteLine("Please, choose one of the following:");
                Console.WriteLine("1: Introducing a bus to the list of buses in the company.");
                Console.WriteLine("2: Choosing a bus to travel.");
                Console.WriteLine("3: Refueling or handling a bus.");
                Console.WriteLine("4: Presentation of the passenger since the last treatment for all vehicles in the company.");
                Console.WriteLine("5: Exit.");
                choose = Console.ReadLine();
                switch (choose)
                {
                    case "1":

                        bool isValid = false;
                        while (!isValid)
                        {
                            liceseNumber = inputLicenseNumber();
                            if (liceseNumber == "The number is incorrect")
                            {
                                Console.WriteLine("The number is incorrect");
                                break;
                            }

                            foreach (Bus bus in busList)
                            {
                                if (bus.LicenseNumber == liceseNumber)
                                {
                                    Console.WriteLine("The license number exists in the system");
                                    IsBusExists = true;
                                }
                            }
                            if (IsBusExists == true)
                            {
                                break;
                            }
                            bool isDateTime = false;
                            while (!isDateTime)
                            {
                                Console.WriteLine("Please enter the start date of the bus activity in fotmat dd/MM/yyyy");
                                string busStartDateStr = Console.ReadLine();
                                isDateTime = DateTime.TryParseExact(busStartDateStr, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out busStartDate);
                            }
                            isValid = InputValidityCheck(liceseNumber, busStartDate);

                        }
                        if (IsBusExists == true)
                        {
                            break;
                        }
                        if (isValid)
                        {                        
                            Bus b = new Bus(liceseNumber, busStartDate);
                            busList.Add(b);
                            Console.WriteLine("The bus was added to the system");
                        }
                        break;
                    case "2":
                        isBusExists = false;
                        liceseNumber = inputLicenseNumber();
                        if (liceseNumber == "The number is incorrect")
                        {
                            Console.WriteLine("The number is incorrect");
                            break;
                        }
                        bool isEmpty = !busList.Any();
                        if (isEmpty)
                        {
                            Console.WriteLine("The bus does not exist in the system");
                            break;
                        }

                        
                        foreach (Bus bus in busList)
                        {
                          
                            isBusFound = bus.IsBusFound(liceseNumber);
                            if (isBusFound)
                            {
                                isBusExists = true;
                                double KilometrageForRide = randKilometrage.NextDouble() * (1200.0 - 0.0) + 0.0;
                                bool isProperBusForTravel = bus.IsProperBusForTravel(liceseNumber, KilometrageForRide,  isBusFound);           
                                if (isProperBusForTravel)
                                {
                                   
                                    Console.WriteLine("The bus is ready for travel");
                                    bus.Kilometrage += KilometrageForRide;
                                    bus.Fuel += KilometrageForRide;
                                    bus.Treatment += KilometrageForRide;
                                }
                            }
                        }                     
                          
                        if (!isBusExists)
                        {
                            Console.WriteLine("No bus found, with the license number you entered");
                        }

                        break;
                    case "3":
                        isBusExists = false;
                        liceseNumber = inputLicenseNumber();                         
                        foreach (Bus bus in busList) 
                        {
                             isBusFound = bus.IsBusFound(liceseNumber);
                            if (isBusFound)
                            {
                                isBusExists = true;
                                Console.WriteLine("Press 'T' for treatment or 'R' to refuel");
                                string ch = Console.ReadLine();
                                if (ch == "T" || ch == "t")
                                {
                                    bus.Treatment = 0;
                                    bus.BusStartDate = DateTime.Now;

                                }
                                else if (ch == "R" || ch == "r")
                                {
                                    bus.Fuel = 0;
                                }
                                else
                                {
                                    Console.WriteLine("Your choice is wrong!");
                                    break;
                                }
                            }
                        }
                        if (!isBusExists)
                        {
                            Console.WriteLine("No bus found, with the license number you entered");
                        }
                        break;
                    case "4":
                        foreach (Bus bus in busList)
                        {
                            string temp = bus.Print(bus.LicenseNumber);
                            Console.WriteLine("The license number is:" + temp);                          
                            Console.WriteLine("The kilometrage is:" + bus.Kilometrage);
                        }
                        break;
                    case "5":

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
            bool isDigitsOnly = IsDigitsOnly(liceseNumber);
            if (isDigitsOnly)
            {
                return liceseNumber;
            }
            string temp = "The number is incorrect";
            return temp;
        }

        private static bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }
            return true;
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


