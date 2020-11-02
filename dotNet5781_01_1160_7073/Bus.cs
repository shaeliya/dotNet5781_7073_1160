using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
namespace dotNet5781_01_1160_7073
{
    class Bus
    {
        public string LicenseNumber { get; set; } //מספר רישוי
        public double Kilometrage { get; set; } //קילומטראג
        public DateTime BusStartDate { get; set; }//תאריך תחילת הפעילות
        public double Fuel { get; set; } //דלק
        public double Treatment { get; set; } //טיפול
        public string LicenseNumberForPrint { get; set; }
        public Bus(string licenseNumber, DateTime busStartDate)
        {
            if (licenseNumber.Length == 7)
            {
                LicenseNumber = licenseNumber;
            }
            else
            {
                LicenseNumber = licenseNumber;
            }
            BusStartDate = busStartDate;
        }

        public bool IsProperBusForTravel(string liceseNumber, double KilometrageForRide)
        {
           
            bool isBusFound = IsBusFound(liceseNumber);

            if (Fuel + KilometrageForRide > 1200)
            {
                Console.WriteLine("The bus can not start traveling,the bus needs to refuel");
                return false;
            }
            bool isYearPassed = IsYearPassed();
            if (Kilometrage + KilometrageForRide > 20000 || isYearPassed)
            {
                Console.WriteLine("The bus can not start traveling,The bus needs care");
                return false;
            }
            return true;

        }
        public bool IsYearPassed()
        {
            DateTime zeroTime = new DateTime(1, 1, 1);

            var diff = DateTime.Now - BusStartDate;
            int years = (zeroTime + diff).Year - 1;
            if (years >= 1)
            {
                return true;

            }
            return false;
        }
        public bool IsBusFound(string liceseNumber)
        {
            if (LicenseNumber != liceseNumber)
            {
                return false;
            }
            return true;
        }
        public string print(string licenseNumber)
        {

            if (licenseNumber.Length == 7)
            {
                LicenseNumberForPrint = licenseNumber.Substring(0, 2) + "-" + licenseNumber.Substring(2, 3) + "-" + licenseNumber.Substring(5, 2);
            }
            else
            {
                LicenseNumberForPrint = licenseNumber.Substring(0, 3) + "-" + licenseNumber.Substring(3, 2) + "-" + licenseNumber.Substring(5, 3);
            }
            return LicenseNumberForPrint;
        }
    }
}
