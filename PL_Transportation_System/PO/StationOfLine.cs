using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PL_Transportation_System.PO
{
    public class StationOfLine: DependencyObject
    {

              
        //public TimeSpan TimeToNextStation


        public string Name
        {
            get { return (string)GetValue(NameProperty); }
            set { SetValue(NameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Name.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NameProperty =
            DependencyProperty.Register("Name", typeof(string), typeof(StationOfLine), new PropertyMetadata(""));



        public int LineStationIndex
        {
            get { return (int)GetValue(LineStationIndexProperty); }
            set { SetValue(LineStationIndexProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LineStationIndex.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LineStationIndexProperty =
            DependencyProperty.Register("LineStationIndex", typeof(int), typeof(StationOfLine), new PropertyMetadata(0));



        public double DistanceToNextStation
        {
            get { return (double)GetValue(DistanceToNextStationProperty); }
            set { SetValue(DistanceToNextStationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DistanceToNextStation.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DistanceToNextStationProperty =
            DependencyProperty.Register("DistanceToNextStation", typeof(double), typeof(StationOfLine), new PropertyMetadata(0.0));



        public TimeSpan TimeToNextStation
        {
            get { return (TimeSpan)GetValue(TimeToNextStationProperty); }
            set { SetValue(TimeToNextStationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TimeToNextStation.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TimeToNextStationProperty =
            DependencyProperty.Register("TimeToNextStation", typeof(TimeSpan), typeof(StationOfLine), new PropertyMetadata(default(TimeSpan)));


        public bool IsDeleted
        {
            get { return (bool)GetValue(IsDeletedProperty); }
            set { SetValue(IsDeletedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsDeleted.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsDeletedProperty =
            DependencyProperty.Register("IsDeleted", typeof(bool), typeof(StationOfLine), new PropertyMetadata(false));



    }
}
