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
    /// Interaction logic for ActionsOnStationWindow.xaml
    /// </summary>
    public partial class ActionsOnStationWindow : Window
    {
        IBL bl = new BLImp();
        public ActionsOnStationWindow()
        {
            InitializeComponent();
            lvStation.ItemsSource = new ObservableCollection<BO.Station>(bl.GetAllStation());
        }

        void RefreshLinesListView()
        {
            lvStation.ItemsSource = new ObservableCollection<BO.Station>(bl.GetAllStation());
        }

        private void Open_Update_Window_Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            if (btn.DataContext is BO.Station)
            {
                BO.Station station = (BO.Station)btn.DataContext;
                UpdateStationWindow updateStationWindow = new UpdateStationWindow(station);
                //updateLineWindow.SelectedLine = line;
                updateStationWindow.Show();
            }

            //UpdateLineWindow updateLineWindow = new UpdateLineWindow();

            //updateLineWindow.Show();
        }
        private void Open_Delete_Window_Button_Click(object sender, RoutedEventArgs e)
        {
            {
                MessageBoxResult res = MessageBox.Show("Are you sure deleting selected station?", "Verification", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (res == MessageBoxResult.No)
                    return;
                try
                {
                    bl.DeleteStation(((BO.Station)((Button)sender).DataContext).StationId);
                    RefreshLinesListView();
                }
                catch (BO.Exceptions.StationNotFoundException ex)
                {
                    MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                Close();

            }

        }
    }
}
