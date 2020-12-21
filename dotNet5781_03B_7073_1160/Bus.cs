using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace dotNet5781_03B_7073_1160
{
    public class Bus: INotifyPropertyChanged
    {
        private const int DurationOfRefuling = 12;
        private const int DurationOfTreatment = 144;
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
        public int DurationOfUnavailability { get { return _durationOfUnavailability; } set { _durationOfUnavailability = value; OnPropertyChanged(nameof(DurationOfUnavailability)); } }  
        public Enum.Status Status { get { return _status; } set 
            { 
                _status = value;
                OnPropertyChanged(nameof(Status));
            } } //מצב האוטובוס

        private DispatcherTimer _statusTimer;
        private Enum.Status _status;
        private double KilometersToTravel;
        private int _durationOfUnavailability;

        public Bus(string licenseNumber, DateTime busStartDate, double kilometrage, double fuel, double treatment, DateTime lastTreatmentDate)//ctor
        {
            LicenseNumber = licenseNumber;
            BusStartDate = busStartDate;
            Kilometrage = kilometrage;
            Fuel = fuel;
            Treatment = treatment;
            LastTreatmentDate = lastTreatmentDate;
            _statusTimer = new DispatcherTimer();
            _statusTimer.Tick += StatusTimerTick;
            UpdateStaus();

        }

        private void UpdateStaus()
        {
            if (Status == Enum.Status.MidTravel ||
                Status == Enum.Status.Refueling ||
                Status == Enum.Status.InTreatment)
            {
                return;
            }

            bool isNeedFuel = false;
            if (Fuel > 1200)
            {
                isNeedFuel = true;
            }

            bool isNeedTreatment = false;

            bool isYearPassed = IsYearPassed();
            if (Treatment > 20000 || isYearPassed)
            {
                isNeedTreatment = true;
            }

            if (isNeedFuel && isNeedTreatment)
            {
                Status = Enum.Status.NeedFuelAndTreatment;
                return;
            }
            if (isNeedTreatment)
            {
                Status = Enum.Status.NeedTreatment;
                return;

            }
            if (isNeedFuel)
            {
                Status = Enum.Status.NeedFuel;
                return;

            }

            Status = Enum.Status.ReadyToGo;

        }

        public event PropertyChangedEventHandler PropertyChanged;

        public bool CanTravel(double KilometrageForRide, out string message, out string failureReason)//The function checks whether the bus is suitable for travel
        {
            message = string.Empty;
            failureReason = string.Empty;

            UpdateStaus();

            switch(Status)
            {
                case Enum.Status.NeedTreatment:
                    failureReason = "Treatment";
                    message = "The bus can not start traveling,The bus needs Treatment";
                    return false;
                case Enum.Status.NeedFuel:
                    failureReason = "Fuel";
                    message = "The bus can not start traveling,the bus needs to refuel";
                    return false;
                case Enum.Status.NeedFuelAndTreatment:
                    failureReason = "Treatment";
                    message = "The bus can not start traveling,the bus needs Treatment and to refuel";
                    return false;
                case Enum.Status.InTreatment:
                    failureReason = string.Empty;
                    message = "The bus can not start traveling while in treatment";
                    return false;
                case Enum.Status.Refueling:
                    failureReason = string.Empty;
                    message = "The bus can not start traveling while refueling";
                    return false;
                case Enum.Status.MidTravel:
                    failureReason = string.Empty;
                    message = "The bus can not start traveling. It is already mid travel";
                    return false;
            }

            bool isYearPassed = IsYearPassed();
            if (Treatment + KilometrageForRide > 20000 || isYearPassed)
            {
                failureReason = "Treatment";
                message = "The bus can not travel this distance because it would need Treatment midway";
                return false;
            }

            if (Fuel + KilometrageForRide > 1200)
            {
                failureReason = "Fuel";
                message = "The bus can not travel this distance because it wouldn't have enough fuel";
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
        public bool TravelKilometrage(double kilometresForRide, out string message)
        {      
            try
            {
                string reason = string.Empty;
                bool isProperBusForTravel = CanTravel(kilometresForRide, out message, out reason);
                if (isProperBusForTravel)
                {
                    message = "The bus is ready for travel";
                    KilometersToTravel = kilometresForRide;
                    Status = Enum.Status.MidTravel;
                    Random RandomSpeed = new Random(DateTime.Now.Millisecond);
                    double speed = RandomSpeed.NextDouble() * (50.0 - 20.0) + 20.0;
                    double travelDuration = 3600 * kilometresForRide / speed;
                    DurationOfUnavailability = (int)travelDuration / 600;
                    _statusTimer.Interval = TimeSpan.FromSeconds(DurationOfUnavailability);
                    _statusTimer.Tag = new Action(FinishTravel);
                    _statusTimer.Start();
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


        private void FinishTravel()
        {
            Status = Enum.Status.ReadyToGo;
            Kilometrage += KilometersToTravel;
            Fuel += KilometersToTravel;
            Treatment += KilometersToTravel;
            DurationOfUnavailability = 0;
            UpdateStaus();
        }

        public string Refuel()
        {
            UpdateStaus();


            switch (Status)
            {
                case Enum.Status.InTreatment:
                    return "The bus can not start fueling while in treatment";
                case Enum.Status.Refueling:
                    return "The bus can not start fueling while refueling";
                case Enum.Status.MidTravel:
                    return "The bus can not start fueling mid travel";
                default:
                    break;
            }

            if (Fuel > 0)
            {
                Status = Enum.Status.Refueling;
                DurationOfUnavailability = DurationOfRefuling;
                _statusTimer.Interval = TimeSpan.FromSeconds(DurationOfRefuling);
                _statusTimer.Tag = new Action(FinishRefuel);
                _statusTimer.Start();
                return "Refuling...";
            }

            return "The bus doesn't need Refueling";

        }
        private void FinishRefuel()
        {
            Status = Enum.Status.ReadyToGo;
            DurationOfUnavailability = 0;
            Fuel = 0;
            UpdateStaus();
        }

        private void StatusTimerTick(object sender, EventArgs e)
        {
            _statusTimer.Stop();
            Status = Enum.Status.ReadyToGo;
            ((Action)_statusTimer.Tag)();
        }

        public string Treat()
        {
            UpdateStaus();

            switch (Status)
            {
                case Enum.Status.InTreatment:
                    return "The bus can not start treatment while in treatment";
                case Enum.Status.Refueling:
                    return "The bus can not start treatment while refueling";
                case Enum.Status.MidTravel:
                    return "The bus can not start treatment mid travel";
                default:
                    break;
            }

            if (Treatment > 0)
            {
                Status = Enum.Status.InTreatment;
                DurationOfUnavailability = DurationOfTreatment;
                _statusTimer.Interval = TimeSpan.FromSeconds(DurationOfTreatment);
                _statusTimer.Tag = new Action(FinishTreatment);
                _statusTimer.Start();
                return "Treating...";
            }

            // גם אם לא צריך טיפול אך צריך תדלוק בלבד - נתדלק אותו
            if (Status == Enum.Status.NeedFuel)
            {
                return Refuel();
            }

            return "The bus doesn't need Treatment";


        }

        private void FinishTreatment()
        {
            DurationOfUnavailability = 0;
            Status = Enum.Status.ReadyToGo;
            Treatment = 0;
            LastTreatmentDate = DateTime.Now;
            UpdateStaus();

            if(Status == Enum.Status.NeedFuel)
            {
                Refuel();
            }

        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
