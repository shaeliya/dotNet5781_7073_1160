using BL;
using BLAPI;
using PL_Transportation_System.PO;
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
    /// Interaction logic for UpdateLineWindow.xaml
    /// </summary>
    public partial class UpdateLineWindow : Window
    {
        IBL bl = new BLImp();
        Random _rand = new Random();

        public UpdateLineWindow(PO.Line selectedLine)
        {
            InitializeComponent();
            SelectedLine = selectedLine;
            MoveUpCommand = new RelayCommand(MoveUp, CanMoveUp);
            MoveDownCommand = new RelayCommand(MoveDown, CanMoveDown);
            DataContext = this;
            //    var lineTrips = bl.GetAllLine().Select(b => b.CopyPropertiesToNew(typeof(BO.LineTrip))).Cast<BO.LineTrip>().ToList();
            //    LineTrips = new ObservableCollection<LineTrip>(lineTrips);
        }
        private void rbStation_check(object sender, RoutedEventArgs e)
        {
            lvudateLine.Visibility = Visibility.Visible;
            btnAddLineStation.Visibility = Visibility.Visible;
            lvudateLineTrip.Visibility = Visibility.Hidden;
        }
        private void rbLineTrip_check(object sender, RoutedEventArgs e)
        {
            lvudateLine.Visibility = Visibility.Hidden;
            btnAddLineStation.Visibility = Visibility.Hidden;
            lvudateLineTrip.Visibility = Visibility.Visible;
        }

        public PO.Line SelectedLine
        {
            get { return (PO.Line)GetValue(SelectedLineProperty); }
            set { SetValue(SelectedLineProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedLineProperty =
            DependencyProperty.Register("SelectedLine", typeof(PO.Line), typeof(UpdateLineWindow), new PropertyMetadata(null));


        public ICommand MoveUpCommand { get; set; }
        public ICommand MoveDownCommand { get; set; }

        public ObservableCollection<LineTrip> LineTrips
        {
            get { return (ObservableCollection<LineTrip>)GetValue(LineTripsProperty); }
            set { SetValue(LineTripsProperty, value); }
        }
        //Using a DependencyProperty as the backing store for Buses.This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LineTripsProperty =
            DependencyProperty.Register("LineTrip", typeof(ObservableCollection<LineTrip>), typeof(UpdateLineWindow), new FrameworkPropertyMetadata(new ObservableCollection<LineTrip>()));



        private void Add_Line_Station_Button_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedLine == null) return;
            AddLineStation addLineStationWindow = new AddLineStation(SelectedLine);
            //addLineStationWindow.BusList = BusList;
            addLineStationWindow.Show();
        }
         private void Add_Line_Trip_Button_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedLine == null) return;
            AddLineTrip addLineTripWindow = new AddLineTrip(SelectedLine);
            addLineTripWindow.Show();
        }

      

        private void MoveUp(object dataContext)
        {

            //Update Data Source
            var line = LinePoToBoAdapter();
            var stationOfLine = (PO.StationOfLine)dataContext;

            var prevStation = line.StationsList.FirstOrDefault(s => s.LineStationIndex == stationOfLine.LineStationIndex - 1);
            var currStation = line.StationsList.FirstOrDefault(s => s.LineStationIndex == stationOfLine.LineStationIndex);
            var nextStation = line.StationsList.FirstOrDefault(s => s.LineStationIndex == stationOfLine.LineStationIndex + 1);

            var prevToPrev = line.StationsList.FirstOrDefault(s => s.LineStationIndex == stationOfLine.LineStationIndex - 2);
            double distanceToNextStation = 0.0;
            var timeToNext = default(TimeSpan);
            
            currStation.DistanceToNextStation = prevStation.DistanceToNextStation;
            currStation.TimeToNextStation = prevStation.TimeToNextStation;

            if (nextStation != null)
            {
                var d = bl.GetDistanceBetweenStations(prevStation.StationId, nextStation.StationId) ??
                      _rand.NextDouble() * 100;
                var t = bl.GetTimeBetweenStations(prevStation.StationId, nextStation.StationId) ??
                    TimeSpan.FromSeconds(_rand.Next(0, 3600));
                prevStation.DistanceToNextStation = d;
                prevStation.TimeToNextStation = t;

            }
            else
            {
                prevStation.DistanceToNextStation = 0.0;
                prevStation.TimeToNextStation = default(TimeSpan);
            }

            if (prevToPrev != null)
            {
                distanceToNextStation = bl.GetDistanceBetweenStations(currStation.StationId, prevToPrev.StationId) ?? _rand.NextDouble() * 100;
                timeToNext = bl.GetTimeBetweenStations(currStation.StationId, prevToPrev.StationId) ?? TimeSpan.FromSeconds(_rand.Next(0, 3600));
                prevToPrev.DistanceToNextStation = distanceToNextStation;
                prevToPrev.TimeToNextStation = timeToNext;
            }

            bl.MoveLineStationUp(line, currStation);

            

            //Update Ui
            SelectedLine.StationsList.RemoveAt(stationOfLine.LineStationIndex - 1);
            SelectedLine.StationsList[stationOfLine.LineStationIndex - 2].LineStationIndex = stationOfLine.LineStationIndex;

            stationOfLine.DistanceToNextStation = currStation.DistanceToNextStation;
            stationOfLine.TimeToNextStation = currStation.TimeToNextStation;

            SelectedLine.StationsList[stationOfLine.LineStationIndex - 2].DistanceToNextStation = prevStation.DistanceToNextStation;
            SelectedLine.StationsList[stationOfLine.LineStationIndex - 2].TimeToNextStation = prevStation.TimeToNextStation;

            var nextStationPO = SelectedLine.StationsList.FirstOrDefault(s => s.LineStationIndex == stationOfLine.LineStationIndex - 2);
            if (nextStationPO != null)
            {
                nextStationPO.DistanceToNextStation = prevToPrev.DistanceToNextStation;
                nextStationPO.TimeToNextStation = prevToPrev.TimeToNextStation;

            }
            SelectedLine.StationsList.Insert(stationOfLine.LineStationIndex - 2, stationOfLine);
            stationOfLine.LineStationIndex--;
        }

        private bool CanMoveUp(object dataContext)
        {

            var stationOfLine = (PO.StationOfLine)dataContext;
            return stationOfLine.LineStationIndex > 1;
        }

        private bool CanMoveDown(object dataContext)
        {
            var stationOfLine = (PO.StationOfLine)dataContext;
            return stationOfLine.LineStationIndex < SelectedLine.StationsList.Count();
        }

        private void MoveDown(object dataContext)
        {
            var stationOfLine = (PO.StationOfLine)dataContext;

            //Update Data Source
            var line = LinePoToBoAdapter();

            var currStation = line.StationsList.FirstOrDefault(s => s.LineStationIndex == stationOfLine.LineStationIndex);
            var nextStation = line.StationsList.FirstOrDefault(s => s.LineStationIndex == stationOfLine.LineStationIndex + 1);
            var prevStation = line.StationsList.FirstOrDefault(s => s.LineStationIndex == stationOfLine.LineStationIndex - 1);

            var distanceToNextStation = 0.0;
            var timeToNext = default(TimeSpan);
            var nextStationId = line.StationsList.FirstOrDefault(s => s.LineStationIndex == stationOfLine.LineStationIndex + 2)?.StationId;
            if (nextStationId.HasValue)
            {
                distanceToNextStation = bl.GetDistanceBetweenStations(currStation.StationId, nextStationId.Value) ??
                   _rand.NextDouble() * 100;
                timeToNext = bl.GetTimeBetweenStations(currStation.StationId, nextStationId.Value) ??
                   TimeSpan.FromSeconds(_rand.Next(0, 3600));

            }
            nextStation.DistanceToNextStation = currStation.DistanceToNextStation;
            nextStation.TimeToNextStation = currStation.TimeToNextStation;

            currStation.DistanceToNextStation = distanceToNextStation;
            currStation.TimeToNextStation = timeToNext;

            if (prevStation != null)
            {
                var d = bl.GetDistanceBetweenStations(prevStation.StationId, nextStation.StationId) ??
                      _rand.NextDouble() * 100;
                var t = bl.GetTimeBetweenStations(prevStation.StationId, nextStation.StationId) ??
                    TimeSpan.FromSeconds(_rand.Next(0, 3600));
                prevStation.DistanceToNextStation = d;
                prevStation.TimeToNextStation = t;

            }

            bl.MoveLineStationDown(line, currStation);

            //Update Ui
            stationOfLine.DistanceToNextStation = distanceToNextStation;
            stationOfLine.TimeToNextStation = timeToNext;
            SelectedLine.StationsList.RemoveAt(stationOfLine.LineStationIndex - 1);
            SelectedLine.StationsList[stationOfLine.LineStationIndex - 1].LineStationIndex = stationOfLine.LineStationIndex;
            SelectedLine.StationsList[stationOfLine.LineStationIndex - 1].DistanceToNextStation = nextStation.DistanceToNextStation;
            SelectedLine.StationsList[stationOfLine.LineStationIndex - 1].TimeToNextStation = nextStation.TimeToNextStation;

            var prevStationPO = SelectedLine.StationsList.FirstOrDefault(s => s.LineStationIndex == stationOfLine.LineStationIndex - 1);
            if (prevStationPO != null)
            {
                prevStationPO.DistanceToNextStation = prevStation.DistanceToNextStation;
                prevStationPO.TimeToNextStation = prevStation.TimeToNextStation;

            }
            SelectedLine.StationsList.Insert(stationOfLine.LineStationIndex, stationOfLine);
            stationOfLine.LineStationIndex++;
        }

        private BO.Line LinePoToBoAdapter()
        {
            var lineBO = (BO.Line)SelectedLine.CopyPropertiesToNew(typeof(BO.Line));
            lineBO.StationsList = SelectedLine.StationsList.Select(s => s.CopyPropertiesToNew(typeof(BO.StationOfLine))).Cast<BO.StationOfLine>().ToList();
            lineBO.LineTripList = SelectedLine.LineTripList.Select(lt => lt.CopyPropertiesToNew(typeof(BO.LineTrip))).Cast<BO.LineTrip>().ToList();
            return lineBO;
        }

        
    }
}
