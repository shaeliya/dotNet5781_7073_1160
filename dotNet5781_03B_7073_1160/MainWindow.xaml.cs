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
namespace dotNet5781_03B_7073_1160
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /////  לפחות אוטובוס אחד יהיה לאחר תאריך טיפול הבא

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
            string licenseNumber;
            double fuel, treatment, kilometrage;
            for (int i = 0; i < 7; i++)
            {
                busStartDate = RandomBusStartDate();

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
                fuel = RandomFuel.NextDouble() * (1200.0 - 0.0) + 0.0;
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
                lastTreatmentDate = RandomLastTreatmentDate(busStartDate);
                licenseNumber = i.ToString();
                Bus bus = new Bus(licenseNumber, busStartDate, kilometrage, fuel, treatment, lastTreatmentDate);
                BusList.Add(bus);
            }
            for (int j = 0; j < 3; j++)
            {
                busStartDate = RandomBusStartDate();

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
                fuel = RandomFuel.NextDouble() * (1200.0 - 1150.0) + 1150.0;
                treatment = RandomTreatment.NextDouble() * (20000.0 - 19900.0) + 19900.0;
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
                lastTreatmentDate = RandomLastTreatmentDate(busStartDate);
                licenseNumber = (7 + j).ToString();
                Bus bus = new Bus(licenseNumber, busStartDate, kilometrage, fuel, treatment, lastTreatmentDate);
                BusList.Add(bus);
            }

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
        private void Open_Edit_Bus_Window_Button_Click(object sender, RoutedEventArgs e)
        {

            Button cmd = (Button)sender;
            if (cmd.DataContext is Bus)
            {
                Bus bus = (Bus)cmd.DataContext;
                EditBus editBusWindow = new EditBus();
                editBusWindow.SelectedBus = bus;
                editBusWindow.Show();

            }

        }
    }
}
