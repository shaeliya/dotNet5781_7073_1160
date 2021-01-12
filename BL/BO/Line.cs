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
    public class Line : INotifyPropertyChanged‏

    {
        private int lineNumber;
        private Enums.Areas area;
        public int LineId { get; set; } // קוד הקו
        public int LineNumber// LineId מספר הקו - לא חד ערכי כי הוא יש גם הלוך וגם חזור ולכן יש
        {
            get
            {
                return lineNumber;
            }
            set
            {
                lineNumber = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("LineNumber"));
            }
        }
        public Enums.Areas Area
        {
            get
            {
                return area;
            }
            set
            {
                area = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Area"));
            }
        }
        /// <summary>
        /// סימון שהישות נמחקה בכדי שלא נמחק אותה בפועל
        /// </summary>
        public bool IsDeleted { get; set; }
        public IEnumerable<StationOfLine> StationsList { get; set; }
        public IEnumerable<LineTrip> LineTripList { get; set; }

        public override string ToString()
        {
            return string.Format("line number: " + lineNumber + "\t Area: " + area);
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
