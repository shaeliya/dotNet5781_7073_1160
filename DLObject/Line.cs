using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLObject
{
    /// <summary>
    /// קו אוטובוס
    /// </summary>
    public class Line
    {
        public int LineId { get; set; } // קוד הקו
        public int LineNumber { get; set; } // LineId מספר הקו - לא חד ערכי כי הוא יש גם הלוך וגם חזור ולכן יש
        public Enums.Areas Area { get; set; }
        public int FirstStationId { get; set; }
        public int LastStationId { get; set; }
    }
}
