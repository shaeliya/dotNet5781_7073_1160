﻿using BL;
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
    /// Interaction logic for AddLineStation.xaml
    /// </summary>
    public partial class AddLineStation : Window
    {
        public AddLineStation(PO.Line line)
        {
            InitializeComponent();
            DataContext = this;
            this.line = line;
            Stations = new ObservableCollection<PO.Station>(bl.GetAllStation().Where(StationsFilter).Select(s => s.CopyPropertiesToNew(typeof(PO.Station))).Cast<PO.Station>());
        }
        IBL bl = new BLImp();
        bool StationsFilter (BO.Station station)
        {
            return station.IsDeleted == false && line.StationsList.Any(s => s.LineStationId == station.StationId) == false;
        }
        public ObservableCollection<PO.Station> Stations
        {
            get { return (ObservableCollection<PO.Station>)GetValue(StationsProperty); }
            set { SetValue(StationsProperty, value); }
        }



        public double DistanceToNextStation
        {
            get { return (double)GetValue(DistanceToNextStationProperty); }
            set { SetValue(DistanceToNextStationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DistanceToNextStation.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DistanceToNextStationProperty =
            DependencyProperty.Register("DistanceToNextStation", typeof(double), typeof(AddLineStation), new PropertyMetadata(0.0));




        public TimeSpan TimeToNextStation
        {
            get { return (TimeSpan)GetValue(TimeToNextStationProperty); }
            set { SetValue(TimeToNextStationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TimeToNextStation.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TimeToNextStationProperty =
            DependencyProperty.Register("TimeToNextStation", typeof(TimeSpan), typeof(AddLineStation), new PropertyMetadata(default(TimeSpan)));


        public int StationIndex
        {
            get { return (int)GetValue(StationIndexProperty); }
            set { SetValue(StationIndexProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StationIndex.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StationIndexProperty =
            DependencyProperty.Register("StationIndex", typeof(int), typeof(AddLineStation), new PropertyMetadata(0));


        // Using a DependencyProperty as the backing store for StationsList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StationsProperty =
            DependencyProperty.Register("Stations", typeof(ObservableCollection<PO.Station>), typeof(AddLineStation), new FrameworkPropertyMetadata(new ObservableCollection<PO.Station>()));
        private readonly PO.Line line;


        public PO.Station SelectedStation
        {
            get { return (PO.Station)GetValue(SelectedStationProperty); }
            set { SetValue(SelectedStationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Station.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedStationProperty =
            DependencyProperty.Register("SelectedStation", typeof(PO.Station), typeof(AddLineStation), new PropertyMetadata(default(PO.Station)));



        private void AddStationToLineClicked(object sender, RoutedEventArgs e)
        {
            var item = new PO.StationOfLine()
            {
                Name = SelectedStation.Name,
                StationId = SelectedStation.StationId,
                DistanceToNextStation = DistanceToNextStation,
                TimeToNextStation = TimeToNextStation,
                LineStationIndex = StationIndex
            };
            foreach (var stationOfLine in line.StationsList.Skip(StationIndex - 1))
            {
                stationOfLine.LineStationIndex++;
            }
            line.StationsList.Insert(StationIndex - 1, item);

            var newL = (BO.Line)line.CopyPropertiesToNew(typeof(BO.Line));
            newL.StationsList = line.StationsList.Select(s => s.CopyPropertiesToNew(typeof(BO.StationOfLine))).Cast<BO.StationOfLine>().ToList();
            bl.UpdateLineStations(newL);
            //bl.up
            Close();
        }
    }
}
