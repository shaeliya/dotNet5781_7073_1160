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
        // מרחק וזמן  לתחנה הבאה
        //כל הקווים העוברים בתחנה זאת
        //זמני אמת של הקו בתחנה
        public BusStop BusStop { get; set; }
        public double DistanceFromPreviousBusStop { get; set; }
        public TimeSpan TravelTimeFromPrevioussBusStop { get; set; }

        public BusLineStation(TimeSpan travelTimeFromPrevioussBusStop, double distanceFromPreviousBusStop, BusStop busStop)
        {
            DistanceFromPreviousBusStop = distanceFromPreviousBusStop;
            TravelTimeFromPrevioussBusStop = travelTimeFromPrevioussBusStop;
            BusStop = busStop;
        }
        public BusLineStation()
        {
        }


    }
}
