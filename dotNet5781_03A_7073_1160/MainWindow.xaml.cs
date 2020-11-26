using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using dotNet5781_02_7073_1160;

namespace dotNet5781_03A_7073_1160
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public BusCollection busLines;
        public BusLine currentDisplayBusLine;
        public MainWindow()
        {
            InitializeComponent();           
            busLines = InitializeBusCollection();
            cbBusLines.ItemsSource = busLines;
            cbBusLines.DisplayMemberPath = " BusLineNumber ";
            cbBusLines.SelectedIndex = 0;


        }
        private void cbBusLines_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {        
            ShowBusLine((cbBusLines.SelectedValue as BusLine).BusLineNumber);
            tbArea.Text = (cbBusLines.SelectedValue as BusLine).Area.ToString();
        }
        private void ShowBusLine(string index)
        {
            currentDisplayBusLine = busLines[index].First();
            UpGrid.DataContext = currentDisplayBusLine;
            lbBusLineStations.DataContext = currentDisplayBusLine.Stations;
        }
        public static BusCollection InitializeBusCollection()
        {
            Random RandomArea = new Random(DateTime.Now.Millisecond);
            Random RandomBusNumber = new Random();
            Random RandomBusStop = new Random();

            BusCollection busCollection = new BusCollection();
            dotNet5781_02_7073_1160.Program.InitializeBusStopsList(busCollection);

            //קווים
            for (int i = 0; i < 10; i++)
            {
                int area = RandomArea.Next(0, 8);
                int busNumber = RandomBusNumber.Next(1, 999);
                BusLine busLine = new BusLine(busNumber.ToString(), (dotNet5781_02_7073_1160.Enum.Area)area);

                for (int j = 0; j <= 12; j++)
                {
                    TimeSpan travelTimeFromPrevioussBusStop = new TimeSpan(0, i, i * 2);
                    BusStop busStop = new BusStop();
                    
                    int BusStationKey = RandomBusStop.Next(1, 1000000);
                    busStop = busCollection.BusStopsList[j];
                    
                    BusLineStation busLineStation = new BusLineStation(travelTimeFromPrevioussBusStop, i * 1.1, busStop);
                    busLine.AddStation(j + 1, busLineStation);
                }
                busCollection.BusLinesList.Add(busLine);
            }

            return busCollection;
        }

    }

}

