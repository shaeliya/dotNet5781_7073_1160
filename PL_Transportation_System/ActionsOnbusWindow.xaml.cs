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
    /// Interaction logic for ActionsOnbusWindow.xaml
    /// </summary>
    public partial class ActionsOnBusWindow : Window

    {
        IBL bl = new BLImp();
        public ActionsOnBusWindow()
        {
            InitializeComponent();
            DataContext = this;
            var buses = bl.GetAllBusses().Select(b => (Bus)b.CopyPropertiesToNew(typeof(Bus)));
            Buses = new ObservableCollection<Bus>(buses);           
            //lvBus.DisplayMemberPath = " LicenseNumber ".ToString();
        }
      
        public ObservableCollection<Bus> Buses
        {
            get { return (ObservableCollection<Bus>)GetValue(BusesProperty); }
            set { SetValue(BusesProperty, value); }
        }
        //Using a DependencyProperty as the backing store for Buses.This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BusesProperty =
            DependencyProperty.Register("Buses", typeof(ObservableCollection<Bus>), typeof(ActionsOnBusWindow), new FrameworkPropertyMetadata(new ObservableCollection<Bus>()));



        private void UpdateAllClicked(object sender, RoutedEventArgs e)
        {

            var BusesToUpdate = Buses.Where(b => b.IsUpdated).Select(b =>
            {
                var newB = (BO.Bus)b.CopyPropertiesToNew(typeof(BO.Bus));
                
                return newB;

            });
                foreach (var bus in BusesToUpdate)
                {
                    bl.UpdateBus(bus);
                }
          
           }

        private void AddBus(object sender, RoutedEventArgs e)
        {
            AddBus addBusWindow = new AddBus();
            addBusWindow.Show();
        }
    }

}
