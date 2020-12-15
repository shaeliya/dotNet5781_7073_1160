using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static dotNet5781_03B_7073_1160.Utils;

namespace dotNet5781_03B_7073_1160
{
    /// <summary>
    /// Interaction logic for EditBus.xaml
    /// </summary>
    public partial class EditBus : Window, INotifyPropertyChanged
    {
        private string _failureReason;

        public event PropertyChangedEventHandler PropertyChanged;
        public Bus SelectedBus { get; set; }
        public ICommand TreatmentCommand { get; set; }
        public ICommand FuelCommand { get; set; }
        public string FailureReason
        {
            get { return _failureReason; }
            set
            {
                _failureReason = value;
                OnPropertyChanged(nameof(FailureReason));
            }
        }
        public EditBus()
        {
            InitializeComponent();
            FailureReason = string.Empty;
            TreatmentCommand = new RelayCommand(Treat, CanTreat);
            FuelCommand = new RelayCommand(Fuel, CanFuel);
            DataContext = this;
        }

        private bool CanTreat(object o)
        {
            return FailureReason != null && FailureReason.Equals("Treatment");
        }

        private void Treat(object o)
        {
            SelectedBus.Treat();
            FailureReason = string.Empty;

            MessageBox.Show("Treat successful");
        }


        private bool CanFuel(object o)
        {
            return FailureReason != null && FailureReason.Equals("Fuel");
        }

        private void Fuel(object o)
        {
            SelectedBus.Refuel();
            FailureReason = string.Empty;
            MessageBox.Show("Fuel successful");
        }


        private void tbKilometrage_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox text = sender as TextBox;
            if (text == null || e == null)
            {
                return;
            }
            if (e.Key == Key.Enter)
            {
                DriveBus();
            }

        }
        private void DriveBus()
        {
            double doubleValue;
            bool isDouble = double.TryParse(tbKilometrage.Text, out doubleValue);

            if (!isDouble)
            {
                MessageBox.Show("Kilometrage format is inccorect");
                return;
            }

            if (doubleValue >= 1200)
            {
                MessageBox.Show("Kilometrage must be below 1,200 Km");
                return;

            }

            string message = string.Empty;
            string reason = string.Empty;
            bool isSuccess = SelectedBus.CanTravel(doubleValue, out message, out reason);


            if (!isSuccess)
            {
                MessageBox.Show(message);
                FailureReason = reason;
            }
            else
            {
                isSuccess = SelectedBus.TravelKilometrage(doubleValue, out message);

                if (isSuccess)
                {
                    MessageBox.Show("You arrived to your destination!");
                    Close();

                }
                else
                {
                    MessageBox.Show(message);
                    FailureReason = reason;
                }
            }
        }
        private void Car_Treatment_Button_Click(object sender, RoutedEventArgs e)
        {
            SelectedBus.Treat();


        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }
        private static readonly Regex _regex = new Regex("[^0-9.-]+"); //regex that matches disallowed text
        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }
    }
}
