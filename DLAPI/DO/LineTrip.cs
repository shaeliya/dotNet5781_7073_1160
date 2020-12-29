using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    /// <summary>
    /// יציאת קו - מסלול
    /// </summary>
    public class LineTrip
    {
        public int LineTripId { get; set; }
        public int LineId { get; set; }
        public TimeSpan StartAt { get; set; }
        public TimeSpan Frequency { get; set; }
        public TimeSpan FinishAt { get; set; }
         /// <summary>
        /// סימון שהישות נמחקה בכדי שלא נמחק אותה בפועל
        /// </summary>
        public bool IsDeleted { get; set; }
        /// <summary>
        /// שם משתמש שייצר את הישות
        /// </summary>
        public bool CreateUserName { get; set; } // Foregin key from User
        /// <summary>
        /// שם משתמש שעדכן את הישות - לצורך תיעוד במקרה של עדכון / מחיקה
        /// </summary>
        public bool UpdateUserName { get; set; } // Foregin key from User
    }
}
