using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// בפפונקצית הוספת אוטובוס במחלקת בס ליין- אולי נגיד לו שיכנס עוד פעם לוקייששן ואז נעשה וייל במיין 
//כשהמשתמש רוצה להוסיף תחנה למסלול של קו אנחנו נדיס לו את אורך המסלול ונשאל אותו איפה הוא רוצה להוסיף
// לזכור לעשות טריי וקאטצ אחרי כל פונקציה שזרקנו אקספשין
// ליצור מחלקה של התחנות הקיימות
// לטפל באקספשן לא יהיה מסוג אקספשן
namespace dotNet5781_02_7073_1160
{
    class Program
    {
        static void Main(string[] args)
        {

            BusCollection busCollection = InitializeBusCollection();

            string choose = string.Empty;
            while (choose != "0")
            {
                Console.WriteLine("Please, choose one of the following:");
                Console.WriteLine("1: Add a new bus line.");
                Console.WriteLine("2: Add a new bus stop to a bus line.");
                Console.WriteLine("3: Delete a bus line.");
                Console.WriteLine("4: Delete a bus stop from a bus line.");
                Console.WriteLine("5: Search all bus lines that stop in bus station.");
                Console.WriteLine("6: Print route options for two station.");
                Console.WriteLine("7: Print all the bus lines in the system.");
                Console.WriteLine("8: Print all the stops and their bus lines.");
                Console.WriteLine("0: Exit.");
                choose = Console.ReadLine();
                switch (choose)
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


            BusCollection busCollection = new BusCollection();
            InitializeBusStopsList(busCollection);

            //קווים
            for (int i = 1; i < 11; i++)
            {
                int area = RandomArea.Next() * (8 + 1) - 1;
                BusLine busLine = new BusLine(i.ToString(), (Enum.Area)area);
                // כל הקווים מתחילים מאותן 10 תחנות וכך יש לי 10 תחנות עם אותו הקו כפול 10
                // נשאר לנו לחלק את תחנות 10 - 39  לשאר הקווים
                // כל קו יקבל 3 תחנות נוספות חדשות מהקווים הללו

                for (int j = 0; j < 12; j++)
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
                    busLine.AddStation(j, busLineStation);
                }
                busCollection.BusLinesList.Add(busLine);
            }

            return busCollection;
        }

        public static void InitializeBusStopsList(BusCollection busCollection)
        {
            busCollection.BusStopsList = new List<BusStop>();

            for (int i = 0; i < 40; i++)
            {
                string busStationKey = "1234" + i.ToString().PadLeft(2, '0');

                BusStop busStop = new BusStop(busStationKey, "Adress " + i);
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
                Console.WriteLine(bus.BusLineNumber);
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
