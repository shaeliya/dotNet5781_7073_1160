using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DalApi;
using DO;
using DO.Exceptions;
using DS;
//using DO;

namespace DL
{
    sealed class DLXML : IDal   //internal
    {
        
        #region singelton
        static readonly DLXML instance = new DLXML();
        static DLXML() { }// static ctor to ensure instance init is done just before first usage
        DLXML() {
        //    if (!File.Exists(busPath))
        //        DL.XMLTools.SaveListToXMLSerializer<DO.Bus>(DS.DataSource.busesList, busPath);

        //    if (!File.Exists(lineStationPath))
        //        DL.XMLTools.SaveListToXMLSerializer<DO.LineStation>(DS.DataSource.lineStationsList, lineStationPath);

        //    if (!File.Exists(linePath))
        //        DL.XMLTools.SaveListToXMLSerializer<DO.Line>(DS.DataSource.linesList, linePath);

        //    if (!File.Exists(stationPath))
        //        DL.XMLTools.SaveListToXMLSerializer<DO.Station>(DS.DataSource.stationsList, stationPath);

        //    if (!File.Exists(adjacentStationsPath))
        //        DL.XMLTools.SaveListToXMLSerializer<DO.AdjacentStations>(DS.DataSource.adjacentStationsList, adjacentStationsPath);

        //    if (!File.Exists(lineTripPath))
        //        DL.XMLTools.SaveListToXMLSerializer<DO.LineTrip>(DS.DataSource.lineTripsList, lineTripPath);
        } // default => private
        public static DLXML Instance { get => instance; }// The public Instance property to use
        #endregion

        #region DS XML Files

        string adjacentStationsPath = @"AdjacentStationsXml.xml"; //XElement
        string busPath = @"BusXml.xml"; //XMLSerializer
        string linePath = @"LineXml.xml"; //XMLSerializer
        string lineStationPath = @"LineStationXml.xml"; //XMLSerializer
        string lineTripPath = @"LineTripXml.xml"; //XMLSerializer
        string stationPath = @"StationXml.xml"; //XMLSerializer

        #endregion

        #region AdjacentStations
        public IEnumerable<AdjacentStations> GetAllAdjacentStations()
        {
            XElement adjacentStationsRootElem = XMLTools.LoadListFromXMLElement(adjacentStationsPath);

            var allAdjacentStations = from a in adjacentStationsRootElem.Elements()
                                      select new AdjacentStations()
                                      {

                                          AdjacentStationsId = Int32.Parse(a.Element("AdjacentStationsId").Value),
                                          StationId1 = Int32.Parse(a.Element("StationId1").Value),
                                          StationId2 = Int32.Parse(a.Element("StationId2").Value),
                                          Distance = double.Parse(a.Element("Distance").Value),
                                          Time = TimeSpan.ParseExact(a.Element("Time").Value, "hh\\:mm\\:ss", CultureInfo.InvariantCulture),
                                          IsDeleted = bool.Parse(a.Element("AdjacentStationsId").Value)
                                      };


            if (allAdjacentStations == null)
            {
                throw new LineNotFoundException(0, $"No Adjacent Stations found in system");
            }
            return allAdjacentStations;
        }
        public IEnumerable<AdjacentStations> GetAllAdjacentStationsBy(Predicate<AdjacentStations> predicate)
        {
            XElement adjacentStationsRootElem = XMLTools.LoadListFromXMLElement(adjacentStationsPath);

            var AdjacentStationsBy = from a in adjacentStationsRootElem.Elements()
                                     let AdjacentStations = new AdjacentStations()
                                     {
                                         AdjacentStationsId = Int32.Parse(a.Element("AdjacentStationsId").Value),
                                         StationId1 = Int32.Parse(a.Element("StationId1").Value),
                                         StationId2 = Int32.Parse(a.Element("StationId2").Value),
                                         Distance = double.Parse(a.Element("Distance").Value),
                                         Time = TimeSpan.ParseExact(a.Element("Time").Value, "hh\\:mm\\:ss", CultureInfo.InvariantCulture),
                                         IsDeleted = bool.Parse(a.Element("AdjacentStationsId").Value)

                                     }
                                     where predicate(AdjacentStations)
                                     select AdjacentStations;



            if (AdjacentStationsBy == null)
            {
                throw new LineNotFoundException(0, $"No Adjacent Stations found for requested predicate: {predicate}");
            }

            return AdjacentStationsBy;
        }
        public AdjacentStations GetAdjacentStationsById(int adjacentStationsId)
        {
            XElement adjacentStationsRootElem = XMLTools.LoadListFromXMLElement(adjacentStationsPath);


            var adjacentStationsById = (from a in adjacentStationsRootElem.Elements()
                            where int.Parse(a.Element("AdjacentStationsId").Value) == adjacentStationsId
                                        select new AdjacentStations()
                            {
                                AdjacentStationsId = Int32.Parse(a.Element("AdjacentStationsId").Value),
                                StationId1 = Int32.Parse(a.Element("StationId1").Value),
                                StationId2 = Int32.Parse(a.Element("StationId2").Value),
                                Distance = double.Parse(a.Element("Distance").Value),
                                Time = TimeSpan.ParseExact(a.Element("Time").Value, "hh\\:mm\\:ss", CultureInfo.InvariantCulture),
                                IsDeleted = bool.Parse(a.Element("AdjacentStationsId").Value)


                            }).FirstOrDefault();

            if (adjacentStationsById == null)
            {
                throw new LineNotFoundException(adjacentStationsId);
            }

            if (adjacentStationsById.IsDeleted)
            {
                throw new LineDeletedException(adjacentStationsId);
            }

            return adjacentStationsById;
        }
        public void AddAdjacentStations(AdjacentStations adjacentStations)
        {
            XElement adjacentStationsRootElem = XMLTools.LoadListFromXMLElement(adjacentStationsPath);


            var adjacentStationsExist = (from a in adjacentStationsRootElem.Elements()
                             where int.Parse(a.Element("AdjacentStationsId").Value) == adjacentStations.AdjacentStationsId
                                         select a).FirstOrDefault();

            if (adjacentStationsExist != null)
            {
                throw new LineAlreadyExistsException(adjacentStations.AdjacentStationsId);

            }
           
            XElement adjacentStationsElem = new XElement("AdjacentStations",
                                   new XElement("AdjacentStationsId", adjacentStations.AdjacentStationsId.ToString()),
                                   new XElement("StationId1", adjacentStations.StationId1.ToString()),
                                   new XElement("StationId2", adjacentStations.StationId2.ToString()),
                                   new XElement("Distance", adjacentStations.Distance.ToString()),
                                   new XElement("Time", adjacentStations.Time.ToString()),
                                   new XElement("IsDeleted", adjacentStations.IsDeleted.ToString()));

            adjacentStationsRootElem.Add(adjacentStationsElem);

            XMLTools.SaveListToXMLElement(adjacentStationsRootElem, linePath);
        }
        public void UpdateAdjacentStations(AdjacentStations adjacentStations)
        {
            XElement adjacentStationsRootElem = XMLTools.LoadListFromXMLElement(adjacentStationsPath);


            var adjacentStationsToUpdate = (from a in adjacentStationsRootElem.Elements()
                                            where int.Parse(a.Element("AdjacentStationsId").Value) == adjacentStations.AdjacentStationsId
                                            select a).FirstOrDefault();

            if (adjacentStationsToUpdate == null)
            {
                throw new LineNotFoundException(adjacentStations.AdjacentStationsId);
            }


            if (bool.Parse(adjacentStationsToUpdate.Element("IsDeleted").Value))
            {
                throw new LineDeletedException(adjacentStations.AdjacentStationsId, "Cannot update deleted adjacentStations");
            }

            adjacentStationsToUpdate.Element("AdjacentStationsId").Value = adjacentStations.AdjacentStationsId.ToString();
            adjacentStationsToUpdate.Element("StationId1").Value = adjacentStations.StationId1.ToString();
            adjacentStationsToUpdate.Element("StationId2").Value = adjacentStations.StationId2.ToString();
            adjacentStationsToUpdate.Element("Distance").Value = adjacentStations.Distance.ToString();
            adjacentStationsToUpdate.Element("Time").Value = adjacentStations.Time.ToString();
            adjacentStationsToUpdate.Element("IsDeleted").Value = adjacentStations.IsDeleted.ToString();
            XMLTools.SaveListToXMLElement(adjacentStationsRootElem, adjacentStationsPath);
        }
        public void UpdateAdjacentStations(AdjacentStations adjacentStations, Action<AdjacentStations> update)
        {
            XElement adjacentStationsRootElem = XMLTools.LoadListFromXMLElement(adjacentStationsPath);
           
            AdjacentStations aj = (from a in adjacentStationsRootElem.Elements()
                       where int.Parse(a.Element("AdjacentStationsId").Value) == adjacentStations.AdjacentStationsId
                      select new AdjacentStations { AdjacentStationsId = int.Parse(a.Element("AdjacentStationsId").Value) }).FirstOrDefault();
            if (aj != null)
            {
                update(aj);
                UpdateAdjacentStations(aj);
            }
        }

        public void DeleteAdjacentStations(int adjacentStationsId)
        {
            XElement adjacentStationsRootElem = XMLTools.LoadListFromXMLElement(adjacentStationsPath);
           
            var adjacentStationsToDelete = (from a in adjacentStationsRootElem.Elements()
                                            where int.Parse(a.Element("AdjacentStationsId").Value) == adjacentStationsId
                                            select a).FirstOrDefault();

            if (adjacentStationsToDelete == null)
            {
                throw new AdjacentStationsNotFoundException(adjacentStationsId, $"Cannot delete adjacent Stations id : {adjacentStationsId} because it was not found");
            }
            adjacentStationsToDelete.Element("IsDeleted").Value = "true";

            XMLTools.SaveListToXMLElement(adjacentStationsRootElem, adjacentStationsPath);
        }

        public void DeleteAdjacentStationsBy(Predicate<AdjacentStations> predicate)
        {
            throw new NotImplementedException();

        }

        #endregion AdjacentStations 
              
        #region Bus
        public IEnumerable<Bus> GetAllBusses()
        {
    
            XElement busRootElem = XMLTools.LoadListFromXMLElement(busPath);

            var allBus = from b in busRootElem.Elements()
                          select new Bus()
                          {
                              LicenseNumber=Int32.Parse(b.Element("LicenseNumber").Value),
                              FromDate= DateTime.Parse(b.Element("FromDate").Value),
                              TotalTrip= double.Parse(b.Element("TotalTrip").Value),
                              FuelRemain = double.Parse(b.Element("FuelRemain").Value),
                              Treatment = double.Parse(b.Element("Treatment").Value),
                              LastTreatmentDate = DateTime.Parse(b.Element("LastTreatmentDate").Value),
                              Status = (Enums.BusStatuses)Enum.Parse(typeof(Enums.BusStatuses), b.Element("BusStatuses").Value),
                              IsDeleted = bool.Parse(b.Element("LicenseNumber").Value)                          
                          };


            if (allBus == null)
            {
                throw new LineNotFoundException(0, $"No Bus found in system");
            }
            return allBus;

        }
        public IEnumerable<Bus> GetAllBussesBy(Predicate<Bus> predicate)
        {
            XElement busRootElem = XMLTools.LoadListFromXMLElement(busPath);

            var busBy = from b in busRootElem.Elements()
                        let bus = new Bus()
                        {
                            LicenseNumber = Int32.Parse(b.Element("LicenseNumber").Value),
                            FromDate = DateTime.Parse(b.Element("FromDate").Value),
                            TotalTrip = double.Parse(b.Element("TotalTrip").Value),
                            FuelRemain = double.Parse(b.Element("FuelRemain").Value),
                            Treatment = double.Parse(b.Element("Treatment").Value),
                            LastTreatmentDate = DateTime.Parse(b.Element("LastTreatmentDate").Value),
                            Status = (Enums.BusStatuses)Enum.Parse(typeof(Enums.BusStatuses), b.Element("BusStatuses").Value),
                            IsDeleted = bool.Parse(b.Element("LicenseNumber").Value)
                        }
                        where predicate(bus)
                        select bus;
            if (busBy == null)
            {
                throw new LineNotFoundException(0, $"No bus found for requested predicate: {predicate}");
            }

            return busBy;

        }
        public Bus GetBusById(int licenseNumber)
        {
            XElement busRootElem = XMLTools.LoadListFromXMLElement(busPath);

            var busById = (from b in busRootElem.Elements()
                            where int.Parse(b.Element("LicenseNumber").Value) == licenseNumber
                           select new Bus()
                            {
                                LicenseNumber = Int32.Parse(b.Element("LicenseNumber").Value),
                                FromDate = DateTime.Parse(b.Element("FromDate").Value),
                                TotalTrip = double.Parse(b.Element("TotalTrip").Value),
                                FuelRemain = double.Parse(b.Element("FuelRemain").Value),
                                Treatment = double.Parse(b.Element("Treatment").Value),
                                LastTreatmentDate = DateTime.Parse(b.Element("LastTreatmentDate").Value),
                                Status = (Enums.BusStatuses)Enum.Parse(typeof(Enums.BusStatuses), b.Element("BusStatuses").Value),
                                IsDeleted = bool.Parse(b.Element("LicenseNumber").Value)

                            }).FirstOrDefault();

            if (busById == null)
            {
                throw new LineNotFoundException(licenseNumber);
            }

            if (busById.IsDeleted)
            {
                throw new LineDeletedException(licenseNumber);
            }

            return busById;
        }
        public void AddBus(Bus bus)
        {
            XElement busRootElem = XMLTools.LoadListFromXMLElement(busPath);

            var busExist = (from b in busRootElem.Elements()
                             where int.Parse(b.Element("LicenseNumber").Value) == bus.LicenseNumber
                            select b).FirstOrDefault();

            if (busExist != null)
            {
                throw new LineAlreadyExistsException(bus.LicenseNumber);

            }

            XElement busElem = new XElement("bus",
                                   new XElement("LicenseNumber", bus.LicenseNumber.ToString()),
                                   new XElement("FromDate", bus.FromDate.ToString()),
                                   new XElement("TotalTrip", bus.TotalTrip.ToString()),
                                   new XElement("FuelRemain", bus.FuelRemain.ToString()),
                                   new XElement("Treatment", bus.Treatment.ToString()),
                                   new XElement("LastTreatmentDate", bus.LastTreatmentDate.ToString()),
                                   new XElement("Status", bus.Status.ToString()),
                                   new XElement("IsDeleted", bus.IsDeleted.ToString()));

            busRootElem.Add(busElem);

            XMLTools.SaveListToXMLElement(busRootElem, busPath);

        }
        public void UpdateBus(Bus bus)
        {
           
            XElement busRootElem = XMLTools.LoadListFromXMLElement(busPath);


            var busToUpdate = (from b in busRootElem.Elements()
                                where int.Parse(b.Element("LicenseNumber").Value) == bus.LicenseNumber
                               select b).FirstOrDefault();

            if (busToUpdate == null)
            {
                throw new LineNotFoundException(bus.LicenseNumber);
            }


            if (bool.Parse(busToUpdate.Element("IsDeleted").Value))
            {
                throw new LineDeletedException(bus.LicenseNumber, "Cannot update deleted bus");
            }

            busToUpdate.Element("LicenseNumber").Value = bus.LicenseNumber.ToString();
            busToUpdate.Element("FromDate").Value = bus.FromDate.ToString();
            busToUpdate.Element("TotalTrip").Value = bus.TotalTrip.ToString();
            busToUpdate.Element("FuelRemain").Value = bus.FuelRemain.ToString();
            busToUpdate.Element("Treatment").Value = bus.Treatment.ToString();
            busToUpdate.Element("LastTreatmentDate").Value = bus.LastTreatmentDate.ToString();
            busToUpdate.Element("Status").Value = bus.Status.ToString();
            busToUpdate.Element("IsDeleted").Value = bus.IsDeleted.ToString();

            XMLTools.SaveListToXMLElement(busRootElem, busPath);
        }
        public void UpdateBus(Bus bus, Action<Bus> update)
        {
            XElement busRootElem = XMLTools.LoadListFromXMLElement(busPath);

            Bus bs = (from b in busRootElem.Elements()
                       where int.Parse(b.Element("LicenseNumber").Value) == bus.LicenseNumber
                       select new Bus { LicenseNumber = int.Parse(b.Element("LicenseNumber").Value) }).FirstOrDefault();
            if (bs != null)
            {
                update(bs);
                UpdateBus(bs);
            }
        }

        public void DeleteBus(int licenseNumber)
        {
            XElement busRootElem = XMLTools.LoadListFromXMLElement(busPath);

            var busToDelete = (from b in busRootElem.Elements()
                                where int.Parse(b.Element("LicenseNumber").Value) == licenseNumber
                               select b).FirstOrDefault();

            if (busToDelete == null)
            {
                throw new BusNotFoundException(licenseNumber, $"Cannot delete license Number : {licenseNumber} because it was not found");
            }

            busToDelete.Element("IsDeleted").Value = "true";

            XMLTools.SaveListToXMLElement(busRootElem, busPath);

        }
        public void DeleteBusBy(Predicate<Bus> predicate)
        {
            throw new NotImplementedException();
        }


        #endregion Bus

        #region LineStation
        public IEnumerable<LineStation> GetAllLineStation()
        {
            List<LineStation> lineStationsList = XMLTools.LoadListFromXMLSerializer<LineStation>(lineStationPath);

            var allLineStations = lineStationsList.Where(lineStation => !lineStation.IsDeleted)
                                                           .Select(lineStation => lineStation);
            return allLineStations;

        }
        public IEnumerable<LineStation> GetAllLineStationBy(Predicate<LineStation> predicate)
        {
            List<LineStation> lineStationsList = XMLTools.LoadListFromXMLSerializer<LineStation>(lineStationPath);

            var LineStationBy = lineStationsList.Where(lineStation => !lineStation.IsDeleted && predicate(lineStation))
                                                                 .Select(lineStation => lineStation);
            return LineStationBy;
        }
        public LineStation GetLineStationById(int lineStationId)
        {
            List<LineStation> lineStationsList = XMLTools.LoadListFromXMLSerializer<LineStation>(lineStationPath);

            var LineStationById = lineStationsList.Where(lineStation => lineStation.LineStationId == lineStationId)
                                                         .Select(lineStation => lineStation)
                                                         .FirstOrDefault();

            if (LineStationById == null)
            {
                throw new LineStationNotFoundException(lineStationId);
            }

            if (LineStationById.IsDeleted)
            {
                throw new LineStationDeletedException(lineStationId);
            }

            return LineStationById;
        }
        public void AddLineStation(LineStation lineStation)
        {
            List<LineStation> lineStationsList = XMLTools.LoadListFromXMLSerializer<LineStation>(lineStationPath);

            var lineStationExist = lineStationsList.FirstOrDefault(l => l.LineStationId == lineStation.LineStationId);
            if (lineStationExist != null)
            {
                throw new LineStationAlreadyExistsException(lineStation.LineStationId);

            }
            lineStationsList.Add(lineStation);
            XMLTools.SaveListToXMLSerializer(lineStationsList, lineStationPath);

        }

        public void UpdateLineStation(LineStation lineStation)
        {
            List<LineStation> lineStationsList = XMLTools.LoadListFromXMLSerializer<LineStation>(lineStationPath);

            LineStation lineStationToUpdate = lineStationsList.Find(l => l.LineStationId == lineStation.LineStationId);

            if (lineStationToUpdate == null)
            {
                throw new LineStationNotFoundException(lineStation.LineStationId);
            }

            if (lineStationToUpdate.IsDeleted)
            {
                throw new LineStationDeletedException(lineStation.LineStationId, "Cannot update deleted line station Id");
            }

            lineStationsList.Remove(lineStationToUpdate);
            lineStationsList.Add(lineStation); 
            XMLTools.SaveListToXMLSerializer(lineStationsList, lineStationPath);

        }


        public void UpdateLineStation(LineStation lineStation, Action<LineStation> update)
        {
            List<LineStation> lineStationsList = XMLTools.LoadListFromXMLSerializer<LineStation>(lineStationPath);

            LineStation LineStationToUpdate = lineStationsList.Find(l => l.LineStationId == lineStation.LineStationId);

            if (LineStationToUpdate == null)
            {
                throw new LineStationNotFoundException(lineStation.LineStationId);
            }

            if (LineStationToUpdate.IsDeleted)
            {
                throw new LineStationDeletedException(lineStation.LineStationId, "Cannot update deleted LineStation");
            }
            update(LineStationToUpdate);
            XMLTools.SaveListToXMLSerializer(lineStationsList, lineStationPath);

        }
        public void DeleteLineStation(int lineStationId, bool isForcedDelete)
        {
            List<LineStation> lineStationsList = XMLTools.LoadListFromXMLSerializer<LineStation>(lineStationPath);

            var lineStationToDelete = lineStationsList.Find(lineStation => !lineStation.IsDeleted &&
                                                                                                     lineStation.LineStationId == lineStationId);

            if (lineStationToDelete == null)
            {
                throw new LineStationNotFoundException(lineStationId, $"Cannot delete line Station id: {lineStationId} because it was not found");
            }
            if (isForcedDelete)
            {

                lineStationsList.Remove(lineStationToDelete);
            }
            else
            {
                lineStationToDelete.IsDeleted = true;
            }

            XMLTools.SaveListToXMLSerializer(lineStationsList, lineStationPath);

        }
        public void DeleteLineStationBy(Predicate<LineStation> predicate)
        {
            List<LineStation> lineStationsList = XMLTools.LoadListFromXMLSerializer<LineStation>(lineStationPath);

            int deletedLineStation = lineStationsList.RemoveAll(predicate);
            if (deletedLineStation == 0)
            {
                throw new LineStationNotFoundException(0, $"Cannot delete Line Station For requested predicate: {predicate}");
            }
        }

        #endregion LineStationStation

        #region Line
        public IEnumerable<Line> GetAllLine()
        {

            XElement lineRootElem = XMLTools.LoadListFromXMLElement(linePath);

            var allLine = from l in lineRootElem.Elements()
                          select new Line()
                          {
                              LineId = Int32.Parse(l.Element("LineId").Value),
                              LineNumber = Int32.Parse(l.Element("LineNumber").Value),
                              Area = (Enums.Areas)Enum.Parse(typeof(Enums.Areas), l.Element("Area").Value),
                              IsDeleted = bool.Parse(l.Element("LineNumber").Value)

                          };


            if (allLine == null)
            {
                throw new LineNotFoundException(0, $"No Lines found in system");
            }
            return allLine;

        }
        public IEnumerable<Line> GetAllLineBy(Predicate<Line> predicate)
        {
            XElement lineRootElem = XMLTools.LoadListFromXMLElement(linePath);

            var lineBy = from l in lineRootElem.Elements()
                         let line = new Line()
                         {
                             LineId = Int32.Parse(l.Element("LineId").Value),
                             LineNumber = Int32.Parse(l.Element("LineNumber").Value),
                             Area = (Enums.Areas)Enum.Parse(typeof(Enums.Areas), l.Element("Area").Value),
                             IsDeleted = bool.Parse(l.Element("LineNumber").Value)

                         }
                         where predicate(line)
                         select line;



            if (lineBy == null)
            {
                throw new LineNotFoundException(0, $"No Lines found for requested predicate: {predicate}");
            }

            return lineBy;
        }
        public Line GetLineById(int lineId)
        {
            XElement lineRootElem = XMLTools.LoadListFromXMLElement(linePath);

            var lineById = (from l in lineRootElem.Elements()
                            where int.Parse(l.Element("LineId").Value) == lineId
                            select new Line()
                            {
                                LineId = Int32.Parse(l.Element("LineId").Value),
                                LineNumber = Int32.Parse(l.Element("LineNumber").Value),
                                Area = (Enums.Areas)Enum.Parse(typeof(Enums.Areas), l.Element("Area").Value),
                                IsDeleted = bool.Parse(l.Element("LineNumber").Value)

                            }).FirstOrDefault();

            if (lineById == null)
            {
                throw new LineNotFoundException(lineId);
            }

            if (lineById.IsDeleted)
            {
                throw new LineDeletedException(lineId);
            }

            return lineById;
        }
        public void AddLine(Line line)
        {
            XElement lineRootElem = XMLTools.LoadListFromXMLElement(linePath);


            var lineExist = (from l in lineRootElem.Elements()
                             where int.Parse(l.Element("LineId").Value) == line.LineId
                             select l).FirstOrDefault();

            if (lineExist != null)
            {
                throw new LineAlreadyExistsException(line.LineId);

            }

            XElement lineElem = new XElement("Line",
                                   new XElement("LineId", line.LineId.ToString()),
                                   new XElement("LineNumber", line.LineNumber.ToString()),
                                   new XElement("Area", line.Area.ToString()),
                                   new XElement("IsDeleted", line.IsDeleted.ToString()));

            lineRootElem.Add(lineElem);

            XMLTools.SaveListToXMLElement(lineRootElem, linePath);
        }
        public void UpdateLine(Line line)
        {
            XElement lineRootElem = XMLTools.LoadListFromXMLElement(linePath);


            var lineToUpdate = (from l in lineRootElem.Elements()
                                where int.Parse(l.Element("LineId").Value) == line.LineId
                                select l).FirstOrDefault();

            if (lineToUpdate == null)
            {
                throw new LineNotFoundException(line.LineId);
            }


            if (bool.Parse(lineToUpdate.Element("IsDeleted").Value))
            {
                throw new LineDeletedException(line.LineId, "Cannot update deleted line");
            }

            lineToUpdate.Element("LineId").Value = line.LineId.ToString();
            lineToUpdate.Element("LineNumber").Value = line.LineNumber.ToString();
            lineToUpdate.Element("Area").Value = line.Area.ToString();
            lineToUpdate.Element("IsDeleted").Value = line.IsDeleted.ToString();

            XMLTools.SaveListToXMLElement(lineRootElem, linePath);
        }
        public void UpdateLine(Line line, Action<Line> update)
        {
            XElement lineRootElem = XMLTools.LoadListFromXMLElement(linePath);
            Line ln = (from l in lineRootElem.Elements()
                       where int.Parse(l.Element("LineId").Value) == line.LineId
                       select new Line { LineNumber = int.Parse(l.Element("LineNumber").Value) }).FirstOrDefault();
            if (ln != null)
            {
                update(ln);
                UpdateLine(ln);
            }
        }
        public void DeleteLine(int lineId)
        {
            XElement lineRootElem = XMLTools.LoadListFromXMLElement(linePath);

            var lineToDelete = (from l in lineRootElem.Elements()
                                where int.Parse(l.Element("LineId").Value) == lineId
                                select l).FirstOrDefault();

            if (lineToDelete == null)
            {
                throw new LineNotFoundException(lineId, $"Cannot delete line id : {lineId} because it was not found");
            }

            var lineStationsToDelete = GetAllLineStationBy(a => a.LineId == lineId);
            if (lineStationsToDelete != null)
            {
                var lineStationsToDeleteList = lineStationsToDelete.ToList();
                lineStationsToDeleteList.ForEach(ls => DeleteLineStation(ls.LineStationId, false));
            }

            var lineTripToDelete = GetAllLineTripBy(a => a.LineId == lineId);
            if (lineTripToDelete != null)
            {
                var lineTripToDeleteList = lineTripToDelete.ToList();
                lineTripToDeleteList.ForEach(lt => DeleteLineTrip(lt.LineTripId));
            }


            lineToDelete.Element("IsDeleted").Value = "true";

            XMLTools.SaveListToXMLElement(lineRootElem, linePath);
        }
        public void DeleteLineBy(Predicate<Line> predicate)
        {
            throw new NotImplementedException();
        }

        #endregion Line

        #region LineTrip
        public IEnumerable<LineTrip> GetAllLineTrip()
        {
            XElement lineTripRootElem = XMLTools.LoadListFromXMLElement(lineTripPath);

            var allLineTrip = from l in lineTripRootElem.Elements()
                              select new LineTrip()
                              {
                                  LineTripId = Int32.Parse(l.Element("LineTripId").Value),
                                  LineId = Int32.Parse(l.Element("LineId").Value),
                                  StartAt = TimeSpan.ParseExact(l.Element("StartAt").Value, "hh\\:mm\\:ss", CultureInfo.InvariantCulture),
                                  IsDeleted = bool.Parse(l.Element("LineTripId").Value)

                              };


            if (allLineTrip == null)
            {
                throw new LineNotFoundException(0, $"No Line Trip found in system");
            }
            return allLineTrip;
        }
        public IEnumerable<LineTrip> GetAllLineTripBy(Predicate<LineTrip> predicate)
        {
            XElement lineTripRootElem = XMLTools.LoadListFromXMLElement(lineTripPath);

            var lineTripBy = from l in lineTripRootElem.Elements()
                             let lineTrip = new LineTrip()
                             {
                                 LineTripId = Int32.Parse(l.Element("LineTripId").Value),
                                 LineId = Int32.Parse(l.Element("LineId").Value),
                                 StartAt = TimeSpan.ParseExact(l.Element("StartAt").Value, "hh\\:mm\\:ss", CultureInfo.InvariantCulture),
                                 IsDeleted = bool.Parse(l.Element("LineTripId").Value)

                             }
                             where predicate(lineTrip)
                             select lineTrip;



            if (lineTripBy == null)
            {
                throw new LineNotFoundException(0, $"No line Trip found for requested predicate: {predicate}");
            }

            return lineTripBy;
        }
        public LineTrip GetLineTripById(int lineTripId)
        {
        XElement lineTripRootElem = XMLTools.LoadListFromXMLElement(lineTripPath);
            var lineTripById = (from l in lineTripRootElem.Elements()
                            where int.Parse(l.Element("LineTripId").Value) == lineTripId
                                select new LineTrip()
                            {
                                LineTripId = Int32.Parse(l.Element("LineTripId").Value),
                                LineId = Int32.Parse(l.Element("LineId").Value),
                                StartAt = TimeSpan.ParseExact(l.Element("StartAt").Value, "hh\\:mm\\:ss", CultureInfo.InvariantCulture),
                                IsDeleted = bool.Parse(l.Element("LineTripId").Value)

                            }).FirstOrDefault();

            if (lineTripById == null)
            {
                throw new LineTripNotFoundException(lineTripId);
            }

            if (lineTripById.IsDeleted)
            {
                throw new LineTripDeletedException(lineTripId);
            }

            return lineTripById; 
            
        }
        public void AddLineTrip(LineTrip lineTrip)

        {
            XElement lineTripRootElem = XMLTools.LoadListFromXMLElement(lineTripPath);
            var lineTripExist = (from l in lineTripRootElem.Elements()
                                 where int.Parse(l.Element("LineTripId").Value) == lineTrip.LineTripId
                                 select l).FirstOrDefault();

            if (lineTripExist != null)
            {
                throw new LineTripAlreadyExistsException(lineTrip.LineTripId);

            }

            XElement lineTripElem = new XElement("LineTrip",
                                   new XElement("LineTripId", lineTrip.LineTripId.ToString()),
                                   new XElement("LineId", lineTrip.LineId.ToString()),
                                   new XElement("StartAt", lineTrip.StartAt.ToString()),
                                   new XElement("IsDeleted", lineTrip.IsDeleted.ToString()));

            lineTripRootElem.Add(lineTripElem);

            XMLTools.SaveListToXMLElement(lineTripRootElem, lineTripPath);

        }
        public void UpdateLineTrip(LineTrip lineTrip)
        {
        XElement lineTripRootElem = XMLTools.LoadListFromXMLElement(lineTripPath);
            var lineTripToUpdate = (from l in lineTripRootElem.Elements()
                                where int.Parse(l.Element("LineTripId").Value) == lineTrip.LineTripId
                                select l).FirstOrDefault();

            if (lineTripToUpdate == null)
            {
                throw new LineTripNotFoundException(lineTrip.LineTripId);
            }


            if (bool.Parse(lineTripToUpdate.Element("IsDeleted").Value))
            {
                throw new LineTripDeletedException(lineTrip.LineTripId, "Cannot update deleted line Trip");
            }

            lineTripToUpdate.Element("LineTripId").Value = lineTrip.LineTripId.ToString();
            lineTripToUpdate.Element("LineId").Value = lineTrip.LineId.ToString();
            lineTripToUpdate.Element("StartAt").Value = lineTrip.StartAt.ToString();
            lineTripToUpdate.Element("IsDeleted").Value = lineTrip.IsDeleted.ToString();

            XMLTools.SaveListToXMLElement(lineTripRootElem, lineTripPath);
        }

        public void UpdateLineTrip(LineTrip lineTrip, Action<LineTrip> update)
        {
        XElement lineTripRootElem = XMLTools.LoadListFromXMLElement(lineTripPath);
            LineTrip lt = (from l in lineTripRootElem.Elements()
                       where int.Parse(l.Element("LineTripId").Value) == lineTrip.LineTripId
                       select new LineTrip { LineTripId = int.Parse(l.Element("LineTripId").Value) }).FirstOrDefault();
            if (lt != null)
            {
                update(lt);
                UpdateLineTrip(lt);
            }
        }
        public void DeleteLineTrip(int lineTripId)
        {
        XElement lineTripRootElem = XMLTools.LoadListFromXMLElement(lineTripPath);

            var lineTripToDelete = (from l in lineTripRootElem.Elements()
                                where int.Parse(l.Element("LineTripId").Value) == lineTripId
                                select l).FirstOrDefault();

            if (lineTripToDelete == null)
            {
                throw new LineTripNotFoundException(lineTripId, $"Cannot delete line Trip id : {lineTripId} because it was not found");
            }

            lineTripToDelete.Element("IsDeleted").Value = "true";

            XMLTools.SaveListToXMLElement(lineTripRootElem, lineTripPath);
        }
        public void DeleteLineTripBy(Predicate<LineTrip> predicate)
        {
            throw new NotImplementedException();
        }


        #endregion LineTrip

        #region Station

        public IEnumerable<Station> GetAllStation()
        {

            List<Station> stationsList = XMLTools.LoadListFromXMLSerializer<Station>(stationPath);

            var allstations = stationsList.Where(station => !station.IsDeleted)
                                                                 .Select(station => station);
            return allstations;
        }
        public IEnumerable<Station> GetAllStationBy(Predicate<Station> predicate)
        {
            List<Station> stationsList = XMLTools.LoadListFromXMLSerializer<Station>(stationPath);

            var stationBy = stationsList.Where(station => !station.IsDeleted && predicate(station))
                                                                       .Select(station => station);
            return stationBy;
        }
        public Station GetStationById(int stationId)
        {
            List<Station> stationsList = XMLTools.LoadListFromXMLSerializer<Station>(stationPath);

            var stationById = stationsList.Where(station => station.StationId == stationId)
                                                              .Select(station => station)
                                                              .FirstOrDefault();

            if (stationById == null)
            {
                throw new StationNotFoundException(stationId);
            }

            if (stationById.IsDeleted)
            {
                throw new StationDeletedException(stationId);
            }

            return stationById;
        }
        public void AddStation(Station station)
        {
            List<Station> stationsList = XMLTools.LoadListFromXMLSerializer<Station>(stationPath);

            var stationExist = stationsList.FirstOrDefault(l => l.StationId == station.StationId);
            if (stationExist != null)
            {
                throw new StationAlreadyExistsException(station.StationId);

            }
            stationsList.Add(station);
            XMLTools.SaveListToXMLSerializer(stationsList, stationPath);

        }
        public void UpdateStation(Station station)
        {
            List<Station> stationsList = XMLTools.LoadListFromXMLSerializer<Station>(stationPath);

            Station stationToUpdate = stationsList.Find(l => l.StationId == station.StationId);

            if (stationToUpdate == null)
            {
                throw new StationNotFoundException(station.StationId);
            }

            if (stationToUpdate.IsDeleted)
            {
                throw new StationDeletedException(station.StationId, "Cannot update deleted station Id");
            }

            stationsList.Remove(stationToUpdate);
            stationsList.Add(station);
            XMLTools.SaveListToXMLSerializer(stationsList, stationPath);

        }
        public void UpdateStation(Station station, Action<Station> update)
        {
            List<Station> stationsList = XMLTools.LoadListFromXMLSerializer<Station>(stationPath);

            Station stationToUpdate = stationsList.Find(l => l.StationId == station.StationId);

            if (stationToUpdate == null)
            {
                throw new StationNotFoundException(station.StationId);
            }

            if (stationToUpdate.IsDeleted)
            {
                throw new StationDeletedException(station.StationId, "Cannot update deleted station");
            }
            update(stationToUpdate);
            XMLTools.SaveListToXMLSerializer(stationsList, stationPath);

        }
        public void DeleteStation(int stationId)
        {
            List<Station> stationsList = XMLTools.LoadListFromXMLSerializer<Station>(stationPath);

            var stationToDelete = stationsList.Find(sl => !sl.IsDeleted && sl.StationId == stationId);

            if (stationToDelete == null)
            {
                throw new StationNotFoundException(stationId, $"Cannot delete station id: {stationId} because it was not found");
            }

            var lineStationsToDelete = GetAllLineStationBy(a => a.StationId == stationId);
            if (lineStationsToDelete != null)
            {
                var lineStationsToDeleteList = lineStationsToDelete.ToList();
                lineStationsToDeleteList.ForEach(ls => DeleteAdjacentStationsAndLineStation(ls));
            }

            stationToDelete.IsDeleted = true;
            XMLTools.SaveListToXMLSerializer(stationsList, stationPath);


        }
        public void DeleteAdjacentStationsAndLineStation(LineStation lineStation)
        {
            List<LineStation> lineStationsList = XMLTools.LoadListFromXMLSerializer<LineStation>(lineStationPath);

            var nextLineStation = lineStationsList.Find(ls => ls.LineId == lineStation.LineId && ls.LineStationIndex == lineStation.LineStationIndex + 1);
            var prevLineStation = lineStationsList.Find(ls => ls.LineId == lineStation.LineId && ls.LineStationIndex == lineStation.LineStationIndex - 1);

            var currAndNextStation = GetAllAdjacentStationsBy(ajs => ajs.StationId1 == lineStation.StationId && ajs.StationId2 == nextLineStation.StationId || ajs.StationId2 == lineStation.StationId && ajs.StationId1 == nextLineStation.StationId).FirstOrDefault();
            var currAndPrevStation = GetAllAdjacentStationsBy(ajs => ajs.StationId1 == lineStation.StationId && ajs.StationId2 == prevLineStation.StationId || ajs.StationId2 == lineStation.StationId && ajs.StationId1 == prevLineStation.StationId).FirstOrDefault();
            AdjacentStations adjacentStations = new AdjacentStations();
            adjacentStations.StationId1 = prevLineStation.LineStationId;
            adjacentStations.StationId2 = nextLineStation.LineStationId;
            adjacentStations.IsDeleted = false;
            adjacentStations.Time = currAndNextStation.Time + currAndPrevStation.Time;
            adjacentStations.Distance = currAndNextStation.Distance + currAndPrevStation.Distance;
            AddAdjacentStations(adjacentStations);
            DeleteAdjacentStationsBy(ajs => ajs.StationId1 == lineStation.StationId || ajs.StationId2 == lineStation.StationId);
            DeleteLineStation(lineStation.LineStationId, false);
            DeleteLineStation(lineStation.LineStationId, false);
            XMLTools.SaveListToXMLSerializer(lineStationsList, lineStationPath);

        }
        public void DeleteStationBy(Predicate<Station> predicate)
        {
            List<Station> stationsList = XMLTools.LoadListFromXMLSerializer<Station>(stationPath);

            int deletedStation = stationsList.RemoveAll(predicate);
            if (deletedStation == 0)
            {
                throw new StationNotFoundException(0, $"Cannot delete Station For requested predicate: {predicate}");
            }
        }


        #endregion Station

    }
}