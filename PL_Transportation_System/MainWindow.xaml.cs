using BLAPI;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace PL_Transportation_System
{
   
    public partial class MainWindow : Window
    {

        IBL bl = BLFactory.GetBL("1");

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Line_Button_Click(object sender, RoutedEventArgs e)
        {
            ActionsOnLineWindow actionsOnLineWindow = new ActionsOnLineWindow( );

            actionsOnLineWindow.Show();
        }

        private void Bus_Button_Click(object sender, RoutedEventArgs e)
        {
            ActionsOnBusWindow actionsOnBusWindow = new ActionsOnBusWindow();

            actionsOnBusWindow.Show();
        }

        private void Station_Button_Click(object sender, RoutedEventArgs e)
        {
            ActionsOnStationWindow actionsOnStationWindow = new ActionsOnStationWindow();

            actionsOnStationWindow.Show();
        }

        private void LineTrip_Button_Click(object sender, RoutedEventArgs e)
        {
            ActionsOnLineTrip actionsOnLineTrip = new ActionsOnLineTrip();

            actionsOnLineTrip.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ShowLineTimingWindow showLineTimingWindow = new ShowLineTimingWindow();
            showLineTimingWindow.Show();
                
        }
    }
}
