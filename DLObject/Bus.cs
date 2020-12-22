using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLObject
{
    /// <summary>
    /// אוטובוס
    /// </summary>
    public class Bus
    {
        public int LicenseNumber { get; set; }
        public DateTime FromDate { get; set; }
        public double TotalTrip { get; set; }
        public double FuelRemain { get; set; }
        public Enums.BusStatuses Status { get; set; }

    }
}
