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
    /// Interaction logic for AddLineStation.xaml
    /// </summary>
    public partial class AddLineStation : Window
    {
        public AddLineStation()
        {
            InitializeComponent();

        }
        IBL bl = new BLImp();

        private void show_Line_stations_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
                       ShowLineStation((cbLineStations.SelectedValue as BO.Station).ToString());
        }
        private void ShowLineStation(string index)
        {
          IEnumerable < BO.Station> currentDisplayLineStation = (IEnumerable<BO.Station>)bl.GetAllStation().Select(s => s.IsDeleted == false);
           // DataContext = new ObservableCollection<BO.Station>(.StationsList);

            //cbLineStations.DataContext = currentDisplayLineStation;
            //lbBusLineStations.DataContext = currentDisplayLineStation.Stations;
        }

    }
}
