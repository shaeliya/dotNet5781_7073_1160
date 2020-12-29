using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    /// <summary>
    /// תחנה
    /// </summary>
    public class Station
    {
        public int StationId { get; set; } // קוד התחנה
        public string Name { get; set; }
        public int Longtitude { get; set; }
        public int Latitude { get; set; }
        public string Adress { get; set; }
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
