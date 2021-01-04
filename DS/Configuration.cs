using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS
{
    public static class Configuration
    {
        public static int MaxAdjacentStationsId;
        public static int MaxBusOnTripId;
        public static int MaxLineId;
        public static int MaxLineStationId;
        public static int MaxLineTripId;
        public static int MaxStationId;

        static Configuration()
        {
            MaxAdjacentStationsId = 0;
            MaxBusOnTripId = 0;
            MaxLineId = 0;
            MaxLineStationId = 0;
            MaxLineTripId = 0;
            MaxStationId = 0;

        }
    }
}
