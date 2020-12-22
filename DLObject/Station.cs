using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLObject
{
    /// <summary>
    /// תחנה
    /// </summary>
    public class Station
    {
        public int StationId { get; set; } // קוד התחנה
        public string Name { get; set; }
        public int Longtitude { get; set; }
        public int Latitude { get; set; }
        public string Adress { get; set; }
    }
}
