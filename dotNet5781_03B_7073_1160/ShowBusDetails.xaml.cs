﻿using System;
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

namespace dotNet5781_03B_7073_1160
{
    /// <summary>
    /// Interaction logic for ShowBusDetails.xaml
    /// </summary>
    public partial class ShowBusDetails : Window
    {
        public Bus SelectedBus { get; set; }

        public ShowBusDetails()
        {
            InitializeComponent();
            DataContext = this;
        }


        private void Treat_Click(object sender, RoutedEventArgs e)
        {

            string msg = SelectedBus.Treat();
            MessageBox.Show(msg);
        }



        private void Fuel_Click(object sender, RoutedEventArgs e)
        {
            string msg = SelectedBus.Refuel();

            MessageBox.Show(msg);
        }
    }
}
