using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_7073_1160
{     //פרטים על קו האוטובוס

    class BusLine
    {

        List<BusLineStation> Stations = new List<BusLineStation>();
        public string BusLineNumber { get; set; }
        public string GetStartStationKey ()
        {
           return Stations[0].BusStop.BusStationKey;
        }
        public string GetLastStationKey()
        {
            return Stations[Stations.Count-1].BusStop.BusStationKey;
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
        private void UpdateDistanceAndTimeFromPreviousStation(int index, double distanceFromPreviousBusStop, TimeSpan travelTimeFromPrevioussBusStop, string choice)
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
        private double DistancBetweenTwoStationsOnBusLine(BusLineStation busLineStation, string busLineStation1,string busLineStation2)
        {      
           int indexBusStationKey1= ReturnsIindexOfStationInList(busLineStation, busLineStation1);
            int indexBusStationKey2 = ReturnsIindexOfStationInList(busLineStation, busLineStation2);
            double distancBetweenTwoStationsOnBusLineStations = Stations[indexBusStationKey1].DistanceFromPreviousBusStop - Stations[indexBusStationKey2].DistanceFromPreviousBusStop;
            return distancBetweenTwoStationsOnBusLineStations;
        }
        private TimeSpan TimeBetweenTwoStationsOnBusLine(BusLineStation busLineStation, string busStationKey, string busLineStation1, string busLineStation2)
        {
            int indexBusStationKey1 = ReturnsIindexOfStationInList(busLineStation, busLineStation1);
            int indexBusStationKey2 = ReturnsIindexOfStationInList(busLineStation, busLineStation2);
            TimeSpan timeBetweenTwoStationsOnBusLineStations = Stations[indexBusStationKey1].TravelTimeFromPrevioussBusStop - Stations[indexBusStationKey2].TravelTimeFromPrevioussBusStop;
            return timeBetweenTwoStationsOnBusLineStations;
        }
    }
}
//בס סטופ מחלקה שיש בה מידע על כל תחנת אוטובוס
//בס ליין סטיישן תחנה בהקשר לקו מסויים
//בס ליין זה קו אוטובוס מוסיפים לקו תחנות