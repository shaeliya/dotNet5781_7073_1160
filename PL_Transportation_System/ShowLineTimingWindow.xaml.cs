using BL;
using BLAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for ShowLineTimingWindow.xaml
    /// </summary>
    public partial class ShowLineTimingWindow : Window
    {
        IBL bl = new BLImp();
        Stopwatch stopwatch;
        BackgroundWorker timeWorker;
        TimeSpan currentTime;


        public ObservableCollection<PO.LineTiming> LineTimings
        {
            get { return (ObservableCollection<PO.LineTiming>)GetValue(LineTimingsProperty); }
            set { SetValue(LineTimingsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for lines.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LineTimingsProperty =
            DependencyProperty.Register("LineTimings", typeof(ObservableCollection<PO.LineTiming>), typeof(ShowLineTimingWindow), new FrameworkPropertyMetadata(new ObservableCollection<PO.LineTiming>()));

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

        public PO.Station SelectedStation
        {
            get { return (PO.Station)GetValue(SelectedStationProperty); }
            set { SetValue(SelectedStationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Station.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedStationProperty =
            DependencyProperty.Register("SelectedStation", typeof(PO.Station), typeof(ShowLineTimingWindow), new PropertyMetadata(default(PO.Station), new PropertyChangedCallback(OnPropChanged)));


        public ShowLineTimingWindow()
        {

            InitializeComponent();
            DataContext = this;
            stopwatch = new Stopwatch();
            timeWorker = new BackgroundWorker();
            timeWorker.WorkerReportsProgress = true;
            timeWorker.WorkerSupportsCancellation = true;
            timeWorker.DoWork += DoWork;
            timeWorker.ProgressChanged += ProgressChanged;
            SetCurrentTime();
            //Update_lblCurrentTime();

            Stations = new ObservableCollection<PO.Station>(bl.GetAllStation().Select(s => s.CopyPropertiesToNew(typeof(PO.Station))).Cast<PO.Station>());

        }

        private void Update_lblCurrentTime()
        {
            lblCurrentTime.Content = currentTime.ToString("hh':'mm':'ss");
        }

        private void ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            currentTime = currentTime.Add(new TimeSpan(0, 10, 0));

            Update_lblCurrentTime();
            var stationBO = (BO.Station)SelectedStation.CopyPropertiesToNew(typeof(BO.Station));
            stationBO.LinesList = SelectedStation.LinesList.Select(l => l.CopyPropertiesToNew(typeof(BO.LineOfStation))).Cast<BO.LineOfStation>().ToList();
            var allCurrentLinesForStation = bl.GetAllCurrentLinesForStation(stationBO, currentTime);

            var allCurrentLinesForStationPO = allCurrentLinesForStation.Select(lt => (PO.LineTiming)lt.CopyPropertiesToNew(typeof(PO.LineTiming))).Cast<PO.LineTiming>();


            LineTimings = new ObservableCollection<PO.LineTiming>(allCurrentLinesForStationPO);



        }

        private void DoWork(object sender, DoWorkEventArgs e)
        {
            while (!timeWorker.CancellationPending)
            {
                timeWorker.ReportProgress(0);
                Thread.Sleep(3000);
            }
        }


        private void cbLineStations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SelectedStation == null)
            {
                return;
            }

            if (!timeWorker.IsBusy)
            {
                timeWorker.RunWorkerAsync();
            }

            if (stopwatch.IsRunning)
            {
                stopwatch.Restart();
            }
            else
            {
                stopwatch.Start();
            }
            SetCurrentTime();

        }

        private void SetCurrentTime()
        {
            //currentTime = DateTime.Now.TimeOfDay;
            currentTime = new TimeSpan(6,0,0);
        }

        private void StopSimulation_Clicked(object sender, RoutedEventArgs e)
        {
            timeWorker.CancelAsync();
        }
    }
}

