using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
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
        /// שם משתמש שייצר את הישות
        /// </summary>
        public bool CreateUserName { get; set; } // Foregin key from User
        /// <summary>
        /// שם משתמש שעדכן את הישות - לצורך תיעוד במקרה של עדכון / מחיקה
        /// </summary>
        public bool UpdateUserName { get; set; } // Foregin key from User
    }
}
