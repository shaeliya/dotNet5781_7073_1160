using BL;
using BLAPI;
using BO;
using PL_Transportation_System.Utils;
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


namespace PL_Transportation_System
{
    /// <summary>
    /// Interaction logic for AddBus.xaml
    /// </summary>
    public partial class AddBus : Window
    {
        public AddBus()
        {
            InitializeComponent();            
        }

        IBL bl = new BLImp();

        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
           bool isValid =  BusValidityCheck();
            if (isValid)
            {
                try
                {
                    Bus bus = new Bus()
                    {
                        LicenseNumber = Convert.ToInt32(tbLicenseNumber.Text),
                        FromDate = tbFromDate.SelectedDate.Value,
                        TotalTrip = Convert.ToDouble(tbTotalTrip.Text),
                        FuelRemain = Convert.ToDouble(tbFuelRemain.Text),
                        Treatment = Convert.ToDouble(tbTreatment.Text),
                        LastTreatmentDate = dpLastTreatmentDate.SelectedDate.Value
                    };

                    bl.AddBus(bus);

                    Close();
                }

                catch (BO.Exceptions.BusAlreadyExistsException)
                {
                    MessageBox.Show("Bus Already Exists");
                }
            }
        }

        private bool BusValidityCheck()
        {
            if (string.IsNullOrWhiteSpace(tbLicenseNumber.Text))
            {
                MessageBox.Show("Must Enter License Number");
                return false;
            }


            if (!UtilsFunctions.IsDigitsOnly(tbLicenseNumber.Text))
            {
                MessageBox.Show("License Number must be digits only");
                return false;
            }

            if (!tbFromDate.SelectedDate.HasValue ||
                tbFromDate.SelectedDate.Value == DateTime.MinValue)
            {
                MessageBox.Show("Must Enter Bus From Date");
                return false;
            }


            if (tbFromDate.SelectedDate.Value > DateTime.Today)
            {
                MessageBox.Show("Must Enter Bus From Date before today");
                return false;
            }


            if (!dpLastTreatmentDate.SelectedDate.HasValue ||
                dpLastTreatmentDate.SelectedDate.Value == DateTime.MinValue)
            {
                MessageBox.Show("Must Enter Bus Last Treatment Date");
                return false;
            }

            if (tbFromDate.SelectedDate.Value > dpLastTreatmentDate.SelectedDate.Value)
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

            isDouble = double.TryParse(tbTotalTrip.Text, out doubleValue);

            if (!isDouble)
            {
                MessageBox.Show("Total Trip is inccorect format");
                return false;
            }

            isDouble = double.TryParse(tbFuelRemain.Text, out doubleValue);
            if (!isDouble)
            {
                MessageBox.Show("Fuel Remain is inccorect format");
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
