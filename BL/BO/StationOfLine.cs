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
        public int StationId { get; set; } // קוד התחנה
        public string Name { get; set; }
        public int LineStationIndex { get; set; } // סדר התחנה בקו

        /// <summary>
        /// סימון שהישות נמחקה בכדי שלא נמחק אותה בפועל
        /// </summary>
        public bool IsDeleted { get; set; }
        /// <summary>
        ///  משתמש שייצר את הישות
        /// </summary>
        public User CreateUser { get; set; }
        /// <summary>
        ///  משתמש שעדכן את הישות - לצורך תיעוד במקרה של עדכון / מחיקה
        /// </summary>
        public User UpdateUser { get; set; }

    }
}
