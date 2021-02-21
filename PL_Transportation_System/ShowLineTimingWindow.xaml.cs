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
        Stopwatch stopwatch; // שעון
        BackgroundWorker timeWorker; 
        TimeSpan currentTime; // זמן נוכחי


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
            DependencyProperty.Register("Stations", typeof(ObservableCollection<PO.Station>), typeof(ShowLineTimingWindow), new FrameworkPropertyMetadata(new ObservableCollection<PO.Station>()));
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
            timeWorker.WorkerSupportsCancellation = true; // לאפשר עצירה שעון
            timeWorker.DoWork += DoWork;
            timeWorker.ProgressChanged += ProgressChanged;
            StartWorkerAndStopwatch();
            Stations = new ObservableCollection<PO.Station>(bl.GetAllStation().Select(s => s.CopyPropertiesToNew(typeof(PO.Station))).Cast<PO.Station>());

        }

        private void Update_lblCurrentTime()
        {
            lblCurrentTime.Content = currentTime.ToString("hh':'mm':'ss");
        }

        /// <summary>
        /// ProgressChanged הפונקציה שנרקאת כשנזרק אירוע
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // מוסיפה 10 דקות לזמן הנוכחי
            currentTime = currentTime.Add(new TimeSpan(0, 10, 0));

            // מעדכנת את השעון במסך עצמו
            Update_lblCurrentTime();

            // עדכון הזמן חייב להעשות בכל מקרה. אבל עדכון תחנות נבצע רק אם נבחרה תחנה
            if (SelectedStation != null)
            {
                // כדי שאוכל להפעיל עליה את הפונקציה BO לתחנה PO ממירה את התחנה
                var stationBO = (BO.Station)SelectedStation.CopyPropertiesToNew(typeof(BO.Station));
                stationBO.LinesList = SelectedStation.LinesList.Select(l => l.CopyPropertiesToNew(typeof(BO.LineOfStation))).Cast<BO.LineOfStation>().ToList();
                // שמביאה את כל התחנות לחצי השעה הקרובה - השאילתה שכתבנו BL-קריאה לפונקציה ב
                var allCurrentLinesForStation = bl.GetAllCurrentLinesForStation(stationBO, currentTime);

                // כדי שנוכל להציג אותם בחלון PO לזמני הקו BO ממירה את זמני הקו
                var allCurrentLinesForStationPO = allCurrentLinesForStation.Select(lt => (PO.LineTiming)lt.CopyPropertiesToNew(typeof(PO.LineTiming))).Cast<PO.LineTiming>();

                // מציגה את רשימת זמני הקוים
                LineTimings = new ObservableCollection<PO.LineTiming>(allCurrentLinesForStationPO);
            }

        }

        /// <summary>
        /// מבצע Worker-הפונקציה שה
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DoWork(object sender, DoWorkEventArgs e)
        {
            // כל עוד לא ביטלנו - עצרנו
            while (!timeWorker.CancellationPending)
            {
                // ProgressChanged מרים (מפעיל) אירוע של 
                timeWorker.ReportProgress(0);
                // ישן שנייה
                Thread.Sleep(1000);
            }
        }

        /// <summary>
        /// ComboBox-הפונקציה שנקראת בשינוי של בחירה ב
        /// </summary>.
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbLineStations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SelectedStation == null)
            {
                return;
            }

            StartWorkerAndStopwatch();

        }

        /// <summary>
        /// :במקרים הבאים Stopwatch-ואת ה Worker-נתחיל את ה
        /// 1. Ctor-בתחילת החלון ב 
        /// 2. RestartSimulation אם לוחצים על הכפתור 
        /// 3. אם משנים את הבחירה במסך והסימולציה כבר עצרה
        /// </summary>
        private void StartWorkerAndStopwatch()
        {
            if (!timeWorker.IsBusy)
            {
                // timeWorker-מפעיל את ה
                timeWorker.RunWorkerAsync();
            }

            if (!stopwatch.IsRunning)
            {
                // מפעילה את השעון וקובעת את הזמן
                stopwatch.Start();
                SetCurrentTime();
            }
        }

        private void SetCurrentTime()
        {
            currentTime = DateTime.Now.TimeOfDay;
            //currentTime = new TimeSpan(7,0,0);
        }

        private void StopSimulation_Clicked(object sender, RoutedEventArgs e)
        {
            timeWorker.CancelAsync();
        }

        private void RestartSimulation_Clicked(object sender, RoutedEventArgs e)
        {
            StartWorkerAndStopwatch();
        }
    }
}

