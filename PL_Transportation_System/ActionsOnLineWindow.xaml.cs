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
            DataContext = this;
            Lines = new ObservableCollection<PO.Line>(bl.GetAllLine().Select(l =>
            {
                var newL = (PO.Line)l.CopyPropertiesToNew(typeof(PO.Line));
                newL.StationsList = l.StationsList.Select(s => s.CopyPropertiesToNew(typeof(PO.StationOfLine))).Cast<PO.StationOfLine>().ToList();
                newL.IsUpdated = false;
                return newL;
            }).Cast<PO.Line>().OrderBy(l => l.LineNumber).ToList());

            lvLine.DisplayMemberPath = " LineNumber ".ToString();

        }

        public ObservableCollection<PO.Line> Lines
        {
            get { return (ObservableCollection<PO.Line>)GetValue(LinesProperty); }
            set { SetValue(LinesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StationsList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LinesProperty =
            DependencyProperty.Register("Lines", typeof(ObservableCollection<PO.Line>), typeof(ActionsOnLineWindow), new FrameworkPropertyMetadata(new ObservableCollection<PO.Line>()));


        void RefreshLinesListView()
        {
            lvLine.DataContext = new ObservableCollection<BO.Line>(bl.GetAllLine());
        }

        private void Open_Update_Window_Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            if (btn.DataContext is PO.Line)
            {
                var line = (PO.Line)btn.DataContext;
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

        private void UpdateAllClicked(object sender, RoutedEventArgs e)
        {

            var linesToUpdate = Lines.Where(l => l.IsUpdated).Select(l => {
                var newL = (BO.Line)l.CopyPropertiesToNew(typeof(BO.Line));
                newL.StationsList = l.StationsList.Select(s => s.CopyPropertiesToNew(typeof(BO.StationOfLine))).Cast<BO.StationOfLine>().ToList();
                return newL;
            });
            foreach (var line in linesToUpdate)
            {
                bl.UpdateLine(line);
            }
        }
    }
}

