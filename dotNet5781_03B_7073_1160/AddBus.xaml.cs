using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;


namespace dotNet5781_03B_7073_1160
{
    /// <summary>
    /// Interaction logic for AddBus.xaml
    /// </summary>
    public partial class AddBus : Window
    {
        public ObservableCollection<Bus> BusList { get; set; }
        public AddBus()
        {
            InitializeComponent();            
        }


        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
           bool isValid =  BusValidityCheck();
            if (isValid)
            {
                Bus bus = new Bus(tbLicenseNumber.Text, dpBusStartDate.SelectedDate.Value, Convert.ToDouble(tbKilometrage.Text), Convert.ToDouble(tbFuel.Text), Convert.ToDouble(tbTreatment.Text), dpLastTreatmentDate.SelectedDate.Value);

                BusList.Add(bus);

                Close();
            }
        }

        private bool BusValidityCheck()
        {
            if (string.IsNullOrWhiteSpace(tbLicenseNumber.Text))
            {
                MessageBox.Show("Must Enter License Number");
                return false;
            }

            var isExistsBus = BusList.Where(b => b.LicenseNumber.Trim() == tbLicenseNumber.Text.Trim()).ToList();
            if (isExistsBus != null && isExistsBus.Count > 0)
            {
                MessageBox.Show("Bus Already Exists");
                return false;
            }

            if (!Utils.IsDigitsOnly(tbLicenseNumber.Text))
            {
                MessageBox.Show("License Number must be digits only");
                return false;
            }

            if (!dpBusStartDate.SelectedDate.HasValue ||
                dpBusStartDate.SelectedDate.Value == DateTime.MinValue)
            {
                MessageBox.Show("Must Enter Bus Start Date");
                return false;
            }


            if (dpBusStartDate.SelectedDate.Value > DateTime.Today)
            {
                MessageBox.Show("Must Enter Bus Start Date before today");
                return false;
            }


            if (!dpLastTreatmentDate.SelectedDate.HasValue ||
                dpLastTreatmentDate.SelectedDate.Value == DateTime.MinValue)
            {
                MessageBox.Show("Must Enter Bus Last Treatment Date");
                return false;
            }

            if (dpBusStartDate.SelectedDate.Value > dpLastTreatmentDate.SelectedDate.Value)
            {
                MessageBox.Show("Bus Start Date must be before Last Treatment Date");
                return false;
            }
            if (dpLastTreatmentDate.SelectedDate.Value > DateTime.Today)
            {
                MessageBox.Show("Must Enter Bus Last Treatment Date before today");
                return false;
            }

            bool isDouble = false;
            double doubleValue;

            isDouble = double.TryParse(tbKilometrage.Text, out doubleValue);

            if (!isDouble)
            {
                MessageBox.Show("Kilometrage is inccorect format");
                return false;
            }

            isDouble = double.TryParse(tbFuel.Text, out doubleValue);
            if (!isDouble)
            {
                MessageBox.Show("Fuel is inccorect format");
                return false;
            }

            isDouble = double.TryParse(tbTreatment.Text, out doubleValue);
            if (!isDouble)
            {
                MessageBox.Show("Treatment is inccorect format");
                return false;
            }

            return true;
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
