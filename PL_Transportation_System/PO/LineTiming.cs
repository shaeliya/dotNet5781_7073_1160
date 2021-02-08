using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL_Transportation_System.PO
{
   public class LineTiming
    {
        public int LineTimingId;
        public TimeSpan TripStart { get; set; } //time of Line start the trip, taken from StartAt of LineTrip
        public int LineId { get; set; } //Line ID from Line
        public int LineNumber { get; set; } //Line Number as understood by the people
        public string LastStation { get; set; }// Last station name - so the passengers will know better which direction it is
        public TimeSpan ExpectedTimeTillArrive { get; set; }//Expected time of arrival

    }
}
