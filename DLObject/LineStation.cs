using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLObject
{
    /// <summary>
    /// תחנת הקו
    /// </summary>
    public class LineStation
    {
        public int LineStationId { get; set; }
        public int LineId { get; set; }
        public int StationId { get; set; }
        public int LineStationIndex { get; set; }
        public int PrevStationId { get; set; }
        public int NextStationId { get; set; }
    }
}
