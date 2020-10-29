using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_01_1160_7073
{
    class Bus
    {
        private string LicenseNumber; //מספר רישוי
        private double Kilometrage; //קילומטראג
        private double Fuel; //דלק
        DateTime BusStartDate;//תאריך תחילת הפעילות

        public Bus(string licenseNumber, DateTime busStartDate)
        {
            if (licenseNumber.Length==7)
            {
                LicenseNumber = licenseNumber.Substring(0, 2) + "-" + licenseNumber.Substring(2, 3) + "-" + licenseNumber.Substring(5, 2);

            }
            else
            {
                LicenseNumber = licenseNumber.Substring(0, 3) + "-" + licenseNumber.Substring(3, 2) + "-" + licenseNumber.Substring(5, 3);
            }
            BusStartDate = busStartDate;
        }

        public bool AddBus(string licenseNumber, DateTime busStartDate)
        {

        }

    }
}
