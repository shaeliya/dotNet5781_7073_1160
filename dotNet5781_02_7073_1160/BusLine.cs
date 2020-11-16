using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_7073_1160
{     //פרטים על קו האוטובוס

    class BusLine:IComparable<BusLine>
    {

        List<BusLineStation> Stations = new List<BusLineStation>();
        public BusLine(string busLineNumber, string area)
        {
            BusLineNumber = busLineNumber;
            Area = area;
        }
        public BusLine()
        {

        }


        public string BusLineNumber { get; set; }
        public BusLineStation GetStartStation ()
        {
            return Stations[0];
        }
        public BusLineStation GetLastStationKey()
        {
            return Stations[Stations.Count-1];
        }
        public string Area { get; set; }
        public override string ToString()
        {
            List<string> stationKeys = new List<string>();
            foreach (var station in Stations)
            {
                stationKeys.Add(station.BusStop.BusStationKey);
            }

            return $@"The bus line is: {BusLineNumber} 
The area is: {Area}
The bus station codes are: { string.Join(", ", stationKeys)}
"
;

        }
        public void AddStation(int index, BusLineStation busLineStation, double distanceFromPreviousBusStop, TimeSpan travelTimeFromPrevioussBusStop)
        {
            bool isBusStopExist = IsBusStopExist(busLineStation.BusStop.BusStationKey);
            string choice = "add";
            if (!isBusStopExist)
            {
                InputCheckAndAddingOrDeletingStation(index, busLineStation, choice);
                UpdateDistanceAndTimeFromPreviousStation(index, distanceFromPreviousBusStop, travelTimeFromPrevioussBusStop, choice);
            }
            else
            {
                throw new Exception("The bus station exists in the system, the requested station cannot be added");            
            }

        }
        public void DeleteStation(int index, BusLineStation busLineStation, double distanceFromPreviousBusStop, TimeSpan travelTimeFromPrevioussBusStop)
        {
            bool isBusStopExist = IsBusStopExist(busLineStation.BusStop.BusStationKey);
            string choice = "delete";
            if (isBusStopExist)
            {
                InputCheckAndAddingOrDeletingStation(index, busLineStation, choice);
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
                if (index == 1)
                {
                    Stations[index].DistanceFromPreviousBusStop = distanceFromPreviousBusStop;
                    Stations[index].TravelTimeFromPrevioussBusStop = travelTimeFromPrevioussBusStop;
                }
                else if (index == Stations.Count + 1)
                {
                    Stations[index - 1].DistanceFromPreviousBusStop = distanceFromPreviousBusStop;
                    Stations[index - 1].TravelTimeFromPrevioussBusStop = travelTimeFromPrevioussBusStop;
                }
                else
                {
                    TimeSpan time = new TimeSpan(0, 3, 58);
                    Stations[index].DistanceFromPreviousBusStop = distanceFromPreviousBusStop + 5.83;
                    Stations[index].TravelTimeFromPrevioussBusStop = travelTimeFromPrevioussBusStop + time;
                    Stations[index - 1].DistanceFromPreviousBusStop = distanceFromPreviousBusStop;
                    Stations[index - 1].TravelTimeFromPrevioussBusStop = travelTimeFromPrevioussBusStop;
                }
            }
            else
            {
                if (index == 1)
                {
                    TimeSpan time = new TimeSpan(0, 0, 0);
                    Stations[index - 1].DistanceFromPreviousBusStop = 0;
                    Stations[index - 1].TravelTimeFromPrevioussBusStop = time;
                }

                if (!(index == 1) || !(index == Stations.Count + 1))
                {
                    Stations[index].DistanceFromPreviousBusStop = distanceFromPreviousBusStop;
                    Stations[index].TravelTimeFromPrevioussBusStop = travelTimeFromPrevioussBusStop;
                }
            }

        }
        public bool IsBusStopExist(string busStationKey)
        {
            if (string.IsNullOrEmpty(busStationKey))
            {
                throw new ArgumentException($"'{nameof(busStationKey)}' cannot be null or empty", nameof(busStationKey));
            }
            foreach (var station in Stations)
            {
                if (station.BusStop.BusStationKey == busStationKey)
                {
                    return true;
                }
            }
            return false;
        }
        private void InputCheckAndAddingOrDeletingStation(int index, BusLineStation busLineStation, string choice)
        {
            if (choice == "add")
            {
                if (index <= Stations.Count || index == Stations.Count + 1)
                {

                    Stations.Insert(index - 1, busLineStation);
                    Console.WriteLine("The station was successfully added");

                }
                else
                {

                    throw new System.IndexOutOfRangeException("It is not possible to add a station in the requested location");
                   
                }
            }
            else
            {
                if (index <= Stations.Count)
                {

                    Stations.Insert(index - 1, busLineStation);
                    Console.WriteLine("The station was successfully deleted");

                }
                else
                {
                    throw new System.IndexOutOfRangeException("It is not possible to delete a station in the requested location");
                  
                }
            }

        }
        private int ReturnsIindexOfStationInList(BusLineStation busLineStation, string busStationKey)
        {
            bool IndexExist = false;
            for (int i = 0; i < Stations.Count; i++)
            {
                if (busLineStation.BusStop.BusStationKey == busStationKey)
                {
                    IndexExist = true;
                    return i;
                }                
            }
            if (!IndexExist)
            {
                throw new Exception("The index does not exist in the system");
            }
            return -1;
        }
        public double DistancBetweenTwoStationsOnBusLine(BusLineStation busLineStation, string busLineStation1, string busLineStation2)
        {      
           int indexBusStationKey1= ReturnsIindexOfStationInList(busLineStation, busLineStation1);
            int indexBusStationKey2 = ReturnsIindexOfStationInList(busLineStation, busLineStation2);
            double distancBetweenTwoStationsOnBusLineStations = Stations[indexBusStationKey1].DistanceFromPreviousBusStop - Stations[indexBusStationKey2].DistanceFromPreviousBusStop;
            return distancBetweenTwoStationsOnBusLineStations;
        }
        public TimeSpan TimeBetweenTwoStationsOnBusLine(BusLineStation busLineStation, string busLineStation1, string busLineStation2)
        {
            int indexBusStationKey1 = ReturnsIindexOfStationInList(busLineStation, busLineStation1);
            int indexBusStationKey2 = ReturnsIindexOfStationInList(busLineStation, busLineStation2);
            TimeSpan timeBetweenTwoStationsOnBusLineStations = Stations[indexBusStationKey1].TravelTimeFromPrevioussBusStop - Stations[indexBusStationKey2].TravelTimeFromPrevioussBusStop;
            return timeBetweenTwoStationsOnBusLineStations;
        }
        public BusLine ReturnsSubRouteOfBusLine(BusLineStation busLineStation,string busLineStationkey1, string busLineStationkey2)
        {
            bool isBusStopExist1 = IsBusStopExist(busLineStationkey1);
            bool isBusStopExist2 = IsBusStopExist(busLineStationkey2);
            BusLine busLine = new BusLine();
            if (!isBusStopExist1 ||! isBusStopExist2) 
            {
                throw new Exception("The bus stop does not exist in the system");
            }
            else
            {
                int Index1 = ReturnsIindexOfStationInList(busLineStation, busLineStationkey1);
                int Index2 = ReturnsIindexOfStationInList(busLineStation, busLineStationkey2);
                int SubRouteSize = Math.Abs(Index2 - Index1);
                for (int i = 0; i < SubRouteSize; i++)
                {
                    busLine.AddStation(i, busLineStation, busLineStation.DistanceFromPreviousBusStop, busLineStation.TravelTimeFromPrevioussBusStop);
                }
                return busLine;
            }

        }
        public int CompareTo(BusLine bus)
        {
            double compare = GetLineTime() - bus.GetLineTime();
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
        private double GetLineTime()
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
//בס סטופ מחלקה שיש בה מידע על כל תחנת אוטובוס
//בס ליין סטיישן תחנה בהקשר לקו מסויים
//בס ליין זה קו אוטובוס מוסיפים לקו תחנות