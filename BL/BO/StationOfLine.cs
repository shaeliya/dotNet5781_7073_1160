using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    /// <summary>
    /// קו אוטובוס
    /// </summary>
    public class StationOfLine
    {
        public int LineStationId { get; set; } // מספר רץ אוטומטי של רשומה
        public int StationId { get; set; } // קוד התחנה
        public string Name { get; set; }
        public int LineStationIndex { get; set; } // סדר התחנה בקו
        public double DistanceToNextStation { get; set; }
        public TimeSpan TimeToNextStation { get; set; }

        /// <summary>
        /// סימון שהישות נמחקה בכדי שלא נמחק אותה בפועל
        /// </summary>
        public bool IsDeleted { get; set; }
        

    }
}
