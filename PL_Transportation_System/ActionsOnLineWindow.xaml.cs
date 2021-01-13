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
    /// Interaction logic for ActionsOnLineWindow.xaml
    /// </summary>
    public partial class ActionsOnLineWindow : Window
    {
        IBL bl = new BLImp();
        public ActionsOnLineWindow()
        {

            InitializeComponent();

            lvLine.ItemsSource = new ObservableCollection<BO.Line>(bl.GetAllLine());
            lvLine.DisplayMemberPath = " LineNumber ".ToString();

        }
        void RefreshLinesListView()
        {
            lvLine.DataContext =new ObservableCollection<BO.Line>( bl.GetAllLine());
        }

        private void Open_Update_Window_Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            if (btn.DataContext is BO.Line)
            {
                BO.Line line = (BO.Line)btn.DataContext;
                UpdateLineWindow updateLineWindow = new UpdateLineWindow(line);
                //updateLineWindow.SelectedLine = line;
                updateLineWindow.Show();

            }

            //UpdateLineWindow updateLineWindow = new UpdateLineWindow();

            //updateLineWindow.Show();
        }
        private void Open_Delete_Window_Button_Click(object sender, RoutedEventArgs e)
        {
            {
                
                MessageBoxResult res = MessageBox.Show("Are you sure deleting selected line?", "Verification", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (res == MessageBoxResult.No)
                    return;
                try
                {
                    bl.DeleteLine(((BO.Line)((Button)sender).DataContext).LineId);
                    RefreshLinesListView();
                }
                catch (BO.Exceptions.LineNotFoundException ex)
                {
                    MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                }           
                Close();
                
            }

        }
    }
}

