using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
//לא מחכה בנסיעה הוא לא מחשב כמ זמן הוא צריך לחכות כשולחים לנסיעה
//כששולחים לנסיעה ישר מקפיץנ= הודעה שהגענו ליעד
namespace dotNet5781_03B_7073_1160
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public ObservableCollection<Bus> BusList
        {
            get { return _busList; }
            set
            {
                _busList = value;
                OnPropertyChanged(nameof(BusList));
            }
        }
        Random RandomLicenseNumber = new Random(DateTime.Now.Millisecond);
        private Random gen = new Random();
        private ObservableCollection<Bus> _busList;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public ICommand OnClickCommand { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            InitializingVariables();
            this.DataContext = this;
        }

        private void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (var bus in e.NewItems.Cast<Bus>())
                {
                    bus.PropertyChanged += (s, ev) => { OnPropertyChanged(nameof(BusList)); };
                }
            }
        }

        private void InitializingVariables()
        {
            BusList = new ObservableCollection<Bus>();
            BusList.CollectionChanged += CollectionChanged;

            DateTime busStartDate, lastTreatmentDate;
            double fuel;
            Random RandomFuel = new Random(DateTime.Now.Millisecond);
            Bus bus;
            for (int i = 0; i < 7; i++)
            {
                fuel = RandomFuel.NextDouble() * (1200.0 - 0.0) + 0.0;
                busStartDate = RandomBusStartDate();
                lastTreatmentDate = RandomLastTreatmentDate(busStartDate);
                bus = CreateNewBus(busStartDate, lastTreatmentDate, fuel);
                BusList.Add(bus);
            }

            // אוטובוס לאחר תאריך טיפול הבא
            fuel = RandomFuel.NextDouble() * (1200.0 - 0.0) + 0.0;
            busStartDate = DateTime.Now.AddMonths(-24);
            lastTreatmentDate = DateTime.Now.AddMonths(-13);
            bus = CreateNewBus(busStartDate, lastTreatmentDate, fuel);
            BusList.Add(bus);


            // אוטובוס לאחר תאריך טיפול הבא
            fuel = RandomFuel.NextDouble() * (1200.0 - 0.0) + 0.0;
            busStartDate = DateTime.Now.AddMonths(-19);
            lastTreatmentDate = DateTime.Now.AddMonths(-14);
            bus = CreateNewBus(busStartDate, lastTreatmentDate, fuel);
            BusList.Add(bus);


            // אוטובוס קרוב לתאריך טיפול הבא
            fuel = RandomFuel.NextDouble() * (1200.0 - 0.0) + 0.0;
            busStartDate = DateTime.Now.AddMonths(-24);
            lastTreatmentDate = DateTime.Now.AddMonths(-11);
            bus = CreateNewBus(busStartDate, lastTreatmentDate, fuel);
            BusList.Add(bus);


            // אוטובוס קרוב לתאריך טיפול הבא
            fuel = RandomFuel.NextDouble() * (1200.0 - 0.0) + 0.0;
            busStartDate = DateTime.Now.AddMonths(-45);
            lastTreatmentDate = DateTime.Now.AddMonths(-10);
            bus = CreateNewBus(busStartDate, lastTreatmentDate, fuel);
            BusList.Add(bus);


            // אוטובוס עם מעט דלק
            fuel = RandomFuel.NextDouble() * (1200.0 - 1150.0) + 1150.0;
            busStartDate = RandomBusStartDate();
            lastTreatmentDate = RandomLastTreatmentDate(busStartDate);
            bus = CreateNewBus(busStartDate, lastTreatmentDate, fuel);
            BusList.Add(bus);

            // אוטובוס עם מעט דלק
            fuel = RandomFuel.NextDouble() * (1200.0 - 1150.0) + 1150.0;
            busStartDate = RandomBusStartDate();
            lastTreatmentDate = RandomLastTreatmentDate(busStartDate);
            bus = CreateNewBus(busStartDate, lastTreatmentDate, fuel);
            BusList.Add(bus);

        }

        private Bus CreateNewBus(DateTime busStartDate, DateTime lastTreatmentDate, double fuel)
        {
            string licenseNumber;
            double treatment, kilometrage;
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
            Random RandomTreatment = new Random(DateTime.Now.Millisecond);
            treatment = RandomTreatment.NextDouble() * (20000.0 - 0.0) + 0.0;
            double min;
            if (fuel > treatment)
            {
                min = fuel;
            }
            else
            {
                min = treatment;
            }
            kilometrage = RandomKilometrage.NextDouble() * (9999999999.9 - min) + min;
            Bus bus = new Bus(licenseNumber, busStartDate, kilometrage, fuel, treatment, lastTreatmentDate);
            return bus;
        }

        DateTime RandomBusStartDate()
        {
            DateTime start = new DateTime(1995, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(gen.Next(range));
        }
        DateTime RandomLastTreatmentDate(DateTime busStartDate)
        {
            DateTime start = new DateTime(busStartDate.Year, busStartDate.Month, busStartDate.Day);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(gen.Next(range));
        }

        private void Open_Add_Bus_Window_Button_Click(object sender, RoutedEventArgs e)
        {
            AddBus addBusWindow = new AddBus();
            addBusWindow.BusList = BusList;
            addBusWindow.Show();

        }
        private void Open_Travel_Window_Button_Click(object sender, RoutedEventArgs e)
        {

            Button cmd = (Button)sender;
            if (cmd.DataContext is Bus)
            {
                Bus bus = (Bus)cmd.DataContext;
                TravelWindow travelWindow = new TravelWindow();
                travelWindow.SelectedBus = bus;
                travelWindow.Show();

            }

        }

        private void FuelBus_Click(object sender, RoutedEventArgs e)
        {
            Button cmd = (Button)sender;
            if (cmd.DataContext is Bus)
            {
                Bus bus = (Bus)cmd.DataContext;
                string msg = bus.Refuel();
                MessageBox.Show(msg);
            }

        }


        private void Open_ShowBusDetails_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = (sender as ListView).SelectedItem;
            if (item is Bus)
            {
                Bus bus = (Bus)item;
                ShowBusDetails showBusDetailsWindow = new ShowBusDetails();
                showBusDetailsWindow.SelectedBus = bus;
                showBusDetailsWindow.Show();

            }

        }
    }
}
