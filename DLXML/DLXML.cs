using System;
using System.Collections.Generic;
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
        DLXML() { } // default => private
        public static DLXML Instance { get => instance; }// The public Instance property to use
        #endregion

        #region DS XML Files

        string adjacentStationsPath = @"AdjacentStationsXml.xml"; //XElement

        string busPath = @"BusXml.xml"; //XMLSerializer
        string busOnTripPath = @"BusOnTripXml.xml"; //XMLSerializer
        string linePath = @"LineXml.xml"; //XMLSerializer
        string lineStationPath = @"LineStationXml.xml"; //XMLSerializer
        string lineTripPath = @"LineTripXml.xml"; //XMLSerializer
        string stationPath = @"StationXml.xml"; //XMLSerializer

        #endregion

        #region AdjacentStations
        public IEnumerable<AdjacentStations> GetAllAdjacentStations()
        {
            List<AdjacentStations> adjacentStationsList = XMLTools.LoadListFromXMLSerializer<AdjacentStations>(adjacentStationsPath);
            var allAdjacentStations = adjacentStationsList.Where(adjacentStations => !adjacentStations.IsDeleted)
                                              .Select(adjacentStations => adjacentStations);
            return allAdjacentStations;

        }
        public IEnumerable<AdjacentStations> GetAllAdjacentStationsBy(Predicate<AdjacentStations> predicate)
        {
            List<AdjacentStations> adjacentStationsList = XMLTools.LoadListFromXMLSerializer<AdjacentStations>(adjacentStationsPath);
            var adjacentStationsBy = adjacentStationsList.Where(adjacentStations => !adjacentStations.IsDeleted && predicate(adjacentStations))
                                                   .Select(adjacentStations => adjacentStations);
            return adjacentStationsBy;
        }
        public AdjacentStations GetAdjacentStationsById(int adjacentStationsId)
        {
            List<AdjacentStations> adjacentStationsList = XMLTools.LoadListFromXMLSerializer<AdjacentStations>(adjacentStationsPath);
            var adjacentStationsById = adjacentStationsList.Where(adjacentStations => adjacentStations.AdjacentStationsId == adjacentStationsId)
                                                  .Select(adjacentStations => adjacentStations)
                                                  .FirstOrDefault();

            if (adjacentStationsById == null)
            {
                throw new AdjacentStationsNotFoundException(adjacentStationsId);
            }

            if (adjacentStationsById.IsDeleted)
            {
                throw new AdjacentStationsDeletedException(adjacentStationsId);
            }

            return adjacentStationsById;
        }
        public void AddAdjacentStations(AdjacentStations adjacentStations)
        {
            List<AdjacentStations> adjacentStationsList = XMLTools.LoadListFromXMLSerializer<AdjacentStations>(adjacentStationsPath);
            var adjacentStationsExist = adjacentStationsList.FirstOrDefault(a => a.AdjacentStationsId == adjacentStations.AdjacentStationsId);
            if (adjacentStationsExist != null)
            {
                throw new AdjacentStationsAlreadyExistsException(adjacentStations.AdjacentStationsId);

            }
            // בדיקה האם התחנות העוקבות עצמן כבר קיימות
            // יתכן שהן קיימות בסדר הפוך - ולכן גם את זה בדקנו
            adjacentStationsExist = adjacentStationsList.FirstOrDefault(a => (a.StationId1 == adjacentStations.StationId1 &&
                                                                                        a.StationId2 == adjacentStations.StationId2) ||
                                                                                        (a.StationId1 == adjacentStations.StationId2 &&
                                                                                        a.StationId2 == adjacentStations.StationId1));
            if (adjacentStationsExist != null)
            {
                throw new AdjacentStationsAlreadyExistsException(adjacentStations.AdjacentStationsId);

            }
            adjacentStations.AdjacentStationsId = ++Configuration.MaxAdjacentStationsId;

            adjacentStationsList.Add(adjacentStations);
            XMLTools.SaveListToXMLSerializer(adjacentStationsList, adjacentStationsPath);
        }
        public void UpdateAdjacentStations(AdjacentStations adjacentStations)
        {
            List<AdjacentStations> adjacentStationsList = XMLTools.LoadListFromXMLSerializer<AdjacentStations>(adjacentStationsPath);
            AdjacentStations adjacentStationsToUpdate = adjacentStationsList.Find(a => a.AdjacentStationsId == adjacentStations.AdjacentStationsId);

            if (adjacentStationsToUpdate == null)
            {
                throw new AdjacentStationsNotFoundException(adjacentStations.AdjacentStationsId);
            }

            if (adjacentStationsToUpdate.IsDeleted)
            {
                throw new AdjacentStationsDeletedException(adjacentStations.AdjacentStationsId, "Cannot update deleted adjacent stations");
            }

            adjacentStationsList.Remove(adjacentStationsToUpdate);
            adjacentStationsList.Add(adjacentStations);
            XMLTools.SaveListToXMLSerializer(adjacentStationsList, adjacentStationsPath);
        }
        public void UpdateAdjacentStations(AdjacentStations adjacentStations, Action<AdjacentStations> update)
        {
            List<AdjacentStations> adjacentStationsList = XMLTools.LoadListFromXMLSerializer<AdjacentStations>(adjacentStationsPath);
            AdjacentStations adjacentStationsToUpdate = adjacentStationsList.Find(a => a.AdjacentStationsId == adjacentStations.AdjacentStationsId);

            if (adjacentStationsToUpdate == null)
            {
                throw new AdjacentStationsNotFoundException(adjacentStations.AdjacentStationsId);
            }

            if (adjacentStationsToUpdate.IsDeleted)
            {
                throw new AdjacentStationsDeletedException(adjacentStations.AdjacentStationsId, "Cannot update deleted adjacent stations");
            }
            update(adjacentStationsToUpdate);
            XMLTools.SaveListToXMLSerializer(adjacentStationsList, adjacentStationsPath);
        }
        public void DeleteAdjacentStations(int adjacentStationsId)
        {
            List<AdjacentStations> adjacentStationsList = XMLTools.LoadListFromXMLSerializer<AdjacentStations>(adjacentStationsPath);
            // לא ניתן למשתמש למחוק זוג תחנות עוקבות
            // מחיקת זוג תחנות תעשה רק במקרה של מחיקת תחנה
            // לכן אין צורך לטפל במחיקת קשרים שיש לישות 

            var adjacentStationsToDelete = adjacentStationsList.Find(adjacentStations => !adjacentStations.IsDeleted &&
                                                                                                     adjacentStations.AdjacentStationsId == adjacentStationsId);

            if (adjacentStationsToDelete == null)
            {
                throw new AdjacentStationsNotFoundException(adjacentStationsId, $"Cannot delete adjacent Stations Id: {adjacentStationsId} because it was not found");
            }


            adjacentStationsToDelete.IsDeleted = true;
            XMLTools.SaveListToXMLSerializer(adjacentStationsList, adjacentStationsPath);

        }

        public void DeleteAdjacentStationsBy(Predicate<AdjacentStations> predicate)
        {
            List<AdjacentStations> adjacentStationsList = XMLTools.LoadListFromXMLSerializer<AdjacentStations>(adjacentStationsPath);

            var allAdjacentStationsBy = GetAllAdjacentStationsBy(predicate);
            if (allAdjacentStationsBy != null)
            {
                var allAdjacentStationsByList = allAdjacentStationsBy.ToList();
                allAdjacentStationsByList.ForEach(aj => DeleteAdjacentStations(aj.AdjacentStationsId));
            }
            else
            {
                throw new AdjacentStationsNotFoundException(0, $"Cannot delete adjacent Stations For requested predicate: {predicate}");
            }
        }

        #endregion AdjacentStations 

        #region Bus
        public IEnumerable<Bus> GetAllBusses()
        {
            List<Bus> busesList = XMLTools.LoadListFromXMLSerializer<Bus>(busPath);

            var allBuses = busesList.Where(bus => !bus.IsDeleted)
                                               .Select(bus => bus);
            return allBuses;

        }
        public IEnumerable<Bus> GetAllBussesBy(Predicate<Bus> predicate)
        {
            List<Bus> busesList = XMLTools.LoadListFromXMLSerializer<Bus>(busPath);
            var bussesBy = busesList.Where(bus => !bus.IsDeleted && predicate(bus))
                                               .Select(bus => bus);
            return bussesBy;
        }

        public Bus GetBusById(int licenseNumber)
        {
            List<Bus> busesList = XMLTools.LoadListFromXMLSerializer<Bus>(busPath);
            var busById = busesList.Where(bus => bus.LicenseNumber == licenseNumber)
                                               .Select(bus => bus)
                                               .FirstOrDefault();

            if (busById == null)
            {
                throw new BusNotFoundException(licenseNumber);
            }

            if (busById.IsDeleted)
            {
                throw new BusDeletedException(licenseNumber);
            }

            return busById;
        }

        public void AddBus(Bus bus)
        {
            List<Bus> busesList = XMLTools.LoadListFromXMLSerializer<Bus>(busPath);
            var busExist = busesList.FirstOrDefault(b => b.LicenseNumber == bus.LicenseNumber);
            if (busExist != null)
            {
                throw new BusAlreadyExistsException(bus.LicenseNumber);

            }
            busesList.Add(bus);
            XMLTools.SaveListToXMLSerializer(busesList, busPath);

        }
        public void UpdateBus(Bus bus)
        {
            List<Bus> busesList = XMLTools.LoadListFromXMLSerializer<Bus>(busPath);
            Bus busToUpdate = busesList.Find(b => b.LicenseNumber == bus.LicenseNumber);

            if (busToUpdate == null)
            {
                throw new BusNotFoundException(bus.LicenseNumber);
            }

            if (busToUpdate.IsDeleted)
            {
                throw new BusDeletedException(bus.LicenseNumber, "Cannot update deleted bus");
            }

            busesList.Remove(busToUpdate);
            busesList.Add(bus);
            XMLTools.SaveListToXMLSerializer(busesList, busPath);
        }
        public void UpdateBus(Bus bus, Action<Bus> update)
        {
            List<Bus> busesList = XMLTools.LoadListFromXMLSerializer<Bus>(busPath);
            Bus busToUpdate = busesList.Find(b => b.LicenseNumber == bus.LicenseNumber);

            if (busToUpdate == null)
            {
                throw new BusNotFoundException(bus.LicenseNumber);
            }

            if (busToUpdate.IsDeleted)
            {
                throw new BusDeletedException(bus.LicenseNumber, "Cannot update deleted bus");
            }
            update(busToUpdate);
            XMLTools.SaveListToXMLSerializer(busesList, busPath);
        }
        public void DeleteBus(int licenseNumber)
        {
            List<Bus> busesList = XMLTools.LoadListFromXMLSerializer<Bus>(busPath);
            // פעולת המחיקה לא באמת מוחקת אלא מעדכנת את האוטובוס

            var busToDelete = busesList.Find(bus => !bus.IsDeleted && bus.LicenseNumber == licenseNumber);


            if (busToDelete == null)
            {
                throw new BusNotFoundException(licenseNumber, $"Cannot delete licenseNumber: {licenseNumber} because it was not found");
            }

            busToDelete.IsDeleted = true;
            XMLTools.SaveListToXMLSerializer(busesList, busPath);

        }
        public void DeleteBusBy(Predicate<Bus> predicate)
        {
            List<Bus> busesList = XMLTools.LoadListFromXMLSerializer<Bus>(busPath);
            busesList.ForEach(b => DeleteBus(b.LicenseNumber));
        }


        #endregion Bus

        #region BusOnTrip
        public IEnumerable<BusOnTrip> GetAllBusOnTrip()
        {
            List<BusOnTrip> busOnTripsList = XMLTools.LoadListFromXMLSerializer<BusOnTrip>(busOnTripPath);
            var allBusesOnTrip = busOnTripsList.Where(busOnTrip => !busOnTrip.IsDeleted)
                                                   .Select(busOnTrip => busOnTrip);
            return allBusesOnTrip;

        }
        public IEnumerable<BusOnTrip> GetAllBusOnTripBy(Predicate<BusOnTrip> predicate)
        {
            List<BusOnTrip> busOnTripsList = XMLTools.LoadListFromXMLSerializer<BusOnTrip>(busOnTripPath);
            var busOnTripsBy = busOnTripsList.Where(busOnTrip => !busOnTrip.IsDeleted && predicate(busOnTrip))
                                                  .Select(busOnTrip => busOnTrip);
            return busOnTripsBy;
        }
        public BusOnTrip GetBusOnTripById(int busOnTripId)
        {
            List<BusOnTrip> busOnTripsList = XMLTools.LoadListFromXMLSerializer<BusOnTrip>(busOnTripPath);
            var busOnTripById = busOnTripsList.Where(busOnTrip => busOnTrip.BusOnTripId == busOnTripId)
                                                  .Select(busOnTrip => busOnTrip)
                                                  .FirstOrDefault();

            if (busOnTripById == null)
            {
                throw new BusOnTripNotFoundException(busOnTripId);
            }

            if (busOnTripById.IsDeleted)
            {
                throw new BusOnTripDeletedException(busOnTripId);
            }

            return busOnTripById;
        }
        public void AddBusOnTrip(BusOnTrip busOnTrip)
        {
            List<BusOnTrip> busOnTripsList = XMLTools.LoadListFromXMLSerializer<BusOnTrip>(busOnTripPath);
            var busOnTripExist = busOnTripsList.FirstOrDefault(b => b.BusOnTripId == busOnTrip.BusOnTripId);
            if (busOnTripExist != null)
            {
                throw new BusOnTripAlreadyExistsException(busOnTrip.BusOnTripId);

            }
            busOnTripsList.Add(busOnTrip);
            XMLTools.SaveListToXMLSerializer(busOnTripsList, busOnTripPath);
        }
        public void UpdateBusOnTrip(BusOnTrip busOnTrip)
        {
            List<BusOnTrip> busOnTripsList = XMLTools.LoadListFromXMLSerializer<BusOnTrip>(busOnTripPath);
            BusOnTrip busOnTripToUpdate = busOnTripsList.Find(b => b.BusOnTripId == busOnTrip.BusOnTripId);

            if (busOnTripToUpdate == null)
            {
                throw new BusOnTripNotFoundException(busOnTrip.BusOnTripId);
            }

            if (busOnTripToUpdate.IsDeleted)
            {
                throw new BusOnTripDeletedException(busOnTrip.BusOnTripId, "Cannot update deleted bus On Trip");
            }

            busOnTripsList.Remove(busOnTripToUpdate);
            busOnTripsList.Add(busOnTrip);
            XMLTools.SaveListToXMLSerializer(busOnTripsList, busOnTripPath);

        }
        public void UpdateBusOnTrip(BusOnTrip busOnTrip, Action<BusOnTrip> update)
        {
            List<BusOnTrip> busOnTripsList = XMLTools.LoadListFromXMLSerializer<BusOnTrip>(busOnTripPath);
            BusOnTrip busOnTripToUpdate = busOnTripsList.Find(b => b.BusOnTripId == busOnTrip.BusOnTripId);

            if (busOnTripToUpdate == null)
            {
                throw new BusOnTripNotFoundException(busOnTrip.BusOnTripId);
            }

            if (busOnTripToUpdate.IsDeleted)
            {
                throw new BusOnTripDeletedException(busOnTrip.BusOnTripId, "Cannot update deleted bus on trip");
            }
            update(busOnTripToUpdate);
            XMLTools.SaveListToXMLSerializer(busOnTripsList, busOnTripPath);

        }
        public void DeleteBusOnTrip(int busOnTripId)
        {
            List<BusOnTrip> busOnTripsList = XMLTools.LoadListFromXMLSerializer<BusOnTrip>(busOnTripPath);
            var busOnTripToDelete = busOnTripsList.Find(bon => !bon.IsDeleted && bon.BusOnTripId == busOnTripId);


            if (busOnTripToDelete == null)
            {
                throw new BusOnTripNotFoundException(busOnTripId, $"Cannot delete bus On Trip id : {busOnTripId} because it was not found");
            }


            busOnTripToDelete.IsDeleted = true;
            XMLTools.SaveListToXMLSerializer(busOnTripsList, busOnTripPath);

        }
        public void DeleteBusOnTripBy(Predicate<BusOnTrip> predicate)
        {
            List<BusOnTrip> busOnTripsList = XMLTools.LoadListFromXMLSerializer<BusOnTrip>(busOnTripPath);
            int deletedBusOnTrip = busOnTripsList.RemoveAll(predicate);
            if (deletedBusOnTrip == 0)
            {
                throw new BusOnTripNotFoundException(0, $"Cannot delete bus On Trip For requested predicate: {predicate}");
            }
        }

        #endregion BusOnTrip

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
            throw new NotImplementedException();
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
                lineStationsToDeleteList.ForEach(ls => DeleteLineStation(ls.LineStationId));
            }

            var lineTripToDelete = GetAllLineTripBy(a => a.LineId == lineId);
            if (lineTripToDelete != null)
            {
                var lineTripToDeleteList = lineTripToDelete.ToList();
                lineTripToDeleteList.ForEach(lt => DeleteLineTrip(lt.LineTripId));
            }

            var busOnTripToDelete = GetAllBusOnTripBy(a => a.LineId == lineId);
            if (busOnTripToDelete != null)
            {
                var busOnTripToDeleteList = busOnTripToDelete.ToList();
                busOnTripToDeleteList.ForEach(lt => DeleteBusOnTrip(lt.BusOnTripId));
            }

            lineToDelete.Element("IsDeleted").Value = "true";

            XMLTools.SaveListToXMLElement(lineRootElem, linePath);
        }

        private void DeleteLineStationLineTripAndBusOnTrip(LineStation lineStation)
        {
            DeleteLineStation(lineStation.LineStationId);

        }
        public void DeleteLineBy(Predicate<Line> predicate)
        {
            throw new NotImplementedException();
        }


        #endregion Line

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
        public void DeleteLineStation(int lineStationId)
        {
            List<LineStation> lineStationsList = XMLTools.LoadListFromXMLSerializer<LineStation>(lineStationPath);

            var lineStationToDelete = lineStationsList.Find(lineStation => !lineStation.IsDeleted &&
                                                                                                     lineStation.LineStationId == lineStationId);

            if (lineStationToDelete == null)
            {
                throw new LineStationNotFoundException(lineStationId, $"Cannot delete line Station id: {lineStationId} because it was not found");
            }

            lineStationToDelete.IsDeleted = true;
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

        #region LineTrip
        public IEnumerable<LineTrip> GetAllLineTrip()
        {
            List<LineTrip> lineTripsList = XMLTools.LoadListFromXMLSerializer<LineTrip>(lineTripPath);

            var allLineTrips = lineTripsList.Where(lineTrip => !lineTrip.IsDeleted)
                                                              .Select(lineTrip => lineTrip);
            return allLineTrips;
        }
        public IEnumerable<LineTrip> GetAllLineTripBy(Predicate<LineTrip> predicate)
        {
            List<LineTrip> lineTripsList = XMLTools.LoadListFromXMLSerializer<LineTrip>(lineTripPath);

            var lineTripBy = lineTripsList.Where(lineTrip => !lineTrip.IsDeleted && predicate(lineTrip))
                                                                    .Select(lineTrip => lineTrip);
            return lineTripBy;
        }
        public LineTrip GetLineTripById(int lineTripId)
        {
            List<LineTrip> lineTripsList = XMLTools.LoadListFromXMLSerializer<LineTrip>(lineTripPath);

            var lineTripById = lineTripsList.Where(lineTrip => lineTrip.LineTripId == lineTripId)
                                                            .Select(lineTrip => lineTrip)
                                                            .FirstOrDefault();

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
            List<LineTrip> lineTripsList = XMLTools.LoadListFromXMLSerializer<LineTrip>(lineTripPath);

            var lineTripExist = lineTripsList.FirstOrDefault(l => l.LineTripId == lineTrip.LineTripId);
            if (lineTripExist != null)
            {
                throw new LineTripAlreadyExistsException(lineTrip.LineTripId);

            }
            lineTripsList.Add(lineTrip);
            XMLTools.SaveListToXMLSerializer(lineTripsList, lineTripPath);

        }
        public void UpdateLineTrip(LineTrip lineTrip)
        {
            List<LineTrip> lineTripsList = XMLTools.LoadListFromXMLSerializer<LineTrip>(lineTripPath);

            LineTrip lineTripToUpdate = lineTripsList.Find(l => l.LineTripId == lineTrip.LineTripId);

            if (lineTripToUpdate == null)
            {
                throw new LineTripNotFoundException(lineTrip.LineTripId);
            }

            if (lineTripToUpdate.IsDeleted)
            {
                throw new LineTripDeletedException(lineTrip.LineTripId, "Cannot update deleted line trip Id");
            }

            lineTripsList.Remove(lineTripToUpdate);
            lineTripsList.Add(lineTrip);
            XMLTools.SaveListToXMLSerializer(lineTripsList, lineTripPath);

        }
        public void UpdateLineTrip(LineTrip lineTrip, Action<LineTrip> update)
        {
            List<LineTrip> lineTripsList = XMLTools.LoadListFromXMLSerializer<LineTrip>(lineTripPath);

            LineTrip lineTripToUpdate = lineTripsList.Find(l => l.LineTripId == lineTrip.LineTripId);

            if (lineTripToUpdate == null)
            {
                throw new LineTripNotFoundException(lineTrip.LineTripId);
            }

            if (lineTripToUpdate.IsDeleted)
            {
                throw new LineTripDeletedException(lineTrip.LineTripId, "Cannot update deleted line Trip");
            }
            update(lineTripToUpdate);
            XMLTools.SaveListToXMLSerializer(lineTripsList, lineTripPath);

        }
        public void DeleteLineTrip(int lineTripId)
        {
            List<LineTrip> lineTripsList = XMLTools.LoadListFromXMLSerializer<LineTrip>(lineTripPath);

            var lineTripToDelete = lineTripsList.Find(lt => !lt.IsDeleted && lt.LineTripId == lineTripId);

            if (lineTripToDelete == null)
            {
                throw new LineTripNotFoundException(lineTripId, $"Cannot delete line Trip id: {lineTripId} because it was not found");
            }

            lineTripsList.Remove(lineTripToDelete);
            XMLTools.SaveListToXMLSerializer(lineTripsList, lineTripPath);

        }
        public void DeleteLineTripBy(Predicate<LineTrip> predicate)
        {
            List<LineTrip> lineTripsList = XMLTools.LoadListFromXMLSerializer<LineTrip>(lineTripPath);

            int deletedLineTrip = lineTripsList.RemoveAll(predicate);
            if (deletedLineTrip == 0)
            {
                throw new LineStationNotFoundException(0, $"Cannot delete Line Trip For requested predicate: {predicate}");
            }
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
            DeleteLineStation(lineStation.LineStationId);
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