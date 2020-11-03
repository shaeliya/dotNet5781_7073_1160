//Shalhevet Eliyahu 211661160
//Orit Stavsky 212507073
using System;
using System.Collections.Generic;
using System.Linq;
namespace dotNet5781_01_1160_7073
{
    class Program
    {
        static Random randKilometrage = new Random(DateTime.Now.Millisecond);       
        
        
        static void Main(string[] args)
        {
            List<Bus> busList = new List<Bus>();
            string choose = string.Empty;
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
                        IntroducingBusToTheSystem( busList);
                        break;
                    case "2":
                        ChoosingBusToTravel(busList);                       
                            break;
                    case "3":
                        RefulingAndHandlingBus(busList);
                        break;
                    case "4":
                        PrintsTheInformationOnTheBuses(busList);
                        break;
                    case "5":
                        break;
                    default:
                        Console.WriteLine("ERROR");
                        break;
                }
            }



        }

        private static void PrintsTheInformationOnTheBuses(List<Bus> busList)//case 4
        {
            foreach (Bus bus in busList)
            {
                string temp = bus.PreparingLicenseNumberForPrint(bus.LicenseNumber);
                Console.WriteLine("The license number is:" + temp);
                Console.WriteLine("The kilometrage is:" + bus.Kilometrage);
            }
        }

        private static void RefulingAndHandlingBus(List<Bus> busList)//case3
        {
            
            string liceseNumber ;    
            bool IsBusExists = false;
            liceseNumber = inputLicenseNumber();
            foreach (Bus bus in busList)
            {
                bool isBusFound = bus.IsBusFound(liceseNumber);
                if (isBusFound)
                {
                    IsBusExists = true;
                    Console.WriteLine("Press 'T' for treatment or 'R' to refuel");
                    string ch = Console.ReadLine();
                    if (ch == "T" || ch == "t")
                    {
                        bus.Treatment = 0;
                        bus.BusStartDate = DateTime.Now;
                        Console.WriteLine("The treatment was successful");

                    }
                    else if (ch == "R" || ch == "r")
                    {
                        bus.Fuel = 0;
                        Console.WriteLine("The refueling was successful");
                    }
                    else
                    {
                        Console.WriteLine("Your choice is wrong!");
                        break;
                    }
                }

            }
            if (!IsBusExists)
            {
                Console.WriteLine("No bus found, with the license number you entered");
            }
        }

        private static void IntroducingBusToTheSystem(List<Bus> busList)//case 1
        {
            bool isValid = false;
            string liceseNumber = string.Empty;
            DateTime busStartDate = new DateTime();
            double kilometrage = 0.0;
            double fuel = 0.0;
            double treatment = 0.0;
            string answer;
            
            DateTime lastTreatmentDate = new DateTime();
            
            bool IsBusExists = false;
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

            if (isValid)
            {

                Console.WriteLine("If you want to add how many miles the bus has traveled Press Y otherwise press any key");
                answer = Console.ReadLine();
                if (answer == "Y" || answer == "y")
                {
                    bool temp = false;
                    while (!temp)
                    {
                        Console.WriteLine("How many miles the bus has traveled?");
                        string kilometrageStr = Console.ReadLine();
                        temp = double.TryParse(kilometrageStr, out kilometrage);
                        if (kilometrage < 0)
                        {
                            Console.WriteLine("The number of miles is incorrect, please try again");
                            temp = false;
                        }

                    }
                }
                Console.WriteLine("If you want to add  how many miles the bus has traveled since the last refueling, Press Y otherwise press any key");
                answer = Console.ReadLine();
                if (answer == "Y" || answer == "y")
                {
                    bool temp = false;
                    while (!temp)
                    {

                        Console.WriteLine("How many miles the bus has traveled since the last refueling?");
                        string kilometrageSinceTheLastRefueling = Console.ReadLine();
                        temp = double.TryParse(kilometrageSinceTheLastRefueling, out fuel);
                        if (fuel > 1200 || fuel > kilometrage)
                        {
                            Console.WriteLine("The number of miles is incorrect, please try again");
                            temp = false;
                        }

                    }
                }

                Console.WriteLine("If you want to add how many miles the bus has traveled since the last refueling, Press Y otherwise press any key");
                answer = Console.ReadLine();
                if (answer == "Y" || answer == "y")
                {

                    bool temp = false;
                    while (!temp)
                    {
                        Console.WriteLine("How many miles the bus has traveled since the last treatment?");
                        string kilometrageSinceTheLastTreatment = Console.ReadLine();
                        temp = double.TryParse(kilometrageSinceTheLastTreatment, out treatment);
                        if (treatment > 20000 || treatment > kilometrage)
                        {
                            Console.WriteLine("The number of miles is incorrect, please try again");
                            temp = false;
                        }
                    }

                }
                Console.WriteLine("If you want to add a last treatment date, Press Y otherwise press any key");
                answer = Console.ReadLine();
                if (answer == "Y" || answer == "y")
                {

                    bool isDateTime = false;
                    while (!isDateTime)
                    {
                        Console.WriteLine("What was the last treatment date?");

                        Console.WriteLine("Please enter the date of the last treatment in fotmat dd/MM/yyyy");
                        string lastTreatmentDateStr = Console.ReadLine();
                        isDateTime = DateTime.TryParseExact(lastTreatmentDateStr, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out lastTreatmentDate);
                        if (lastTreatmentDate < busStartDate || lastTreatmentDate > DateTime.Now)
                        {
                            Console.WriteLine("The date is incorrect, please try again");
                            isDateTime = false;
                        }
                    }
                }
                else
                {
                    lastTreatmentDate = busStartDate;
                }
                Bus b = new Bus(liceseNumber, busStartDate, kilometrage, fuel, treatment, lastTreatmentDate);
                busList.Add(b);
                Console.WriteLine("The bus was added to the system");

              
            }
        } 
        private static void ChoosingBusToTravel(List<Bus> busList)//case2
        {            
             string liceseNumber;            
             bool IsBusExists= false;           
            liceseNumber = inputLicenseNumber();
            if (liceseNumber == "The number is incorrect")
            {
                Console.WriteLine("The number is incorrect");
                return;
            }
            bool isEmpty = !busList.Any();
            if (isEmpty)
            {
                Console.WriteLine("The bus does not exist in the system");
                return;
            }

            foreach (Bus bus in busList)
            {
                bool isBusFound = bus.IsBusFound(liceseNumber);
                if (isBusFound)
                {
                    IsBusExists = true;
                    double KilometrageForRide = randKilometrage.NextDouble() * (1200.0 - 0.0) + 0.0;
                    bool isProperBusForTravel = bus.IsProperBusForTravel(liceseNumber, KilometrageForRide);
                    if (isProperBusForTravel)
                    {
                        Console.WriteLine("The bus is ready for travel");
                        bus.Kilometrage += KilometrageForRide;
                        bus.Fuel += KilometrageForRide;
                        bus.Treatment += KilometrageForRide;
                    }
                }


            }
            if (!IsBusExists)
            {
                Console.WriteLine("No bus found, with the license number you entered");
            }
        }
        private static string inputLicenseNumber()// Receiving a license number
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

        private static bool IsDigitsOnly(string str)//Checks if the license number is just digits
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }
            return true;
        }
        private static bool InputValidityCheck(string liceseNumber, DateTime busStartDate)//Checks if the number of digits in the license number matches the year
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


