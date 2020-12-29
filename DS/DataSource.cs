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
            adjacentStationsList = new List<AdjacentStations>();
            busesList = new List<Bus>();
            busOnTripsList = new List<BusOnTrip>();
            linesList = new List<Line>();
            stationsList = new List<Station>();
            lineStationsList = new List<LineStation>();
            lineTripsList = new List<LineTrip>();
            usersList = new List<User>();

            // פה עוברים בלולאה על כל רשימה ומאתחילים אותה

        }

    }
}
