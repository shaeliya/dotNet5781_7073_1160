using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    /// <summary>
    /// תחנה
    /// </summary>
    public class Station
    {
        public int StationId { get; set; } // קוד התחנה
        public string Name { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string Adress { get; set; }
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

        public IEnumerable<LineOfStation> LinesList { get; set; }
    }
}
