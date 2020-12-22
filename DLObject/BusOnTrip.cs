using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLObject
{
    /// <summary>
    /// אוטובוס בנסיעה
    /// </summary>
    public class BusOnTrip
    {
        public int BusOnTripId { get; set; }
        public int LicenseNumber { get; set; }
        public int LineId { get; set; }
        public DateTime PlannedTakeOff { get; set; }
        public DateTime ActualTakeOff { get; set; }
        public int PrevStation { get; set; }
        public DateTime PrevStationAt { get; set; }
        public DateTime NextStationAt { get; set; }

    }
}
