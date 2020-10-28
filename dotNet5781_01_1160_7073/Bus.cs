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

        public Bus(string licenseNumber, string busStartDate)
        {
            LicenseNumber = licenseNumber;
            BusStartDate = DateTime.Now;
            if (BusStartDate.Year < 2018)
            {
                if (LicenseNumber.Length == 7)
                {
                    LicenseNumber = licenseNumber.Substring(0, 2) + "-" + licenseNumber.Substring(2, 3) + "-" + licenseNumber.Substring(5, 2);
                }
                else
                {
                    Console.WriteLine("Please write a number with 7 digits");
                }
            }
            else {
                if(LicenseNumber.Length == 8)
                {
                    LicenseNumber = licenseNumber.Substring(0, 3) + "-" + licenseNumber.Substring(3, 2) + "-" + licenseNumber.Substring(5, 3);
                }
                else
                {
                    Console.WriteLine("Please write a number with 8 digits");
                }
                
            }  
        }
        
    }
}
