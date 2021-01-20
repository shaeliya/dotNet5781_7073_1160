using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    /// <summary>
    /// קו אוטובוס
    /// </summary>
    public class Line 
    {
        public int LineId { get; set; } // קוד הקו
        public int LineNumber { get; set; } // LineId מספר הקו - לא חד ערכי כי הוא יש גם הלוך וגם חזור ולכן יש
        public Areas Area { get; set; }
        /// <summary>
        /// סימון שהישות נמחקה בכדי שלא נמחק אותה בפועל
        /// </summary>
        public bool IsDeleted { get; set; }
        public IEnumerable<StationOfLine> StationsList { get; set; }
        public IEnumerable<LineTrip> LineTripList { get; set; }

        public override string ToString()
        {
            return string.Format("line number: " + LineNumber + "\t Area: " + Area +"\t Is deleted: " +IsDeleted);
        }

       
    }
}
