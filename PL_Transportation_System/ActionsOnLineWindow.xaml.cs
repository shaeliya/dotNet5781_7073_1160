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
        BO.Line ln;
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

        public class line : DependencyObject
        {
            public int lineNumber
            {
                get { return (int)GetValue(lineNumberProperty); }
                set { SetValue(lineNumberProperty, value); }
            }

            // Using a DependencyProperty as the backing store for lineNumber.  This enables animation, styling, binding, etc...
            public static readonly DependencyProperty lineNumberProperty =
                DependencyProperty.Register("lineNumber", typeof(int), typeof(Line), new PropertyMetadata(0));


            public Enum Area
            {
                get { return (Enum)GetValue(AreaProperty); }
                set { SetValue(AreaProperty, value); }
            }

            // Using a DependencyProperty as the backing store for Area.  This enables animation, styling, binding, etc...
            public static readonly DependencyProperty AreaProperty =
                DependencyProperty.Register("Area", typeof(Enum), typeof(line), new PropertyMetadata(0));


        }


        private void Open_Update_Window_Button_Click(object sender, RoutedEventArgs e)
        {
            UpdateLineWindow updateLineWindow = new UpdateLineWindow();

            updateLineWindow.Show();
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

