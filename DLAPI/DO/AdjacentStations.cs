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
    /// מידע על זוג תחנות עוקבות
    /// </summary>
    public class AdjacentStations
    {
        public int AdjacentStationsId { get; set; }
        public int StationId1 { get; set; }
        public int StationId2 { get; set; }
        public double Distance { get; set; }

        [XmlIgnore]
        public TimeSpan Time { get; set; }

        // XmlSerializer does not support TimeSpan, so use this property for 
        // serialization instead.
        [Browsable(false)]
        [XmlElement(DataType = "duration", ElementName = "Time")]
        public string TimeString
        {
            get
            {
                return XmlConvert.ToString(Time);
            }
            set
            {
                Time = string.IsNullOrEmpty(value) ?
                    TimeSpan.Zero : XmlConvert.ToTimeSpan(value);
            }
        }
        /// <summary>
        /// סימון שהישות נמחקה בכדי שלא נמחק אותה בפועל
        /// </summary>
        public bool IsDeleted { get; set; }
       
    }
}
