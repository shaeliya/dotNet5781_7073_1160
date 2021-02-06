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
    /// Interaction logic for AddLineTrip.xaml
    /// </summary>
    public partial class AddLineTrip : Window
    {
        IBL bl = new BLImp();

        public AddLineTrip(PO.Line line)
        {
            InitializeComponent();
            DataContext = this;
            this.line = line;
            LineTrips = new ObservableCollection<PO.LineTrip>(bl.GetAllLineTrips().Where(LineTripFilter).Select(s => s.CopyPropertiesToNew(typeof(PO.LineTrip))).Cast<PO.LineTrip>());

        }
        bool LineTripFilter(BO.LineTrip lineTrip)
        {
            return lineTrip.IsDeleted == false && line.LineTripList.Any(s => s.LineTripId == lineTrip.LineTripId) == false;
        }

        public ObservableCollection<PO.LineTrip> LineTrips
        {
            get { return (ObservableCollection<PO.LineTrip>)GetValue(LineTripsProperty); }
            set { SetValue(LineTripsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StationsList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LineTripsProperty =
            DependencyProperty.Register("LineTrips", typeof(ObservableCollection<PO.LineTrip>), typeof(AddLineTrip), new FrameworkPropertyMetadata(new ObservableCollection<PO.LineTrip>()));
        private readonly PO.Line line;

        public TimeSpan StartAt
        {
            get { return (TimeSpan)GetValue(StartAtProperty); }
            set { SetValue(StartAtProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TimeToNextStation.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StartAtProperty =
            DependencyProperty.Register("StartAt", typeof(TimeSpan), typeof(AddLineTrip ), new PropertyMetadata(default(TimeSpan)));
        public PO.LineTrip SelectedLineTrip
        {
            get { return (PO.LineTrip)GetValue(SelectedLineTripProperty); }
            set { SetValue(SelectedLineTripProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Station.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedLineTripProperty =
            DependencyProperty.Register("SelectedLineTrip", typeof(PO.LineTrip), typeof(AddLineTrip), new PropertyMetadata(default(PO.LineTrip)));

        private void Add_Line_Trip_Button_Click(object sender, RoutedEventArgs e)
        {

            var lineTrip = new PO.LineTrip()
            {
             
                StartAt = StartAt
            };
           
            var lineBO = (BO.Line)line.CopyPropertiesToNew(typeof(BO.Line));
            var lineTripBO = (BO.LineTrip)lineTrip.CopyPropertiesToNew(typeof(BO.LineTrip));
            lineBO.LineTripList = line.LineTripList.Select(l => l.CopyPropertiesToNew(typeof(BO.LineTrip))).Cast<BO.LineTrip>().ToList();
            bl.AddLineTripToLine(lineBO, lineTripBO);
            lineBO = bl.GetLineById(lineBO.LineId);
            line.LineTripList = new ObservableCollection<PO.LineTrip>(lineBO.LineTripList.Select(s => s.CopyPropertiesToNew(typeof(PO.LineTrip))).Cast<PO.LineTrip>());

            MessageBox.Show("Line trip Added Successfully!");
       

            Close();
        }
        private void keyCheck(object sender, KeyEventArgs e)
        {
            if (((int)e.Key < (int)Key.D0 || (int)e.Key > (int)Key.D9) && ((int)e.Key < (int)Key.NumPad0 || (int)e.Key > (int)Key.NumPad9) && e.Key != Key.OemPeriod && e.Key != Key.Escape && e.Key != Key.Back)
                e.Handled = true;
        }
    }
    
}
