using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    /// <summary>
    /// קו אוטובוס
    /// </summary>
    public class Line
    {
        public int LineId { get; set; } // קוד הקו
        public int LineNumber { get; set; } // LineId מספר הקו - לא חד ערכי כי הוא יש גם הלוך וגם חזור ולכן יש
        public Enums.Areas Area { get; set; }
        /// <summary>
        /// סימון שהישות נמחקה בכדי שלא נמחק אותה בפועל
        /// </summary>
        public bool IsDeleted { get; set; }
        
    }
}
