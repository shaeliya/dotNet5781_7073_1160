﻿using DO;
using DS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CreateXmlFilesFromDataSource
{
    public class MaxCounters
    {

        public int MaxAdjacentStationsId { get; set; }
        public int MaxLineId { get; set; }
        public int MaxLineStationId { get; set; }
        public int MaxLineTripId { get; set; }
        public int MaxStationId { get; set; }

    }
    class Program
    {
        static void Main(string[] args)
        {

            SerializeAdjacentStations();
            SerializeBus();
            SerializeLine();
            SerializeLineStation();
            SerializeLineTrip();
            SerializeStation();
            SerializeConfig();
        }


        private static void SerializeBus()
        {
            XmlSerializer x = new XmlSerializer(typeof(List<Bus>));
            TextWriter writer = new StreamWriter("C:\\Temp\\BusXml.xml");
            x.Serialize(writer, DataSource.busesList);
        }
        private static void SerializeAdjacentStations()
        {
            var lst = DataSource.adjacentStationsList.ToList();
            XmlSerializer x = new XmlSerializer(typeof(List<AdjacentStations>));
            TextWriter writer = new StreamWriter("C:\\Temp\\AdjacentStationsXml.xml");
            x.Serialize(writer, lst);
        }
        private static void SerializeLine()
        {
            XmlSerializer x = new XmlSerializer(typeof(List<Line>));
            TextWriter writer = new StreamWriter("C:\\Temp\\LineXml.xml");
            x.Serialize(writer, DataSource.linesList);
        }
        private static void SerializeLineStation()
        {
            XmlSerializer x = new XmlSerializer(typeof(List<LineStation>));
            TextWriter writer = new StreamWriter("C:\\Temp\\LineStationXml.xml");
            x.Serialize(writer, DataSource.lineStationsList);
        }
        private static void SerializeLineTrip()
        {
            XmlSerializer x = new XmlSerializer(typeof(List<LineTrip>));
            TextWriter writer = new StreamWriter("C:\\Temp\\LineTripXml.xml");
            x.Serialize(writer, DataSource.lineTripsList);
        }
        private static void SerializeStation()
        {
            XmlSerializer x = new XmlSerializer(typeof(List<Station>));
            TextWriter writer = new StreamWriter("C:\\Temp\\StationXml.xml");
            x.Serialize(writer, DataSource.stationsList);
        }
        private static void SerializeConfig()
        { 
            MaxCounters maxCounters = new MaxCounters
            {
                MaxAdjacentStationsId = Configuration.MaxAdjacentStationsId,
                MaxLineId = Configuration.MaxLineId,
                MaxLineStationId = Configuration.MaxLineStationId,
                MaxLineTripId = Configuration.MaxLineTripId,
                MaxStationId = Configuration.MaxStationId

            };
            List<MaxCounters> lst = new List<MaxCounters>();
            lst.Add(maxCounters);
            XmlSerializer x = new XmlSerializer(typeof(List<MaxCounters>));
            TextWriter writer = new StreamWriter("C:\\Temp\\MaxCounters.xml");
            x.Serialize(writer, lst);
        }
    }
}
