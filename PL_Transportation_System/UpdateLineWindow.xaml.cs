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
    /// Interaction logic for UpdateLineWindow.xaml
    /// </summary>
    public partial class UpdateLineWindow : Window
    {
        IBL bl = new BLImp();
        public PO.Line SelectedLine = new PO.Line();

        public ObservableCollection<PO.StationOfLine> StationsList
        {
            get { return (ObservableCollection<PO.StationOfLine>)GetValue(StationsOfLine); }
            set { SetValue(StationsOfLine, value); }
        }

        // Using a DependencyProperty as the backing store for StationOfLine.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StationsOfLine =
            DependencyProperty.Register("StationsList", typeof(ObservableCollection<PO.StationOfLine>), typeof(ActionsOnLineWindow), new FrameworkPropertyMetadata(new ObservableCollection<PO.StationOfLine>()));

        public UpdateLineWindow(PO.Line selectedLine)
        {
            InitializeComponent();
            SelectedLine = selectedLine;
            StationsList = new ObservableCollection<PO.StationOfLine>(SelectedLine.StationsList);
            DataContext = StationsList;

        }

        private void Add_Line_Station_Button_Click(object sender, RoutedEventArgs e)
        {
            DataContext= new ObservableCollection<PO.StationOfLine>(SelectedLine.StationsList);
            AddLineStation addLineStationWindow = new AddLineStation();
            //addLineStationWindow.BusList = BusList;
            addLineStationWindow.Show();
        }

        private void MoveUp_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            if (btn.DataContext is PO.StationOfLine)
            {
                var stationOfLine = (PO.StationOfLine)btn.DataContext;
                var line = LinePoToBoAdapter();
                var stationOfLineBO = (BO.StationOfLine)stationOfLine.CopyPropertiesToNew(typeof(BO.StationOfLine));
                bl.MoveLineStationUp(line, stationOfLineBO);
            }
        }


        private void MoveDown_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            if (btn.DataContext is PO.StationOfLine)
            {
                var stationOfLine = (PO.StationOfLine)btn.DataContext;
                var line = LinePoToBoAdapter();
                var stationOfLineBO = (BO.StationOfLine)stationOfLine.CopyPropertiesToNew(typeof(BO.StationOfLine));
                bl.MoveLineStationDown(line, stationOfLineBO);
            }
        }

        private BO.Line LinePoToBoAdapter()
        {
            var lineBO = (BO.Line)SelectedLine.CopyPropertiesToNew(typeof(BO.Line));
            lineBO.StationsList = SelectedLine.StationsList.Select(s => s.CopyPropertiesToNew(typeof(BO.StationOfLine))).Cast<BO.StationOfLine>().ToList();
            return lineBO;
        }
    }
}
