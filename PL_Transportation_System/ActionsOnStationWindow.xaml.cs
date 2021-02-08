using BL;
using BLAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Interaction logic for ActionsOnStationWindow.xaml
    /// </summary>
    public partial class ActionsOnStationWindow : Window
    {
        IBL bl = new BLImp();
        public ActionsOnStationWindow()
        {
            InitializeComponent();
            DataContext = this;
            GetStations();
        }

        private void GetStations()
        {
            Stations = new ObservableCollection<PO.Station>(bl.GetAllStation().Select(s =>
            {
                var newS = (PO.Station)s.CopyPropertiesToNew(typeof(PO.Station));
                newS.LinesList = s.LinesList.Select(l => l.CopyPropertiesToNew(typeof(PO.LineOfStation))).Cast<PO.LineOfStation>().ToList();
                newS.IsUpdated = false;
                return newS;
            }).Cast<PO.Station>());
        }

        public ObservableCollection<PO.Station> Stations
        {
            get { return (ObservableCollection<PO.Station>)GetValue(StationsProperty); }
            set { SetValue(StationsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for stations.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StationsProperty =
            DependencyProperty.Register("Stations", typeof(ObservableCollection<PO.Station>), typeof(ActionsOnStationWindow), new FrameworkPropertyMetadata(new ObservableCollection<PO.Station>()));

        void RefreshLinesListView()
        {
            lvStation.ItemsSource = new ObservableCollection<BO.Station>(bl.GetAllStation());
        }
            

        private void UpdateAllClicked(object sender, RoutedEventArgs e)
        {
            var stationsToUpdate = Stations.Where(s => s.IsUpdated).Select(s => {
                var newS = (BO.Station)s.CopyPropertiesToNew(typeof(BO.Station));
                newS.LinesList = s.LinesList.Select(l => l.CopyPropertiesToNew(typeof(BO.LineOfStation))).Cast<BO.LineOfStation>().ToList();
                return newS;
            });
            foreach (var station in stationsToUpdate)
            {
                bl.UpdateStation(station);
            }

        }

        private void AddStation(object sender, RoutedEventArgs e)
        {
            AddStation addStationwindow = new AddStation();
            addStationwindow.Closing += OnCloseWindow;
            addStationwindow.Show();
        }

        private void OnCloseWindow(object sender, CancelEventArgs e)
        {
            GetStations();
        }

        private void OpenLinesOfStationsWindow_Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            if (btn.DataContext is PO.Station)
            {
                PO.Station station = (PO.Station)btn.DataContext;
                if (!station.IsDeleted)
                {
                    ShowLinesOfStation showLinesOfStation = new ShowLinesOfStation(station);
                    showLinesOfStation.Show();
                }
                else
                {
                    MessageBox.Show("Cannot update deleted station");
                }
            }

          
        }
    }
}
