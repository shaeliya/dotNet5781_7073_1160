using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_7073_1160
{
    //תחנה של  קו ספציפי
    public class BusLineStation
    {
        public BusStop BusStop { get; set; }
        public double DistanceFromPreviousBusStop { get; set; }
        public TimeSpan TravelTimeFromPreviousBusStop { get; set; }
        
        public BusLineStation(TimeSpan travelTimeFromPrevioussBusStop, double distanceFromPreviousBusStop, BusStop busStop)
        {
            DistanceFromPreviousBusStop = distanceFromPreviousBusStop;
            TravelTimeFromPreviousBusStop = travelTimeFromPrevioussBusStop;
            BusStop = busStop;
        }
        public BusLineStation()
        {
        }
        public override string ToString()
        {
            return BusStop.ToString() + "       " + "Travel time from previous bus stop: " +TravelTimeFromPreviousBusStop;
        }


    }
}
