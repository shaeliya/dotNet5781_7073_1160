//Shalhevet Eliyahu 211661160
//Orit Stavsky 212507073
using System;
namespace dotNet5781_01_1160_7073
{
    class Bus
    {
        public string LicenseNumber { get; set; } //מספר רישוי
        public double Kilometrage { get; set; } //קילומטראג
        public DateTime BusStartDate { get; set; }//תאריך תחילת הפעילות
        public double Fuel { get; set; } //דלק
        public double Treatment { get; set; } //טיפול
        public string LicenseNumberForPrint { get; set; } //מספר רישוי להדפסה 
        public DateTime LastTreatmentDate { get; set; } //תאירך אחרון לטיפול 
        public Bus(string licenseNumber, DateTime busStartDate, double kilometrage,double fuel, double treatment, DateTime lastTreatmentDate)//ctor
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
            Kilometrage = kilometrage;
            Fuel = kilometrage;
            Treatment = kilometrage;
            LastTreatmentDate = lastTreatmentDate;
        }

        public bool IsProperBusForTravel(string liceseNumber, double KilometrageForRide)//The function checks whether the bus is suitable for travel
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
        public bool IsYearPassed()//The function checks if a year has passed since the last treatment of the bus
        {
            DateTime zeroTime = new DateTime(1, 1, 1);

            var diff = DateTime.Now - LastTreatmentDate;
            int years = (zeroTime + diff).Year - 1;
            if (years >= 1)
            {
                return true;

            }
            return false;
        }
        public bool IsBusFound(string liceseNumber)//The function checks if the bus is in the system
        {
            if (LicenseNumber != liceseNumber)
            {
                return false;
            }
            return true;
        }
        public string PreparingLicenseNumberForPrint(string licenseNumber)//The function prepares for printing the license number according to the requested format
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
