using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_7073_1160
{
    class BusCollection//:IEnumerable
    {
        public List<BusLine> BusLines { get; set; }
        public void AddBus(BusLine busLineNumber)
        {
            bool isBusExist = IsBusExist(busLineNumber.BusLineNumber);
            string choice = "add";
            if (!isBusExist)
            {
                BusLineStation busLineStation = new BusLineStation();
                InputCheckAndAddingOrDeletingBus(busLineNumber, choice, busLineStation);
                if ()//להשלים כאן
                {

                }
                //UpdateDistanceAndTimeFromPreviousStation(distanceFromPreviousBusStop, travelTimeFromPrevioussBusStop, choice);
            }
            else
            {
                throw new Exception("The bus station exists in the system, the requested station cannot be added");
            }
        }


        public void InputCheckAndAddingOrDeletingBus(BusLine busLineNumber, string choice, BusLineStation busLineStation)//, int index)
        {
            if (choice == "add")
            {
                Console.WriteLine("To add a station, press A");
                string ch = Console.ReadLine();
                while (ch == "A" || ch == "a")
                {

                    Console.WriteLine("Please enter Bus Station Key");
                    string busStationKey = Console.ReadLine();
                    if (busLineNumber.IsBusStopExist(busStationKey))
                    {
                        throw new Exception("The bus station exists in the system, the requested station cannot be added");
                    }
                    else
                    {
                        foreach (var bus in BusLines)
                        {
                            foreach (var stationkey in busLineStation.BusStop.BusStationKey)
                            {
                                if (busLineStation.BusStop.BusStationKey == busStationKey)
                                {
                                    AddingBusStopToBusLine(choice);
                                }
                            }
                        }
                        Console.WriteLine("Please enter the station address");
                        string stationAddress = Console.ReadLine();
                        if (string.IsNullOrEmpty(stationAddress))
                        {
                            throw new ArgumentException($"'{nameof(stationAddress)}' cannot be null or empty", nameof(stationAddress));
                        }
                        AddingBusStopToBusLine(choice);
                        BusStop busStop = new BusStop(busStationKey, stationAddress);
                    }
                }
                //else
                //    {

                //    }

            }

        }

        private static void AddingBusStopToBusLine(String choice)
        {
            BusLine bus = new BusLine();
            Console.WriteLine("Enter the number you want to place the station on the list");
            string indexStr = Console.ReadLine();
            int index;
            bool temp = int.TryParse(indexStr, out index);
            Console.WriteLine("Please enter distance from previous bus stop");
            string distanceFromPreviousBusStopStr = Console.ReadLine();
            double distanceFromPreviousBusStop;
            bool temp1 = double.TryParse(distanceFromPreviousBusStopStr, out distanceFromPreviousBusStop);
            TimeSpan travelTimeFromPrevioussBusStop;
            Console.WriteLine("Please enter travel from previous bus stop");
            string travelTimeFromPrevioussBusStopStr = Console.ReadLine();
            bool temp2 = TimeSpan.TryParse(travelTimeFromPrevioussBusStopStr, out travelTimeFromPrevioussBusStop);
            bus.UpdateDistanceAndTimeFromPreviousStation(index, distanceFromPreviousBusStop, travelTimeFromPrevioussBusStop, choice);  
        }

        private void IsBusE(BusLine busLineNumber)
        {
            if (string.IsNullOrEmpty(busLineNumber.BusLineNumber))
            {
                throw new ArgumentException($"'{nameof(busLineNumber)}' cannot be null or empty", nameof(busLineNumber));
            }
            int count = 0;
            foreach (BusLine busLine in BusLines)
            {
                if (busLine.BusLineNumber == busLineNumber.BusLineNumber)
                {
                    count++;
                }
            }
            if (count >= 2)
            {
                throw new Exception("This line already exists in the system");
            }
        }

        private bool IsBusExist(string busLineNumber)
        {
            if (string.IsNullOrEmpty(busLineNumber))
            {
                throw new ArgumentException($"'{nameof(busLineNumber)}' cannot be null or empty", nameof(busLineNumber));
            }

            foreach (var busLines in BusLines)
            {
                if (busLines.BusLineNumber == busLineNumber)
                {
                    return true;
                }
            }
            return false;
        }
        ////public IEnumerator GetEnumerator()
        //{
        //    return BusLines.GetEnumerator();
        //}
    }
}
