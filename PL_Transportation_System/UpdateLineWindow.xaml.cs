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

        public PO.Line SelectedLine 
        {
            get { return (PO.Line)GetValue(SelectedLineProperty); }
            set { SetValue(SelectedLineProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedLineProperty =
            DependencyProperty.Register("SelectedLine", typeof(PO.Line), typeof(UpdateLineWindow), new PropertyMetadata(null));


        public UpdateLineWindow(PO.Line selectedLine)
        {
            InitializeComponent();
            SelectedLine = selectedLine;
            DataContext = this;

        }
     
        private void Add_Line_Station_Button_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedLine == null) return;
            AddLineStation addLineStationWindow = new AddLineStation(SelectedLine);
            //addLineStationWindow.BusList = BusList;
            addLineStationWindow.Show();
        }

        private void MoveUp_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            if (btn.DataContext is PO.StationOfLine)
            {
                var stationOfLine = (PO.StationOfLine)btn.DataContext;

                //Update Data Source
                var line = LinePoToBoAdapter();
                var stationOfLineBO = (BO.StationOfLine)stationOfLine.CopyPropertiesToNew(typeof(BO.StationOfLine));
                bl.MoveLineStationUp(line, stationOfLineBO);

                //Update Ui
                SelectedLine.StationsList.RemoveAt(stationOfLine.LineStationIndex - 1);
                SelectedLine.StationsList[stationOfLine.LineStationIndex - 2].LineStationIndex = stationOfLine.LineStationIndex;
                SelectedLine.StationsList.Insert(stationOfLine.LineStationIndex - 2, stationOfLine);
                stationOfLine.LineStationIndex--;


            }
        }


        private void MoveDown_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            if (btn.DataContext is PO.StationOfLine)
            {
                var stationOfLine = (PO.StationOfLine)btn.DataContext;

                //Update Data Source
                var line = LinePoToBoAdapter();
                var stationOfLineBO = (BO.StationOfLine)stationOfLine.CopyPropertiesToNew(typeof(BO.StationOfLine));
                bl.MoveLineStationDown(line, stationOfLineBO);

                //Update Ui
                SelectedLine.StationsList.RemoveAt(stationOfLine.LineStationIndex - 1);
                SelectedLine.StationsList[stationOfLine.LineStationIndex - 1].LineStationIndex = stationOfLine.LineStationIndex;
                SelectedLine.StationsList.Insert(stationOfLine.LineStationIndex, stationOfLine);
                stationOfLine.LineStationIndex++;
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
