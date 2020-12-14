using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using dotNet5781_01_1160_7073;
namespace dotNet5781_03B_7073_1160
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /////  לפחות אוטובוס אחד יהיה לאחר תאריך טיפול הבא
    //o לפחות אוטובוס אחד יהיה קרוב נסועת הטיפול הבא
    //o לפחות אוטובוס אחד יהיה עם מעט דלק
    //o האוטובוסים המוזכרים לעיל יהיו אוטובוסים שונים!
    public partial class MainWindow : Window
    {
       
        public Bus bus;
        Random RandomLicenseNumber = new Random(DateTime.Now.Millisecond);
        public MainWindow()
        {
            DateTime busStartDate = RandomBusStartDate();
            InitializeComponent();
          
            string licenseNumber;
            if (busStartDate.Year < 2018) 
            {
                int licenseNumberRandom = RandomLicenseNumber.Next(1000000, 10000000);
                licenseNumber = licenseNumberRandom.ToString();
            }
            else
            {
                int licenseNumberRandom = RandomLicenseNumber.Next(10000000, 100000000);
                licenseNumber = licenseNumberRandom.ToString();
            }          
            Random RandomKilometrage = new Random(DateTime.Now.Millisecond);
            Random RandomFuel = new Random(DateTime.Now.Millisecond);
            Random RandomTreatment = new Random(DateTime.Now.Millisecond);
            double fuel = RandomFuel.NextDouble() * (1200.0 - 0.0) + 0.0;
            double treatment = RandomFuel.NextDouble() * (20000.0 - 0.0) + 0.0;
            double min;
            if (fuel> treatment)
            {
                 min = fuel;
            }
            else
            {
                 min = treatment;
            }
            double kilometrage = RandomKilometrage.NextDouble() * (9999999999.9 - min) + min;
            DateTime lastTreatmentDate = RandomLastTreatmentDate( busStartDate);
            Bus bus = new Bus(licenseNumber, busStartDate, kilometrage, fuel, treatment, lastTreatmentDate);
           


        }
        private Random gen = new Random();      
        DateTime RandomBusStartDate()
        {
            DateTime start = new DateTime(1995, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(gen.Next(range));
        }
        DateTime RandomLastTreatmentDate(DateTime busStartDate )
        {
            DateTime start = new DateTime(busStartDate.Year, busStartDate.Month, busStartDate.Day);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(gen.Next(range));
        }
    }
}
