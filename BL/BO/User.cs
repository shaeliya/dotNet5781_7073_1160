using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    /// <summary>
    /// משתמש
    /// </summary>
    public class User
    {
        public string UserName { get; set; } // key 
        public string FirstName { get; set; } 
        public string LastName { get; set; } 
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
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
