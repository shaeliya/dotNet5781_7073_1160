using BLAPI;
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

namespace PL_Transportation_System
{
    /// <summary>
    /// Interaction logic for ActionsOnLineWindow.xaml
    /// </summary>
    public partial class ActionsOnLineWindow : Window
    {
        IBL bl;
        //BO.Line line;

        public ActionsOnLineWindow(IBL _bl)
        {
            InitializeComponent();
            bl = _bl;
            lvLine.ItemsSource = bl.GetAllLine().ToList();
            //lvLine.DisplayMemberPath = "LineNumber";
           // lvLine.DisplayMemberPath = line.ToString();


        }
    }
}
