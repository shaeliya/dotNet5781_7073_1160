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
            InitializeUser();

            // פה עוברים בלולאה על כל רשימה ומאתחילים אותה
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

        adjacentStationsList = new List<AdjacentStations>();
        busOnTripsList = new List<BusOnTrip>();
            linesList = new List<Line>();
            stationsList = new List<Station>();
            lineStationsList = new List<LineStation>();
            lineTripsList = new List<LineTrip>();

            #region Initialize User 
             private static void InitializeUser()
        {
            usersList = new List<User>();
            User user = new User();
            user.FirstName = "orit";
            user.LastName = "stavsky";
            user.UserName = "orstavsk1";
            user.Password = "or1234";
            user.IsAdmin = true;
            usersList.Add(user);            
            user.FirstName = "shalhevet";
            user.LastName = "eliyahu";
            user.UserName = "shaeliya2";
            user.Password = "sh4567";
            user.IsAdmin = true;
            usersList.Add(user);
            user.FirstName = "dan";
            user.LastName = "zilbershtein";
            user.UserName = "daz3";
            user.Password = "d026Z";
            user.IsAdmin = false;
            usersList.Add(user);
            user.FirstName = "orit";
            user.LastName = "rozenblit";
            user.UserName = "rozority52";
            user.Password = "roz46";
            user.IsAdmin = false;
            usersList.Add(user);
            user.FirstName = "oshri";
            user.LastName = "cohen";
            user.UserName = "osh63";
            user.Password = "Os782";
            user.IsAdmin = false;
            usersList.Add(user);
        }
        #endregion Initialize User

    }

}
