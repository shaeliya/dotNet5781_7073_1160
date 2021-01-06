using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    /// <summary>
    /// תחנת הקו
    /// </summary>
    public class LineStation
    {
        public int LineStationId { get; set; } // מספר רץ אוטומטי של רשומה
        public int LineId { get; set; }
        public int StationId { get; set; }
        public int LineStationIndex { get; set; } // סדר התחנה בקו
        /// <summary>
        /// סימון שהישות נמחקה בכדי שלא נמחק אותה בפועל
        /// </summary>
        public bool IsDeleted { get; set; }
        /// <summary>
        /// שם משתמש שייצר את הישות
        /// </summary>
        public string CreateUserName { get; set; } // Foregin key from User
        /// <summary>
        /// שם משתמש שעדכן את הישות - לצורך תיעוד במקרה של עדכון / מחיקה
        /// </summary>
        public string UpdateUserName { get; set; } // Foregin key from User
    }
}
