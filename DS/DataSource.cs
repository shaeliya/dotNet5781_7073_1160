using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DS
{
    //מחלקה המגדירה את זהות של ישות תחנות עוקבות
    class AdjacentStationsComparer : IEqualityComparer<AdjacentStations>
    {
        public bool Equals(AdjacentStations x, AdjacentStations y)
        {
            return (x.StationId1 == y.StationId1 && x.StationId2 == y.StationId2) ||
                (x.StationId1 == y.StationId2 && x.StationId2 == y.StationId1);
        }

        public int GetHashCode(AdjacentStations obj)
        {
            return 0;
        }
    }
    public static class DataSource
    {
        public static HashSet<AdjacentStations> adjacentStationsList = new HashSet<AdjacentStations>(new AdjacentStationsComparer());
        public static List<Bus> busesList;
        public static List<BusOnTrip> busOnTripsList;
        public static List<Line> linesList;
        public static List<LineStation> lineStationsList;
        public static List<LineTrip> lineTripsList;
        public static List<Station> stationsList;

        static DataSource()
        {
            InitializeAllLists();
        }
        static public void InitializeAllLists()
        {
            InitializeBus();
            InitializeStationsList();
            InitializeLineList();
            InitializeLineStationList();

         //   InitializeAdjacentStationsList();
            InitializeLineTripList();
            InitializeBusOnTripList();

        }

        #region Initialize Bus 


        private static void InitializeBus()
        {
            busesList = new List<Bus>();
            DateTime fromDate, lastTreatmentDate;
            double fuelRemain;
            Random RandomFuel = new Random(DateTime.Now.Millisecond);
            Bus bus;
            for (int i = 0; i < 20; i++)
            {
                fuelRemain = RandomFuel.NextDouble() * (1201.0 - 0.0) + 0.0;
                fromDate = RandomfromDate();
                lastTreatmentDate = RandomLastTreatmentDate(fromDate);
                bus = CreateNewBus(fromDate, lastTreatmentDate, fuelRemain);
                busesList.Add(bus);
            }
        }

        private static DateTime RandomfromDate()
        {
            Random gen = new Random();
            DateTime start = new DateTime(1995, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(gen.Next(range));
        }
        private static DateTime RandomLastTreatmentDate(DateTime busStartDate)
        {
            Random gen = new Random();
            DateTime start = new DateTime(busStartDate.Year, busStartDate.Month, busStartDate.Day);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(gen.Next(range));
        }


        private static Bus CreateNewBus(DateTime fromDate, DateTime lastTreatmentDate, double fuelRemain)
        {
            Bus bus = new Bus();
            Random RandomLicenseNumber = new Random(DateTime.Now.Millisecond);
            int licenseNumber;
            double totalTrip, treatment;
            if (fromDate.Year < 2018)
            {
                int licenseNumberRandom = RandomLicenseNumber.Next(1000000, 10000000);
                licenseNumber = licenseNumberRandom;
            }
            else
            {
                int licenseNumberRandom = RandomLicenseNumber.Next(10000000, 100000000);
                licenseNumber = licenseNumberRandom;
            }
            Random RandomTotalTrip = new Random(DateTime.Now.Millisecond);
            Random RandomTreatment = new Random(DateTime.Now.Millisecond);
            treatment = RandomTreatment.NextDouble() * (20000.0 - 0.0) + 0.0;
            double min;
            if (1200 - fuelRemain > treatment)
            {
                min = 1200 - fuelRemain;
            }
            else
            {
                min = treatment;
            }
            totalTrip = RandomTotalTrip.NextDouble() * (9999999999.9 - min) + min;

            bus.LicenseNumber = licenseNumber;
            bus.FromDate = fromDate;
            bus.TotalTrip = totalTrip;
            bus.FuelRemain = fuelRemain;
            bus.IsDeleted = false;

            return bus;
        }
        private static void UpdateStaus(Bus bus)
        {

            bool isNeedFuel = true;
            if (bus.Status == Enums.BusStatuses.MidTravel ||
                bus.Status == Enums.BusStatuses.Refueling ||
                bus.Status == Enums.BusStatuses.InTreatment)
            {
                return;
            }

            if (bus.FuelRemain == 0)
            {
                isNeedFuel = true;
            }

            bool isNeedTreatment = false;

            bool isYearPassed = IsYearPassed(bus);
            if (bus.Treatment > 20000 || isYearPassed)
            {
                isNeedTreatment = true;
            }

            if (isNeedFuel && isNeedTreatment)
            {
                bus.Status = Enums.BusStatuses.NeedFuelAndTreatment;
                return;
            }
            if (isNeedTreatment)
            {
                bus.Status = Enums.BusStatuses.NeedTreatment;
                return;

            }
            if (isNeedFuel)
            {
                bus.Status = Enums.BusStatuses.NeedFuel;
                return;

            }

            bus.Status = Enums.BusStatuses.ReadyToGo;

        }
        public static bool IsYearPassed(Bus bus)//The function checks if a year has passed since the last treatment of the bus
        {

            DateTime zeroTime = new DateTime(1, 1, 1);
            var diff = DateTime.Now - bus.LastTreatmentDate;
            int years = (zeroTime + diff).Year - 1;
            if (years >= 1)
            {
                return true;

            }
            return false;
        }
        #endregion Initialize Bus

        #region Initialize Station


        private static void InitializeStationsList()
        {
            stationsList = new List<Station>();
            Random RandomLongitude = new Random(DateTime.Now.Millisecond);
            Random RandomLatitude = new Random(DateTime.Now.Millisecond);
            CreateNewStations("HA-CALANIOT", RandomLongitude, RandomLatitude);
            CreateNewStations("HA-DUVDEVAN", RandomLongitude, RandomLatitude);
            CreateNewStations("HA-LIMON", RandomLongitude, RandomLatitude);
            CreateNewStations("HA-CHARZIT", RandomLongitude, RandomLatitude);
            CreateNewStations("HA-CHAMANIOT", RandomLongitude, RandomLatitude);
        }
        private static void CreateNewStations(string name, Random RandomLongitude, Random RandomLatitude)
        {
            for (int i = 0; i < 10; i++)
            {

                double Latitude = RandomLatitude.NextDouble() * (33.4 - 30.9) + 30.9;
                double Longitude = RandomLongitude.NextDouble() * (35.6 - 34.2) + 34.2;
                Station station = new Station();
                station.StationId = ++Configuration.MaxStationId;
                station.Name = name + (i + 2);
                station.Adress = name + (i + 2);
                station.Latitude = Latitude;
                station.Longitude = Longitude;
                station.IsDeleted = false;

                stationsList.Add(station);
            }
        }

        #endregion Initialize Station

        #region Initialize AdjacentStations


        private static void InitializeAdjacentStationsList()
        {
            Random RandomDistance = new Random(DateTime.Now.Millisecond);
            Random RandomStationId1 = new Random(DateTime.Now.Millisecond);

            for (int i = 0; i < 100; i++)
            {
                AdjacentStations adjacentStations = new AdjacentStations();
                double distance = RandomDistance.NextDouble() * (10.5 - 0.5) + 0.5;
                TimeSpan time = new TimeSpan(0, i + 2, i + 5);

                adjacentStations.Distance = distance;
                adjacentStations.Time = time;
                adjacentStations.IsDeleted = false;
                adjacentStations.AdjacentStationsId = ++Configuration.MaxAdjacentStationsId;
                int stationId1Random = i;
                int stationId2Random = RandomStationId1.Next(1, 100);
                while (stationId1Random == stationId2Random)
                {
                    stationId2Random = RandomStationId1.Next(1, 100);
                }
                adjacentStations.StationId1 = stationId1Random;
                adjacentStations.StationId2 = stationId2Random;
                adjacentStationsList.Add(adjacentStations);
            }
        }

        #endregion Initialize AdjacentStations

        #region Initialize Line


        private static void InitializeLineList()
        {
            linesList = new List<Line>();
            Random RandomArea = new Random(DateTime.Now.Millisecond);
            for (int i = 1; i < 11; i++)
            {
                Line line = new Line();
                int area = RandomArea.Next(0, 8);
                line.LineNumber = i;
                line.Area = (Enums.Areas)area;
                line.LineId = ++Configuration.MaxLineId;
                line.IsDeleted = false;
                linesList.Add(line);
            }

        }

        #endregion Initialize Line

        #region Initialize LineStation 


        private static void InitializeLineStationList()
        {
            lineStationsList = new List<LineStation>();
            int temp = 0;
            for (int i = 0; i < 10; i++)//קווים
            {
                for (int j = 0; j < 10; j++)//תחנות
                {
                    LineStation lineStation = new LineStation();
                    lineStation.LineStationIndex = j + 1;
                    lineStation.LineStationId = ++Configuration.MaxLineStationId;
                    lineStation.LineId = linesList[i].LineId;
                    lineStation.StationId = stationsList[j + temp].StationId;
                    lineStation.IsDeleted = false;
                    if (lineStationsList.Count > 0 && j > 0)
                    {
                        var adj = new AdjacentStations
                        {
                            AdjacentStationsId = ++Configuration.MaxAdjacentStationsId,
                            StationId1 = lineStation.StationId,
                            StationId2 = lineStationsList.Last().StationId,
                            Distance = new Random(DateTime.Now.Millisecond).NextDouble() * (10.5 - 0.5) + 0.5,
                            IsDeleted = false,
                            Time = new TimeSpan(0, i + 2, i + 5)
                        };
                        adjacentStationsList.Add(adj);
                    }
                    lineStationsList.Add(lineStation);
                }
                if (i >= 4 && i < 8)
                {
                    temp -= 10;
                }
                else { temp += 10; }

            }

        }
        #endregion Initialize LineStation 

        #region Initialize BusOnTrip


        private static void InitializeBusOnTripList()
        {
            busOnTripsList = new List<BusOnTrip>();
            Random RandomPrevStation = new Random(DateTime.Now.Millisecond);

            for (int i = 0; i < 10; i++)
            {
                BusOnTrip busOnTrip = new BusOnTrip();
                TimeSpan actualTakeOff;
                busOnTrip.PlannedTakeOff = new TimeSpan(5 + (i / 2), i + 1, i + 4);
                if (i % 2 == 0)
                {
                    actualTakeOff = new TimeSpan(5 + (i / 2), i + 1, i + 50);
                }
                else
                {
                    actualTakeOff = new TimeSpan(5 + (i / 2), i + 6, i + 7);
                }
                int prevStationRandom = RandomPrevStation.Next(1, 50);
                busOnTrip.PrevStationAt = new TimeSpan(5 + (i / 2), i + 1, i + 4);
                busOnTrip.NextStationAt = new TimeSpan(5 + (i / 2), i + 7, i + 9);
                busOnTrip.PrevStation = prevStationRandom;
                busOnTrip.LicenseNumber = busesList[i].LicenseNumber;
                busOnTrip.LineId = linesList[i].LineId;
                busOnTrip.ActualTakeOff = actualTakeOff;
                busOnTrip.BusOnTripId = ++Configuration.MaxBusOnTripId;
                busOnTrip.IsDeleted = false;

                busOnTrip.LicenseNumber = busesList[i + 10].LicenseNumber;
                busOnTripsList.Add(busOnTrip);
            }

            for (int i = 0; i < 10; i++)
            {
                BusOnTrip busOnTrip = new BusOnTrip();
                TimeSpan actualTakeOff;
                busOnTrip.PlannedTakeOff = new TimeSpan(5 + (i / 2), i + 1, i + 4);
                if (i % 2 == 0)
                {
                    actualTakeOff = new TimeSpan(5 + (i / 2), i + 1, i + 50);
                }
                else
                {
                    actualTakeOff = new TimeSpan(5 + (i / 2), i + 6, i + 7);
                }
                int prevStationRandom = RandomPrevStation.Next(1, 50);
                busOnTrip.PrevStationAt = new TimeSpan(5 + (i / 2), i + 1, i + 4);
                busOnTrip.NextStationAt = new TimeSpan(5 + (i / 2), i + 7, i + 9);
                busOnTrip.PrevStation = prevStationRandom;
                busOnTrip.LicenseNumber = busesList[i].LicenseNumber;
                busOnTrip.LineId = linesList[i].LineId;
                busOnTrip.ActualTakeOff = actualTakeOff;
                busOnTrip.BusOnTripId = ++Configuration.MaxBusOnTripId;
                busOnTrip.IsDeleted = false;

                busOnTrip.LicenseNumber = busesList[i + 10].LicenseNumber;
                busOnTripsList.Add(busOnTrip);
            }

        }
        #endregion Initialize BusOnTrip

        #region Initialize LineTrip 
        private static void InitializeLineTripList()
        {
            lineTripsList = new List<LineTrip>();

            for (int i = 0; i < 10; i++)
            {
                createLineTrip(i);
            }

            for (int i = 0; i < 10; i++)
            {
                createLineTrip(i);
            }

        }

        private static void createLineTrip(int i)
        {
            LineTrip lineTrip = new LineTrip();
            lineTrip.LineTripId = ++Configuration.MaxLineTripId;
            lineTrip.LineId = linesList[i].LineId;
            TimeSpan startAt = new TimeSpan(5 + (i / 2), i + 1, i + 4);
            lineTrip.StartAt = startAt;
            TimeSpan frequency = new TimeSpan(i / 2, 0, 0);
            lineTrip.Frequency = frequency;
            TimeSpan temp = new TimeSpan(0, 0, 0);
            if (lineTrip.Frequency > temp)
            {
                TimeSpan finishAt = new TimeSpan(22, i + 58, i + 58);
                lineTrip.FinishAt = finishAt;
            }
            lineTripsList.Add(lineTrip);
        }

        #endregion Initialize LineTrip



    }
}
