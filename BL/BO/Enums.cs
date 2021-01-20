using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    /// <summary>
    /// אזורי תחנות
    /// </summary>
    public enum Areas
    {
        General,
        North,
        South,
        Center,
        Jerusalem,
        TelAviv,
        Haifa
    }
    public enum BusStatuses
    {
        ReadyToGo,
        MidTravel,
        Refueling,
        InTreatment,
        NeedFuel,
        NeedTreatment,
        NeedFuelAndTreatment

    }
}
