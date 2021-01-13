using BL;
using BLAPI;
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
using System.Windows.Shapes;

namespace PL_Transportation_System
{
    /// <summary>
    /// Interaction logic for ChangeStationDetails.xaml
    /// </summary>
    public partial class ChangeStationDetails : Window
    {
        IBL bl = new BLImp();
        public ChangeStationDetails(int stationId)
        {
            InitializeComponent();
        }
        private void Change_Button_Click(object sender, RoutedEventArgs e)
        {
           
        }
        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
