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
    /// Interaction logic for UpdateStationWindow.xaml
    /// </summary>
    public partial class UpdateStationWindow : Window
    {
        //IBL bl = new BLImp();
        public BO.Station SelectedStation = new BO.Station();

        public UpdateStationWindow(BO.Station selectedStation)
        {
            InitializeComponent();
            SelectedStation = selectedStation;
            lvUpdateStation.ItemsSource = new ObservableCollection<BO.LineOfStation>(SelectedStation.LinesList);
        }

        private void Change_address_station_Button_Click(object sender, RoutedEventArgs e)
        {
           int station= ((BO.Station)((Button)sender).DataContext).StationId;
            ChangeStationDetails changeStationDetails = new ChangeStationDetails(station);
            changeStationDetails.Show();
        }
    }
}
