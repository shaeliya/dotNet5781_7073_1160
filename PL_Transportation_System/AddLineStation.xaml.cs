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
        public AddLineStation(PO.Line line)
        {
            InitializeComponent();
            DataContext = this;
            this.line = line;
            rbStart.IsChecked = true;
            Stations = new ObservableCollection<PO.Station>(bl.GetAllStation().Where(StationsFilter).Select(s => s.CopyPropertiesToNew(typeof(PO.Station))).Cast<PO.Station>());
            SelectedStation = Stations.FirstOrDefault();
        }

        IBL bl = new BLImp();

        private void rbStart_check(object sender, RoutedEventArgs e)
        {
            tbIndex.Visibility = Visibility.Collapsed;
            lbIndex.Visibility = Visibility.Collapsed;
            lbDistanceTo.Visibility = Visibility.Visible;
            tbDistanceTo.Visibility = Visibility.Visible;
            lbStation.Visibility = Visibility.Visible;
            cbLineStations.Visibility = Visibility.Visible;
            tbTimeTo.Visibility = Visibility.Visible;
            lbTimeTo.Visibility = Visibility.Visible;
            tbDistanceFrom.Visibility = Visibility.Collapsed;
            lbDistanceFrom.Visibility = Visibility.Collapsed;
            lbTimeFrom.Visibility = Visibility.Collapsed;
            tbTimeFrom.Visibility = Visibility.Collapsed;
            btnAddStart.Visibility = Visibility.Visible;
            btnAddMiddle.Visibility = Visibility.Collapsed;
            btnAddEnd.Visibility = Visibility.Collapsed;
        }
        private void rbMiddle_check(object sender, RoutedEventArgs e)
        {
            lbIndex.Visibility = Visibility.Visible;
            tbIndex.Visibility = Visibility.Visible;
            lbDistanceTo.Visibility = Visibility.Visible;
            tbDistanceTo.Visibility = Visibility.Visible;
            lbStation.Visibility = Visibility.Visible;
            cbLineStations.Visibility = Visibility.Visible;
            tbTimeTo.Visibility = Visibility.Visible;
            lbTimeTo.Visibility = Visibility.Visible;
            tbDistanceFrom.Visibility = Visibility.Visible;
            lbDistanceFrom.Visibility = Visibility.Visible;
            lbTimeFrom.Visibility = Visibility.Visible;
            tbTimeFrom.Visibility = Visibility.Visible;
            btnAddStart.Visibility = Visibility.Collapsed;
            btnAddMiddle.Visibility = Visibility.Visible;
            btnAddEnd.Visibility = Visibility.Collapsed;
        }
        private void rbEnd_check(object sender, RoutedEventArgs e)
        {
            tbIndex.Visibility = Visibility.Collapsed;
            lbIndex.Visibility = Visibility.Collapsed;
            lbDistanceTo.Visibility = Visibility.Collapsed;
            tbDistanceTo.Visibility = Visibility.Collapsed;
            lbStation.Visibility = Visibility.Visible;
            cbLineStations.Visibility = Visibility.Visible;
            tbTimeTo.Visibility = Visibility.Collapsed;
            lbTimeTo.Visibility = Visibility.Collapsed;
            tbDistanceFrom.Visibility = Visibility.Visible;
            lbDistanceFrom.Visibility = Visibility.Visible;
            lbTimeFrom.Visibility = Visibility.Visible;
            tbTimeFrom.Visibility = Visibility.Visible;
            btnAddStart.Visibility = Visibility.Collapsed;
            btnAddMiddle.Visibility = Visibility.Collapsed;
            btnAddEnd.Visibility = Visibility.Visible;
        }

        bool StationsFilter(BO.Station station)
        {
            return station.IsDeleted == false && line.StationsList.Any(ls => ls.StationId == station.StationId) == false;
        }

        public double DistanceToNextStation
        {
            get { return (double)GetValue(DistanceToNextStationProperty); }
            set { SetValue(DistanceToNextStationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DistanceToNextStation.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DistanceToNextStationProperty =
            DependencyProperty.Register("DistanceToNextStation", typeof(double), typeof(AddLineStation), new PropertyMetadata(0.0));



        public double DistanceFromPrevStation
        {
            get { return (double)GetValue(DistanceFromPrevStationProperty); }
            set { SetValue(DistanceFromPrevStationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DistanceFromPrevStation.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DistanceFromPrevStationProperty =
            DependencyProperty.Register("DistanceFromPrevStation", typeof(double), typeof(AddLineStation), new PropertyMetadata(0.0));


        public TimeSpan TimeToNextStation
        {
            get { return (TimeSpan)GetValue(TimeToNextStationProperty); }
            set { SetValue(TimeToNextStationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TimeToNextStation.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TimeToNextStationProperty =
            DependencyProperty.Register("TimeToNextStation", typeof(TimeSpan), typeof(AddLineStation), new PropertyMetadata(default(TimeSpan)));



        public TimeSpan TimeFromPrevStation
        {
            get { return (TimeSpan)GetValue(TimeFromPrevStationProperty); }
            set { SetValue(TimeFromPrevStationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TimeFromPrevStation.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TimeFromPrevStationProperty =
            DependencyProperty.Register("TimeFromPrevStation", typeof(TimeSpan), typeof(AddLineStation), new PropertyMetadata(default(TimeSpan)));



        public int StationIndex
        {
            get { return (int)GetValue(StationIndexProperty); }
            set { SetValue(StationIndexProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StationIndex.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StationIndexProperty =
            DependencyProperty.Register("StationIndex", typeof(int), typeof(AddLineStation), new PropertyMetadata(0));

        public ObservableCollection<PO.Station> Stations
        {
            get { return (ObservableCollection<PO.Station>)GetValue(StationsProperty); }
            set { SetValue(StationsProperty, value); }
        }
        // Using a DependencyProperty as the backing store for StationsList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StationsProperty =
            DependencyProperty.Register("Stations", typeof(ObservableCollection<PO.Station>), typeof(AddLineStation), new FrameworkPropertyMetadata(new ObservableCollection<PO.Station>()));

        private static void OnPropChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

        private PO.Line line;

        public PO.Station SelectedStation
        {
            get { return (PO.Station)GetValue(SelectedStationProperty); }
            set { SetValue(SelectedStationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Station.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedStationProperty =
            DependencyProperty.Register("SelectedStation", typeof(PO.Station), typeof(AddLineStation), new PropertyMetadata(default(PO.Station), new PropertyChangedCallback(OnPropChanged)));

        private void AddStationToLineStartClicked(object sender, RoutedEventArgs e)
        {
            AddStationToLine(1, DistanceToNextStation, TimeToNextStation, 0, new TimeSpan());
        }

        private void AddStationToLineMiddleClicked(object sender, RoutedEventArgs e)
        {
            AddStationToLine(StationIndex, DistanceToNextStation, TimeToNextStation, DistanceFromPrevStation, TimeFromPrevStation);
        }
        private void AddStationToLineEndClicked(object sender, RoutedEventArgs e)
        {
            AddStationToLine(line.StationsList.Count + 1, 0, new TimeSpan(), DistanceFromPrevStation, TimeFromPrevStation);
        }

        private void AddStationToLine(int index, double distanceToNextStation, TimeSpan timeToNextStation, double distanceFromPrevStation, TimeSpan timeFromPrevStation)
        {
            bool isValid = LineStationValidityCheck(index, distanceToNextStation, timeToNextStation, distanceFromPrevStation, timeFromPrevStation);
            if (isValid)
            {
                try
                {
                    var stationOfLine = new PO.StationOfLine()
                    {
                        Name = SelectedStation.Name,
                        StationId = SelectedStation.StationId,
                        DistanceToNextStation = distanceToNextStation,
                        TimeToNextStation = timeToNextStation,
                        LineStationIndex = index
                    };

                    var lineBO = (BO.Line)line.CopyPropertiesToNew(typeof(BO.Line));
                    var stationOfLineBO = (BO.StationOfLine)stationOfLine.CopyPropertiesToNew(typeof(BO.StationOfLine));
                    lineBO.StationsList = line.StationsList.Select(s => s.CopyPropertiesToNew(typeof(BO.StationOfLine))).Cast<BO.StationOfLine>().ToList();
                    bl.AddLineStationToLine(lineBO, stationOfLineBO, distanceFromPrevStation, timeFromPrevStation);
                    lineBO = bl.GetLineById(lineBO.LineId);
                    line.StationsList = new ObservableCollection<PO.StationOfLine>(lineBO.StationsList.Select(s => s.CopyPropertiesToNew(typeof(PO.StationOfLine))).Cast<PO.StationOfLine>());
                    MessageBox.Show("Line Station Added Successfully!");
                    Close();
                }
                catch (BO.Exceptions.StationAlreadyExistsException)
                {
                    MessageBox.Show("Line station Already Exists!");

                }
                catch
                {
                    MessageBox.Show("General Error");

                }
            }
        }

        private bool LineStationValidityCheck(int index, double distanceToNextStation, TimeSpan timeToNextStation, double distanceFromPrevStation, TimeSpan timeFromPrevStation)
        {

            if (Convert.ToInt32(index) == 0 || Convert.ToInt32(index) > line.StationsList.Select(s => s.LineStationIndex).Max() + 1)
            {
                MessageBox.Show(" The index is unvalid");
                return false;
            }

            if ((distanceToNextStation == 0 || timeToNextStation == new TimeSpan()) &&
                index != line.StationsList.Count + 1)
            {
                MessageBox.Show("Must enter distance and time to next station");
                return false;
            }


            if ((distanceFromPrevStation == 0 || timeFromPrevStation == new TimeSpan()) &&
                index != 1)
            {
                MessageBox.Show("Must enter distance and time from prev station");
                return false;
            }



            return true;
        }
        private void keyCheck(object sender, KeyEventArgs e)
        {
            if (((int)e.Key < (int)Key.D0 || (int)e.Key > (int)Key.D9) && ((int)e.Key < (int)Key.NumPad0 || (int)e.Key > (int)Key.NumPad9) && e.Key != Key.OemPeriod && e.Key != Key.Escape && e.Key != Key.Back)
                e.Handled = true;
        }
        private void keyCheckWithoutPoint(object sender, KeyEventArgs e)
        {
            if (((int)e.Key < (int)Key.D0 || (int)e.Key > (int)Key.D9) && ((int)e.Key < (int)Key.NumPad0 || (int)e.Key > (int)Key.NumPad9) && e.Key != Key.Escape && e.Key != Key.Back)
                e.Handled = true;
        }

    }


}



