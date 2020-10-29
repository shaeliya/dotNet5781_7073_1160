﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
namespace dotNet5781_01_1160_7073
{
    class Bus
    {
        public string LicenseNumber { get; set; } //מספר רישוי
        public  double Kilometrage { get; set; } //קילומטראג
        public DateTime BusStartDate { get; set; }//תאריך תחילת הפעילות
        public double Fuel { get; set; } //דלק
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

        public bool IsProperBusForTravel(string liceseNumber, double KilometrageForRide)
        {
            if (LicenseNumber != liceseNumber)
            {
                Console.WriteLine("No bus found, with the license number you entered");
                return false;
            }
            
            if (Fuel + KilometrageForRide > 1200)
            {
                Console.WriteLine("The bus can not start traveling,the bus needs to refuel");
                return false;
            }
            bool isYearPassed = IsYearPassed();
            if (Kilometrage + KilometrageForRide > 20000|| isYearPassed)
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
    }
}