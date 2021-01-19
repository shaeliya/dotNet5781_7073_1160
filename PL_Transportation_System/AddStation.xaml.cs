using BL;
using BLAPI;
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


        private void keyCheck(object sender, KeyEventArgs e)
        {
            if (((int)e.Key < (int)Key.D0 || (int)e.Key > (int)Key.D9) && ((int)e.Key < (int)Key.NumPad0 || (int)e.Key > (int)Key.NumPad9) && e.Key != Key.OemPeriod && e.Key != Key.Escape && e.Key != Key.Back)
                e.Handled = true;
        }

        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                ValidityCheck();
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
        private bool ValidityCheck()
        {
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
