using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BL;
using BLAPI;
using BO;

namespace PL_Transportation_System
{
    /// <summary>
    /// Interaction logic for AddLine.xaml
    /// </summary>
    public partial class AddLine : Window
    {
        IBL bl = new BLImp();
        public AddLine()
        {
            InitializeComponent();
               
                DataContext = this;                       
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bool isValid = LineValidityCheck();
            if (isValid)
            {
                try
                {
                    BO.Line line = new BO.Line();                  

                    line.LineNumber = Convert.ToInt32(tbLineNumber.Text);
                    Areas area;

                    bool isAreas = Enum.TryParse(cbArea.Text, out area);
                    if (isAreas)
                    {
                        line.Area = area;
                    }
                   
                    bl.AddLine(line);
                    MessageBox.Show("Line Added Successfully!");
                    Close();
                }
                catch (BO.Exceptions.LineAlreadyExistsException)
                {
                    MessageBox.Show("Line Already Exists!");
                }
            }
        }
        private bool LineValidityCheck()
        {
            if (string.IsNullOrWhiteSpace(tbLineNumber.Text))
            {
                MessageBox.Show("Must Enter Line Number");
                return false;
            }
            bool isDouble = false;
            double doubleValue;

            isDouble = double.TryParse(tbLineNumber.Text, out doubleValue);

            if (!isDouble)
            {
                MessageBox.Show("Line Number is inccorect format");
                return false;
            }
            if (string.IsNullOrWhiteSpace(cbArea.Text))
            {
                MessageBox.Show("Must Enter Area");
                return false;
            }
                                
            return true;
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
