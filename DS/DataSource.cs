using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DS
{
    public static class DataSource
    {
        public static List<AdjacentStations> adjacentStationsList;
        public static List<Bus> busesList;
        public static List<BusOnTrip> busOnTripsList;
        public static List<Line> linesList;
        public static List<LineStation> lineStationsList;
        public static List<LineTrip> lineTripsList;
        public static List<Station> stationsList;
        public static List<User> usersList;
        static DataSource()
        {
            InitializeAllLists();
        }
        static void InitializeAllLists()
        {
            InitializeBus();         
            InitializeStationsList();
            InitializeAdjacentStationsList();
            InitializeLineList();
            InitializeLineTripList();
            InitializeBusOnTripList();
            InitializeLineStationList();
            InitializeUser();
        }

        #region Initialize Bus 
        
        //UpdateUserName -לא אתחלנו כאן את 
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
            bus.CreateUserName = "orstavsk1";
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

        //UpdateUserName-לא אתחלנו כאן את     
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
                station.CreateUserName = "shaeliya2";
                stationsList.Add(station);
            }
        }

        #endregion Initialize Station

        #region Initialize AdjacentStations

        //UpdateUserName-לא אתחלנו כאן את
        private static void InitializeAdjacentStationsList()
        {
            adjacentStationsList = new List<AdjacentStations>();
            Random RandomDistance = new Random(DateTime.Now.Millisecond);           
            Random RandomStationId1 = new Random(DateTime.Now.Millisecond);
            Random RandomStationId2 = new Random(DateTime.Now.Millisecond);

            for (int i = 0; i < 100; i++)
            {
                AdjacentStations adjacentStations = new AdjacentStations();              
                double distance = RandomDistance.NextDouble() * (10.5 - 0.5) + 0.5;
                TimeSpan time = new TimeSpan(0,  i + 2, i + 5);
                adjacentStations.CreateUserName = "orstavsk1";
                adjacentStations.Distance = distance;
                adjacentStations.Time = time;
                adjacentStations.IsDeleted = false;               
                adjacentStations.AdjacentStationsId =++ Configuration.MaxAdjacentStationsId;
                int stationId1Random = RandomStationId1.Next(1, 50);
                int stationId2Random = stationId1Random;
                while (stationId1Random == stationId2Random)
                {
                    stationId2Random = RandomStationId2.Next(1, 50);
                }
                adjacentStations.StationId1 = stationId1Random;
                adjacentStations.StationId2 = stationId2Random;

            }
        }

        #endregion Initialize AdjacentStations
       
        #region Initialize Line
       
        //UpdateUserName-לא אתחלנו כאן את
        private static void InitializeLineList()
    {
            linesList = new List<Line>();
            Random RandomArea = new Random(DateTime.Now.Millisecond);
            Random RandomfirstStationId = new Random(DateTime.Now.Millisecond);
            Random RandomlastStationId = new Random(DateTime.Now.Millisecond);
            for (int i = 1; i < 11; i++)
            {
                int area = RandomArea.Next(0, 8);
                Line line = new Line();
                line.LineNumber = i;
                line.Area = (Enums.Areas) area;
                line.LineId = ++Configuration.MaxLineId;
                line.IsDeleted = false;
                line.CreateUserName = "orstavsk1";                
                int firstStationIdRandom = RandomfirstStationId.Next(1, 50);
                int lastStationIdRandom = firstStationIdRandom;
                while (lastStationIdRandom == firstStationIdRandom)
                {
                    lastStationIdRandom = RandomlastStationId.Next(1, 50);
                }
                line.FirstStationId = firstStationIdRandom;
                line.LastStationId = lastStationIdRandom;
            }
        }

        #endregion Initialize Line

        #region Initialize LineStation 

        //UpdateUserName-לא אתחלנו כאן את 
        private static void InitializeLineStationList()
        {
            lineStationsList = new List<LineStation>();
            int temp = 0;
            LineStation lineStation = new LineStation();
            for (int i = 0; i < 10; i++)//קווים
            {             
                for (int j = 0; j < 10; j++)//תחנות
                {                                                          
                    lineStation.LineStationIndex = i + 1;
                    lineStation.LineStationId = ++Configuration.MaxLineStationId;
                    lineStation.LineId = linesList[i].LineId;
                    lineStation.StationId = stationsList[j+temp].StationId;
                    lineStation.IsDeleted = false;
                    lineStation.CreateUserName = "shaeliya2";
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

        //UpdateUserName-לא אתחלנו כאן את
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
                busOnTrip.CreateUserName = "shaeliya2";
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
                busOnTrip.CreateUserName = "orstavsk1";
                busOnTrip.LicenseNumber = busesList[i + 10].LicenseNumber;
                busOnTripsList.Add(busOnTrip);
            }

        }
        #endregion Initialize BusOnTrip

        #region Initialize LineTrip 
        private static void InitializeLineTripList()
        {
            lineTripsList = new List<LineTrip>();

            for (int i = 0; i < 20; i++)
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

        }

        #endregion Initialize LineTrip

        #region Initialize User 

        //UpdateUserName-לא אתחלנו כאן את
        private static void InitializeUser()
        {
            usersList = new List<User>();
            User user1 = new User();
            user1.FirstName = "orit";
            user1.LastName = "stavsky";
            user1.UserName = "orstavsk1";
            user1.Password = "or1234";
            user1.IsAdmin = true;
            user1.IsDeleted = false;
            user1.CreateUserName = "orstavsk1";
            usersList.Add(user1);
            User user2 = new User();
            user2.FirstName = "shalhevet";
            user2.LastName = "eliyahu";
            user2.UserName = "shaeliya2";
            user2.Password = "sh4567";
            user2.IsAdmin = true;
            user2.IsDeleted = false;
            user2.CreateUserName = "shaeliya2";
            usersList.Add(user2);
            User user3 = new User();
            user3.FirstName = "dan";
            user3.LastName = "zilbershtein";
            user3.UserName = "daz3";
            user3.Password = "d026Z";
            user3.IsAdmin = false;
            user3.CreateUserName = "daz3";
            usersList.Add(user3);
            User user4 = new User();
            user4.FirstName = "orit";
            user4.LastName = "rozenblit";
            user4.UserName = "rozority52";
            user4.Password = "roz46";
            user4.CreateUserName = "orstavsk1";
            user4.IsAdmin = false;
            user4.IsDeleted = false;
            usersList.Add(user4);
            User user5 = new User();
            user5.FirstName = "oshri";
            user5.LastName = "cohen";
            user5.UserName = "osh63";
            user5.Password = "Os782";
            user5.IsAdmin = false;
            user5.IsDeleted = false;
            user5.CreateUserName = "shaeliya2";
            usersList.Add(user5);
        }
        #endregion Initialize User

    }
}
