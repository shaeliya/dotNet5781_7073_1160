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
    /// Interaction logic for StationDetailsWindow.xaml
    /// </summary>
    public partial class StationDetailsWindow : Window
    {
        //IBL bl = new BLImp();
        public BO.Station SelectedStation = new BO.Station();

        public StationDetailsWindow(BO.Station selectedStation)
        {
            InitializeComponent();
            SelectedStation = selectedStation;
            lvUpdateStation.ItemsSource = new ObservableCollection<BO.LineOfStation>(SelectedStation.LinesList);
        }

        
        private void ChangeAddressStation(object sender, RoutedEventArgs e)
        {

        }

        private void ChangeNameStation(object sender, RoutedEventArgs e)
        {

        }
    }
}
