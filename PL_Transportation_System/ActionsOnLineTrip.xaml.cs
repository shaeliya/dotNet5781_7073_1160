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

//namespace PL_Transportation_System
////{
////    /// <summary>
////    /// Interaction logic for ActionsOnLineTrip.xaml
////    /// </summary>
////    public partial class ActionsOnLineTrip : Window
////    {
//////        public ActionsOnLineTrip()
//////        {
//////            InitializeComponent();
//////            DataContext = this;
//////            Lines = new ObservableCollection<PO.Line>(bl.GetAllLine().Select(l =>
////            {
////                var newL = (PO.Line)l.CopyPropertiesToNew(typeof(PO.Line));
////                newL.StationsList = new ObservableCollection<PO.StationOfLine>(l.StationsList.Select(s => s.CopyPropertiesToNew(typeof(PO.StationOfLine))).Cast<PO.StationOfLine>());
////                newL.IsUpdated = false;
////                return newL;
////            }).Cast<PO.Line>());

////        }
////        private void Open_Update_Window_Button_Click(object sender, RoutedEventArgs e)
////        {
////            Button btn = (Button)sender;
////            if (btn.DataContext is PO.Line)
////            {
////                var line = (PO.Line)btn.DataContext;
////                if (!line.IsDeleted)
////                {
////                    UpdateLineWindow updateLineWindow = new UpdateLineWindow(line);
////                    //updateLineWindow.SelectedLine = line;
////                    updateLineWindow.Show();
////                }
////                else
////                {
////                    MessageBox.Show("Cannot update deleted line");
////                }
////            }

////            //UpdateLineWindow updateLineWindow = new UpdateLineWindow();

////            //updateLineWindow.Show();
////        }

////    }
////}
