using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_7073_1160
{
    //תחנה של  קו ספציפי
    class BusLineStation
    {
        public BusLineStation(TimeSpan travelTimeFromPrevioussBusStop, double distanceFromPreviousBusStop)
        {
            TravelTimeFromPrevioussBusStop = travelTimeFromPrevioussBusStop;
            DistanceFromPreviousBusStop = distanceFromPreviousBusStop;
        }
        // מרחק וזמן  לתחנה הבאה
        //כל הקווים העוברים בתחנה זאת
        //זמני אמת של הקו בתחנה
        public BusStop BusStop { get; set; } = new BusStop();
        public double DistanceFromPreviousBusStop { get; set; }
        public TimeSpan TravelTimeFromPrevioussBusStop{ get; set; }

    }
}
