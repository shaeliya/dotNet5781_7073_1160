using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_7073_1160
{     //פרטים על קו האוטובוס

    public class BusLine : IComparable<BusLine>
    {

        public List<BusLineStation> Stations = new List<BusLineStation>();
        public string BusLineNumber { get; set; }
        public Enum.Area Area { get; set; }
        public int BusLineNum { get; set; }

        public BusLine(string busLineNumber, Enum.Area area)
        {
            BusLineNumber = busLineNumber;
            Area = area;
            Stations = new List<BusLineStation>();
        }
        public BusLine()
        {

        }


        public BusLineStation GetStartStation()
        {
            return Stations[0];
        }
        public BusLineStation GetLastStationKey()
        {
            return Stations[Stations.Count - 1];
        }
        public override string ToString()
        {
            List<string> stationKeys = new List<string>();
            foreach (var station in Stations)
            {
                stationKeys.Add(station.BusStop.BusStationKey);
            }

            return $@"The bus line is: {BusLineNumber} 
                       The area is: {Area}
                       The bus station codes are: { string.Join(", ", stationKeys)}";

        }


        public void AddSingleBusStopToBusLine(List<BusStop> BusStopsList)
        {
            Console.WriteLine("Enter the index you want to place the station on the list. start from 1");
            string indexStr = Console.ReadLine();
            int index;
            bool temp = int.TryParse(indexStr, out index);
            double distanceFromPreviousBusStop = 0;
            TimeSpan travelTimeFromPrevioussBusStop = new TimeSpan(0, 0, 0);
            if (index != 1)
            {
                Console.WriteLine("Please enter distance from previous bus stop");
                string distanceFromPreviousBusStopStr = Console.ReadLine();
                Console.WriteLine("Please enter travel time from previous bus stop:");
                Console.WriteLine("hours:");
                int hours = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("minutes:");
                int minutes = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("seconds:");
                int seconds = Convert.ToInt32(Console.ReadLine());
                bool temp1 = double.TryParse(distanceFromPreviousBusStopStr, out distanceFromPreviousBusStop);
                travelTimeFromPrevioussBusStop = new TimeSpan(hours, minutes, seconds);

                if (distanceFromPreviousBusStop <= 0.0)
                {
                    throw new FormatException(("The distance format is incorrect. Distance must be positive and greater than 0"));
                }

                TimeSpan minTime = new TimeSpan(0, 0, 0);
                if (travelTimeFromPrevioussBusStop <= minTime)
                {
                    throw new FormatException("The travel time format is incorrect.Travel time must be positive and greater than 0");
                }
            }

            //ניצור תחנה אם לא קיימת ואם כן - נביא אותה כולל קווי אורך ורוחב שהוגרלו לה בעבר
            BusStop busStop = AddBusStopToList(BusStopsList);
            // ניצור תחנה ביחס לקו - כולל מרחק וזמן נסיעה מתחנה קודמת
            BusLineStation station = new BusLineStation(travelTimeFromPrevioussBusStop, distanceFromPreviousBusStop, busStop);

            //נוסיף את התחנה לקו
            AddStation(index, station);

            Console.WriteLine("The station was successfully added");
        }

        /// <summary>
        /// לבדוק שהתחנה קיימת בליסט ואם לא ליצור
        /// </summary>
        /// <returns></returns>
        public BusStop AddBusStopToList(List<BusStop> BusStopsList)
        {
            string busStationKey;
            Console.WriteLine("enter busStationKey ");
            busStationKey = Console.ReadLine();

            if (busStationKey.Length > 6)
            {
                throw new FormatException("busStationKey must be 6 digits or less");
            }
            string stationAddress;
            Console.WriteLine("enter stationAddress ");
            stationAddress = Console.ReadLine();

            foreach (var stop in BusStopsList)
            {
                if (stop.BusStationKey == busStationKey)
                {
                    return stop;
                }
            }

            BusStop busStop = new BusStop(busStationKey, stationAddress);
            BusStopsList.Add(busStop);
            return busStop;

        }

        /// <summary>
        /// לבדוק שהתחנה קיימת בליסט ואם לא ליצור
        /// </summary>
        /// <returns></returns>
        public void AddStation(int index, BusLineStation busLineStation)
        {
            bool isBusStopExist = IsBusStopExist(busLineStation.BusStop.BusStationKey);
            string choice = "add";
            if (!isBusStopExist)
            {
                ValidityCheckAndAddingOrDeletingStation(index, busLineStation, choice);
                UpdateDistanceAndTimeFromPreviousStation(index, busLineStation.DistanceFromPreviousBusStop, busLineStation.TravelTimeFromPrevioussBusStop, choice);
            }
            else
            {
                throw new ItemAlreadyExistsException("Bus Station", "The bus station exists in the line, the requested station cannot be added");
            }

        }
        public void DeleteStation(string busStationKey)
        {
            int index = FindIndexOfStationInList(busStationKey);
            if (index > -1)
            {
                string choice = "delete";
                double distanceFromPreviousBusStop = Stations[index].DistanceFromPreviousBusStop;
                TimeSpan travelTimeFromPrevioussBusStop = Stations[index].TravelTimeFromPrevioussBusStop;
                // הפונקציות של בדיקת תקינות מתייחסות לאינדקס + 1 ולכן נוסיף פה 1
                index = index + 1;
                ValidityCheckAndAddingOrDeletingStation(index, null, choice);
                UpdateDistanceAndTimeFromPreviousStation(index, distanceFromPreviousBusStop, travelTimeFromPrevioussBusStop, choice);
            }
            else
            {
                Console.WriteLine("The bus station does not exist in the system, the requested station cannot be deleted");
            }
        }
        public void UpdateDistanceAndTimeFromPreviousStation(int index, double distanceFromPreviousBusStop, TimeSpan travelTimeFromPrevioussBusStop, string choice)
        {


            if (choice == "add")
            {
                // מכיוון שהמרחק והזמן הם מהתחנה הקודמת, צריך לעדכן תמיד רק את התחנה הבאה לחנה שמכניסים
                // ולכן, אם מדובר בתחנה אחרונה - אין צורך לעדכן כלום
                if (index == Stations.Count)
                {
                    return;
                }
                TimeSpan time = new TimeSpan(0, 3, 9);
                // החלטת מרצת הקורס: מכייון שאחנו לא יודעים את המרחק מהתחנה החדשה לתחנה הקיימת
                // נחליט על מספר שרירותי
                Stations[index].DistanceFromPreviousBusStop += 5.83;
                Stations[index].TravelTimeFromPrevioussBusStop += time;
            }
            else
            {
                // מכיוון שהמרחק והזמן הם מהתחנה הקודמת, צריך לעדכן תמיד רק את התחנה הבאה לחנה שמכניסים
                // ולכן, אם מדובר בתחנה אחרונה - אין צורך לעדכן כלום
                if (index == Stations.Count + 1)
                {
                    return;
                }
                if (index == 1)
                {
                    TimeSpan time = new TimeSpan(0, 0, 0);
                    Stations[index - 1].DistanceFromPreviousBusStop = 0;
                    Stations[index - 1].TravelTimeFromPrevioussBusStop = time;
                }
                else
                {
                    Stations[index - 1].DistanceFromPreviousBusStop += distanceFromPreviousBusStop;
                    Stations[index - 1].TravelTimeFromPrevioussBusStop += travelTimeFromPrevioussBusStop;
                }
            }

        }
        public bool IsBusStopExist(string busStationKey)
        {
            if (string.IsNullOrEmpty(busStationKey))
            {
                throw new FormatException($"'{nameof(busStationKey)}' cannot be null or empty");
            }

            int index = FindIndexOfStationInList(busStationKey);

            return index > -1;
        }

        private void ValidityCheckAndAddingOrDeletingStation(int index, BusLineStation busLineStation, string choice)
        {
            if (choice == "add")
            {
                if (index <= Stations.Count || index == Stations.Count + 1)
                {
                    Stations.Insert(index - 1, busLineStation);
                }
                else
                {

                    throw new IndexOutOfRangeException("It is not possible to add a station in the requested location");

                }
            }
            else
            {
                if (index <= Stations.Count)
                {
                    Stations.RemoveAt(index - 1);
                    Console.WriteLine("The station was successfully deleted");

                }
                else
                {
                    throw new IndexOutOfRangeException("It is not possible to delete a station in the requested location");

                }
            }

        }
        /// <summary>
        /// פונקציה שמחזירה אינדקס של תחנה. אם לא נמצא מחזירה -1
        /// </summary>
        /// <param name="busStationKey"></param>
        /// <returns></returns>
        public int FindIndexOfStationInList(string busStationKey)
        {
            for (int i = 0; i < Stations.Count; i++)
            {
                if (Stations[i].BusStop.BusStationKey == busStationKey)
                {
                    return i;
                }
            }
            return -1;
        }
        public double DistancBetweenTwoStationsOnBusLine(string busLineStation1, string busLineStation2)
        {
            int indexBusStationKey1 = FindIndexOfStationInList(busLineStation1);
            if (indexBusStationKey1 == -1)
            {
                throw new KeyNotFoundException("The index does not exist in the system");
            }
            int indexBusStationKey2 = FindIndexOfStationInList(busLineStation2);
            if (indexBusStationKey2 == -1)
            {
                throw new KeyNotFoundException("The index does not exist in the system");
            }
            double distancBetweenTwoStationsOnBusLineStations = Stations[indexBusStationKey1].DistanceFromPreviousBusStop - Stations[indexBusStationKey2].DistanceFromPreviousBusStop;
            return distancBetweenTwoStationsOnBusLineStations;
        }
        public TimeSpan TimeBetweenTwoStationsOnBusLine(string busLineStation1, string busLineStation2)
        {
            int indexBusStationKey1 = FindIndexOfStationInList(busLineStation1);
            if (indexBusStationKey1 == -1)
            {
                throw new KeyNotFoundException("The index does not exist in the system");
            }
            int indexBusStationKey2 = FindIndexOfStationInList(busLineStation2);
            if (indexBusStationKey2 == -1)
            {
                throw new KeyNotFoundException("The index does not exist in the system");
            }
            TimeSpan timeBetweenTwoStationsOnBusLineStations = Stations[indexBusStationKey1].TravelTimeFromPrevioussBusStop - Stations[indexBusStationKey2].TravelTimeFromPrevioussBusStop;
            return timeBetweenTwoStationsOnBusLineStations;
        }
        public BusLine ReturnsSubRouteOfBusLine(string busLineStationkey1, string busLineStationkey2)
        {
            bool isBusStopExist1 = IsBusStopExist(busLineStationkey1);
            bool isBusStopExist2 = IsBusStopExist(busLineStationkey2);
            BusLine subBusLine = new BusLine();
            if (!isBusStopExist1 || !isBusStopExist2)
            {
                return null;
            }
            else
            {
                int Index1 = FindIndexOfStationInList(busLineStationkey1);
                int Index2 = FindIndexOfStationInList(busLineStationkey2);
                int maxStationIndex = Math.Max(Index2, Index1);
                int minStationIndex = Math.Min(Index2, Index1);
                for (int i = minStationIndex; i <= maxStationIndex; i++)
                {
                    BusLineStation busLineStation = Stations[i];
                    subBusLine.AddStation(i - minStationIndex + 1, busLineStation);
                }
                return subBusLine;
            }

        }
        public int CompareTo(BusLine bus)
        {
            double compare = GetLineLength() - bus.GetLineLength();
            if (compare > 0)
            {
                return 1;
            }
            else if (compare < 0)
            {
                return -1;
            }
            return 0;
        }
        public void PrintBusRoute(bool isNew)
        {
            string newString = isNew ? "New " : string.Empty;
            Console.WriteLine("---------------------------");
            Console.WriteLine(newString + "Route fot Bus Line: " + BusLineNumber);
            Console.WriteLine("---------------------------");
            foreach (var station in Stations)
            {
                Console.Write(station.BusStop.BusStationKey + " -> ");
            }
            Console.WriteLine(" You have arrived to your destination! ");
        }

        public double GetLineLength()
        {
            double time = 0;
            bool isFirst = true;
            foreach (BusLineStation station in Stations)
            {
                if (isFirst)
                {
                    isFirst = false;
                }
                else
                {
                    time += station.TravelTimeFromPrevioussBusStop.TotalMilliseconds;
                }
            }
            return time;

        }
    }
}