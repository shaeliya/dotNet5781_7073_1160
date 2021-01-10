using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    /// <summary>
    /// מידע על זוג תחנות עוקבות
    /// </summary>
    public class AdjacentStations
    {
        public int AdjacentStationsId { get; set; }
        public int StationId1 { get; set; }
        public int StationId2 { get; set; }
        public double Distance { get; set; }
        public TimeSpan Time { get; set; }
        /// <summary>
        /// סימון שהישות נמחקה בכדי שלא נמחק אותה בפועל
        /// </summary>
        public bool IsDeleted { get; set; }
       
    }
}
