using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//-------------------------------
// Shalhevet Eliyahu 211661160
// Orit Stavsky 212507073
//-------------------------------

namespace dotNet5781_02_7073_1160
{
   public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                BusCollection busCollection = InitializeBusCollection();
                Menu(busCollection);
            }
            catch(NotEnoughStationsException ex)
            {
                Console.WriteLine(ex.ToString());
            }
            catch (ItemAlreadyExistsException ex)
            {
                Console.WriteLine(ex.ToString());
            }
            catch (KeyNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (IndexOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            finally
            {
                Console.WriteLine("Thank you for using our system!");
                Console.WriteLine("We hope you enjoyed the experience :)");
                Console.ReadKey();

            }

        }

        private static void Menu(BusCollection busCollection)
        {
            string choice = string.Empty;
            while (choice != "0")
            {
                Console.WriteLine("----------------");
                Console.WriteLine("~~~~~~Menu~~~~~~");
                Console.WriteLine("----------------");
                Console.WriteLine("Please choose one of the following:");
                Console.WriteLine("1: Add a new bus line.");
                Console.WriteLine("2: Add a new bus stop to a bus line.");
                Console.WriteLine("3: Delete a bus line.");
                Console.WriteLine("4: Delete a bus stop from a bus line.");
                Console.WriteLine("5: Search all bus lines that stop in bus station.");
                Console.WriteLine("6: Print route options for two station.");
                Console.WriteLine("7: Print all the bus lines in the system.");
                Console.WriteLine("8: Print all the stops and their bus lines.");
                Console.WriteLine("0: Exit.");
                choice = Console.ReadLine();
                switch (choice.Trim())
                {
                    case "1":
                        AddNewBusLine(busCollection);
                        break;
                    case "2":
                        AddNewBusStopToBusLine(busCollection);
                        break;
                    case "3":
                        DeleteBusLine(busCollection);
                        break;
                    case "4":
                        DeleteBusStopFromBusLine(busCollection);
                        break;
                    case "5":
                        SearchBusLinesThatStopInBusStation(busCollection);
                        break;
                    case "6":
                        PrintRouteOptionsForTwoStation(busCollection);
                        break;
                    case "7":
                        PrintAllBusses(busCollection);
                        break;
                    case "8":
                        PrintAllTheStopsAndTheirBusLines(busCollection);
                        break;
                    case "0":
                        break;
                    default:
                        Console.WriteLine("ERROR");
                        break;
                }
            }
        }


        /// <summary>
        /// הפונקציה מאתחלת אוסף קווים
        /// </summary>
        /// <returns>BusCollection</returns>
        public static BusCollection InitializeBusCollection()
        {
            Random RandomArea = new Random(DateTime.Now.Millisecond);

            Random RandomBusLine = new Random(DateTime.Now.Millisecond);

            BusCollection busCollection = new BusCollection();
            InitializeBusStopsList(busCollection);

            //קווים
            for (int i = 1; i < 11; i++)
            {
                int area = RandomArea.Next(0,8);
                int busLineRandom = RandomBusLine.Next(1, 999);

                BusLine busLine = new BusLine(busLineRandom.ToString(), (Enum.Area)area);
                // כל הקווים מתחילים מאותן 10 תחנות וכך יש לי 10 תחנות עם אותו הקו כפול 10
                // נשאר לנו לחלק את תחנות 10 - 39  לשאר הקווים
                // כל קו יקבל 3 תחנות נוספות חדשות מהקווים הללו

                for (int j = 0; j <= 12; j++)
                {
                    TimeSpan travelTimeFromPrevioussBusStop = new TimeSpan(0, i, i * 2);
                    BusStop busStop = new BusStop();
                    if (j >= 0 && j <= 9)
                    {
                        busStop = busCollection.BusStopsList[j];
                    }
                    else
                    {
                        busStop = busCollection.BusStopsList[j + (i - 1) * 3];
                    }
                    BusLineStation busLineStation = new BusLineStation(travelTimeFromPrevioussBusStop, i * 1.1, busStop);
                    busLine.AddStation(j + 1, busLineStation);
                }
                busCollection.BusLinesList.Add(busLine);
            }

            return busCollection;
        }

        public static void InitializeBusStopsList(BusCollection busCollection)
        {
            Random RandomBusStop = new Random(DateTime.Now.Millisecond);
            Random RandomAdress = new Random();

            busCollection.BusStopsList = new List<BusStop>();

            for (int i = 0; i < 40; i++)
            {
                string busStationKey = RandomBusStop.Next(1, 1000000).ToString();
                int adress = RandomAdress.Next(1, 1000);
                BusStop busStop = new BusStop(busStationKey, "Adress " + adress);
                busCollection.BusStopsList.Add(busStop);
            }
        }


        public static void AddNewBusLine(BusCollection busCollection)
        {

            busCollection.AddBusLine();

        }


        public static void AddNewBusStopToBusLine(BusCollection busCollection)
        {

            busCollection.AddStationToSpecificBusLine();

        }
        public static void DeleteBusLine(BusCollection busCollection)
        {

            busCollection.DeleteBusLine();

        }

        public static void DeleteBusStopFromBusLine(BusCollection busCollection)
        {

            busCollection.DeleteStationFromSpecificBusLine();

        }


        public static void SearchBusLinesThatStopInBusStation(BusCollection busCollection)
        {

            Console.WriteLine("Enter bus station key");
            string busStationKey = Console.ReadLine();
            PrintAllLinesForStation(busCollection, busStationKey);

        }
        /// <summary>
        /// פונקציית עזר המקבלת מפתח של תחנה ומדפיסה את כל הקווים שעוברים דרכה
        /// </summary>
        /// <param name="busCollection"></param>
        /// <param name="busStationKey"></param>
        private static void PrintAllLinesForStation(BusCollection busCollection, string busStationKey)
        {
            List<string> allBusLinesThatStopInBusStation = busCollection.GetBusLinesThatStopInBusStation(busStationKey);

            Console.WriteLine("-----------------------------------");
            Console.WriteLine("Bus Station Key " + busStationKey + ":");
            Console.WriteLine("-----------------------------------");
            if (allBusLinesThatStopInBusStation.Count == 0)
            {
                Console.WriteLine("No bus lines found.");
            }

            foreach (string busLineNumber in allBusLinesThatStopInBusStation)
            {
                Console.WriteLine(busLineNumber);
            }
        }

        public static void PrintRouteOptionsForTwoStation(BusCollection busCollection)
        {
            BusCollection busLines = new BusCollection();

            Console.WriteLine("Enter Station key 1");
            string busLineStationkey1 = Console.ReadLine();
            Console.WriteLine("Enter Station key 2");
            string busLineStationkey2 = Console.ReadLine();
            foreach (BusLine bus in busCollection)
            {
                BusLine subRoute = bus.ReturnsSubRouteOfBusLine(busLineStationkey1, busLineStationkey2);
                if (subRoute != null)
                {
                    subRoute.BusLineNumber = bus.BusLineNumber;                        
                    busLines.BusLinesList.Add(subRoute);
                }
            }
            if (busLines.BusLinesList.Count == 0)
            {
                Console.WriteLine("No bus lines found for selected stations");
                return;

            }

            Console.WriteLine("Bus lines found for selected stations:");

            busLines.SortBusCollection();
            foreach (var bus in busLines)
            {
                bus.PrintBusRoute(false);
            }

        }

        public static void PrintAllBusses(BusCollection busCollection)
        {
            busCollection.PrintAllBusses();
        }

        public static void PrintAllTheStopsAndTheirBusLines(BusCollection busCollection)
        {
            if (busCollection.BusStopsList == null || busCollection.BusStopsList.Count == 0)
            {
                Console.WriteLine("no Bus Stops in collection:");
                return;
            }

            foreach (var stop in busCollection.BusStopsList)
            {
                PrintAllLinesForStation(busCollection, stop.BusStationKey);
            }
        }
    }

}
