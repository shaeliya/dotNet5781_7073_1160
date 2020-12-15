using System.Linq;
using System.Windows;
using System.Windows.Controls;
using dotNet5781_02_7073_1160;
//----------------------------
//----------------------------
//Shalhevet Eliyahu 211661160
//Orit Stavsky 212507073
//----------------------------
//----------------------------
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
            busLines = Program.InitializeBusCollection();
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
       

    }

}

