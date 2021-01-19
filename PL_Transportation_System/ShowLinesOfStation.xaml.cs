using BL;
using BLAPI;
using PL_Transportation_System.PO;
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
    /// Interaction logic for ShowLinesOfStation.xaml
    /// </summary>
    public partial class ShowLinesOfStation : Window

    {
        IBL bl = new BLImp();
        public List<LineOfStation> LineOfStationList { get; set; }
   
        public ShowLinesOfStation(Station selectedStation)
        {
            InitializeComponent();
            LineOfStationList = selectedStation.LinesList.ToList();
            DataContext = this;
            //lvBus.DisplayMemberPath = " LicenseNumber ".ToString();
        }

    }

}
