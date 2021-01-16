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
        public UpdateLineWindow(PO.Line selectedLine)
        {
            InitializeComponent();
            SelectedLine = selectedLine;
            DataContext = new ObservableCollection<PO.StationOfLine>(SelectedLine.StationsList);

        }

        private void Add_Line_Station_Button_Click(object sender, RoutedEventArgs e)
        {
            DataContext= new ObservableCollection<PO.StationOfLine>(SelectedLine.StationsList);
            AddLineStation addLineStationWindow = new AddLineStation();
            //addLineStationWindow.BusList = BusList;
            addLineStationWindow.Show();
        }
    }
}
