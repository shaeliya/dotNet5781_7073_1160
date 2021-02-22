using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace DO
{
    /// <summary>
    /// יציאת קו - מסלול
    /// </summary>
    public class LineTrip
    {
        public int LineTripId { get; set; }
        public int LineId { get; set; }

        [XmlIgnore]
        public TimeSpan StartAt { get; set; }
        // XmlSerializer does not support TimeSpan, so use this property for 
        // serialization instead.
        [Browsable(false)]
        [XmlElement(DataType = "duration", ElementName = "StartAt")]
        public string StartAtString
        {
            get
            {
                return XmlConvert.ToString(StartAt);
            }
            set
            {
                StartAt = string.IsNullOrEmpty(value) ?
                    TimeSpan.Zero : XmlConvert.ToTimeSpan(value);
            }
        }
        /// <summary>
        /// סימון שהישות נמחקה בכדי שלא נמחק אותה בפועל
        /// </summary>
        public bool IsDeleted { get; set; }
       
    }
}
