﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_03B_7073_1160
{
    public class Bus: INotifyPropertyChanged
    {
        private double _fuel;
        private double _treatment;
        private DateTime _lastTreatmentDate;

        public string LicenseNumber { get; set; } //מספר רישוי
        public double Kilometrage { get; set; } //קילומטראג
        public DateTime BusStartDate { get; set; }//תאריך תחילת הפעילות
        public double Fuel
        { 
            get { return _fuel; }
            set { 
                _fuel = value;
                OnPropertyChanged(nameof(Fuel));
            }
        } //דלק
        public double Treatment { get { return _treatment; } set { _treatment = value; OnPropertyChanged(nameof(Treatment)); } } //טיפול
        public string LicenseNumberForPrint { get; set; } //מספר רישוי להדפסה 
        public DateTime LastTreatmentDate { get { return _lastTreatmentDate; } set { _lastTreatmentDate = value; OnPropertyChanged(nameof(LastTreatmentDate)); } }  //תאירך אחרון לטיפול 
        public Enum.Status Status { get; set; } //מצב האוטובוס
        public Bus(string licenseNumber, DateTime busStartDate, double kilometrage, double fuel, double treatment, DateTime lastTreatmentDate)//ctor
        {
            LicenseNumber = licenseNumber;
            BusStartDate = busStartDate;
            Kilometrage = kilometrage;
            Fuel = fuel;
            Treatment = treatment;
            LastTreatmentDate = lastTreatmentDate;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public bool CanTravel(double KilometrageForRide, out string message, out string failureReason)//The function checks whether the bus is suitable for travel
        {
            message = string.Empty;
            failureReason = string.Empty;
            if (Fuel + KilometrageForRide > 1200)
            {
                failureReason = "Fuel";
                message = "The bus can not start traveling,the bus needs to refuel";
                return false;
            }
            bool isYearPassed = IsYearPassed();
            if (Treatment + KilometrageForRide > 20000 || isYearPassed)
            {
                failureReason = "Treatment";
                message = "The bus can not start traveling,The bus needs Treatment";
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

        public override string ToString()
        {
            return "LicenseNumber: " + LicenseNumber +
                   ", BusStartDate: " + BusStartDate.ToString("dd/MM/yyyy") +
                   ", Kilometrage: " + Kilometrage +
                   ", Fuel: " + Fuel +
                   ", Treatment: " + Treatment +
                   ", LastTreatmentDate: " + LastTreatmentDate.ToString("dd/MM/yyyy");
                 
        }
        public bool TravelKilometrage(double KilometrageForRide, out string message)
        {      
            try
            {
                string reason = string.Empty;
                bool isProperBusForTravel = CanTravel(KilometrageForRide, out message, out reason);
                if (isProperBusForTravel)
                {
                    message = "The bus is ready for travel";
                    Kilometrage += KilometrageForRide;
                    Fuel += KilometrageForRide;
                    Treatment += KilometrageForRide;
                    return true;
                }
                return false;

            }
            catch (Exception ex)
            {
                message = "Error: " + ex;
                return false;
            }
        }

        public void Refuel()
        {
            Status = Enum.Status.Refueling;
            Fuel = 0;
            Status = Enum.Status.ReadyToGo;
        }

        public void Treat()
        {
            Status = Enum.Status.Treatment;
            Treatment = 0;
            LastTreatmentDate = DateTime.Now;
            Status = Enum.Status.ReadyToGo;
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
