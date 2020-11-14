using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_7073_1160
{
  // תחנה
    class BusStop
    {
        Random RandomLongitude = new Random(DateTime.Now.Millisecond);
        Random RandomLatitude = new Random(DateTime.Now.Millisecond);
        public string BusStationKey { get; set; } // קוד תחנה
        public double Latitude { get; set; } //קו רוחב
        public double Longitude { get; set; } //קו אורך
        public string StationAddress { get; set; } //כתובת התחנה
        public BusStop(string busStationKey, string stationAddress)
        {
            Latitude = RandomLatitude.NextDouble() * (33.4 - 30.9) + 30.9;
            Longitude = RandomLongitude.NextDouble() * (35.6 - 34.2) + 34.2;
            BusStationKey = busStationKey;
            StationAddress = stationAddress;
        }
        public BusStop()
        {
        }
        public override string ToString()
        {
            return "Bus Station Code: " + BusStationKey + "," + Latitude+ "°N" + Longitude + "°E";
        }              
    }
    
}
