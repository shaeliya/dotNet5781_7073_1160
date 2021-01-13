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
        public BO.Line SelectedLine = new BO.Line();
        public UpdateLineWindow(BO.Line selectedLine)
        {
            InitializeComponent();
            SelectedLine = selectedLine;
            lvudateLine.ItemsSource = new ObservableCollection<BO.StationOfLine>(SelectedLine.StationsList);


        }
    }
}
