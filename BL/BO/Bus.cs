using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    /// <summary>
    /// אוטובוס
    /// </summary>
    public class Bus
    {
        public int LicenseNumber { get; set; }
        public DateTime FromDate { get; set; }
        public double TotalTrip { get; set; }
        public double FuelRemain { get; set; }//כמה נישאר לו ליסוע עד התדלוק הבא
        public double Treatment { get; set; }//כמה הוא נסוע מאז הטיפול האחרון 
        public DateTime LastTreatmentDate { get; set; }      
        public Enums.BusStatuses Status { get; set; }
        /// <summary>
        /// סימון שהישות נמחקה בכדי שלא נמחק אותה בפועל
        /// </summary>
        public bool IsDeleted { get; set; }
       

    }
}
