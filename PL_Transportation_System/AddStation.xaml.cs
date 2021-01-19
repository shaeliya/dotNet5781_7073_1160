using BL;
using BLAPI;
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
    /// Interaction logic for AddStation.xaml
    /// </summary>
    public partial class AddStation : Window
    {
        public AddStation()
        {
            InitializeComponent();
            DataContext = this;
        }
        IBL bl = new BLImp();


          

        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            bool isValid = StationValidityCheck();
            if (isValid)
            {
                try
                {
                    BO.Station station = new BO.Station();
                    station.Name = tbName.Text;
                    station.Longitude = Convert.ToDouble(tbLongitude.Text);
                    station.Latitude = Convert.ToDouble(tbLatitude.Text);
                    station.Adress = tbAdress.Text;
                    bl.AddStation(station);
                    MessageBox.Show("Station Added Successfully!");
                    Close();
                }
                catch (BO.Exceptions.StationAlreadyExistsException)
                {
                    MessageBox.Show("Station Already Exists!");

                }
            }
        }

        private bool StationValidityCheck()
        {
            if (string.IsNullOrWhiteSpace(tbName.Text))
            {
                MessageBox.Show("Must Enter Name");
                return false;
            }
            if (string.IsNullOrWhiteSpace(tbLongitude.Text))
            {
                MessageBox.Show("Must Enter Longitude");
                return false;
            }
            bool isDouble = false;
            double doubleValue;

            isDouble = double.TryParse(tbLongitude.Text, out doubleValue);

            if (!isDouble)
            {
                MessageBox.Show("Longitude is inccorect format");
                return false;
            }
            if (string.IsNullOrWhiteSpace(tbLatitude.Text))
            {
                MessageBox.Show("Must Enter Latitude");
                return false;
            }
            isDouble = double.TryParse(tbLatitude.Text, out doubleValue);
            if (!isDouble)
            {
                MessageBox.Show("Latitude is inccorect format");
                return false;
            }
            if (string.IsNullOrWhiteSpace(tbAdress.Text))
            {
                MessageBox.Show("Must Enter Adress");
                return false;
            }
                             
            return true;
        }


            private void Cancel_Button_Click(object sender, RoutedEventArgs e)
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure you want to close the window without saving?", "Close Confirmation", System.Windows.MessageBoxButton.YesNo);

                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    Close();
                }
            }
        }
    }

