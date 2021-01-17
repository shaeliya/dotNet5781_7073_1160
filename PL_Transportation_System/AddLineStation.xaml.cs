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
            DataContext = this;
            Stations = new ObservableCollection<PO.Station>(bl.GetAllStation().Select(s => s.CopyPropertiesToNew(typeof(PO.Station))).Cast<PO.Station>());
        }
        IBL bl = new BLImp();

        public ObservableCollection<PO.Station> Stations
        {
            get { return (ObservableCollection<PO.Station>)GetValue(StationsProperty); }
            set { SetValue(StationsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StationsList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StationsProperty =
            DependencyProperty.Register("Stations", typeof(ObservableCollection<PO.Station>), typeof(AddLineStation), new FrameworkPropertyMetadata(new ObservableCollection<PO.Station>()));

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
