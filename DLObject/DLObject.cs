﻿using DalApi;
using DO;
using DO.Exceptions;
using DS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DL
{
    sealed class DLObject : IDal
    {
        // מאפשר להקצות לכל היותר אובייקט אחד 
        #region singelton
        static readonly DLObject instance = new DLObject();
        static DLObject() { }// static ctor to ensure instance init is done just before first usage
        DLObject() { } // default => private
        public static DLObject Instance { get => instance; }// The public Instance property to use
        #endregion

        //Implement IDL methods, CRUD

        #region AdjacentStations
        public IEnumerable<AdjacentStations> GetAllAdjacentStations()
        {
            var allAdjacentStations = DataSource.adjacentStationsList.Where(adjacentStations => !adjacentStations.IsDeleted)
                                              .Select(adjacentStations => adjacentStations.Clone());
            return allAdjacentStations;

        }
        public IEnumerable<AdjacentStations> GetAllAdjacentStationsBy(Predicate<AdjacentStations> predicate)
        {
            var adjacentStationsBy = DataSource.adjacentStationsList.Where(adjacentStations => !adjacentStations.IsDeleted && predicate(adjacentStations))
                                                   .Select(adjacentStations => adjacentStations.Clone());
            return adjacentStationsBy;
        }
        public AdjacentStations GetAdjacentStationsById(int adjacentStationsId)
        {
            var adjacentStationsById = DataSource.adjacentStationsList.Where(adjacentStations => adjacentStations.AdjacentStationsId == adjacentStationsId)
                                                  .Select(adjacentStations => adjacentStations.Clone())
                                                  .FirstOrDefault();

            if (adjacentStationsById == null)
            {
                throw new AdjacentStationsNotFoundException(adjacentStationsId);
            }

            //if (adjacentStationsById.IsDeleted)
            //{
            //    throw new AdjacentStationsDeletedException(adjacentStationsId);
            //}

            return adjacentStationsById;
        }
        public void AddAdjacentStations(AdjacentStations adjacentStations)
        {
            var adjacentStationsExist = DataSource.adjacentStationsList.FirstOrDefault(a => a.AdjacentStationsId == adjacentStations.AdjacentStationsId);
            if (adjacentStationsExist != null)
            {
                throw new AdjacentStationsAlreadyExistsException(adjacentStations.AdjacentStationsId);

            }
            // בדיקה האם התחנות העוקבות עצמן כבר קיימות
            // יתכן שהן קיימות בסדר הפוך - ולכן גם את זה בדקנו
            adjacentStationsExist = DataSource.adjacentStationsList.FirstOrDefault(a => (a.StationId1 == adjacentStations.StationId1 &&
                                                                                        a.StationId2 == adjacentStations.StationId2) ||
                                                                                        (a.StationId1 == adjacentStations.StationId2 &&
                                                                                        a.StationId2 == adjacentStations.StationId1));
            if (adjacentStationsExist != null)
            {
                throw new AdjacentStationsAlreadyExistsException(adjacentStations.AdjacentStationsId);

            }
            adjacentStations.AdjacentStationsId = ++Configuration.MaxAdjacentStationsId;
            
            adjacentStations.Time = adjacentStations.Time.Duration();

            DataSource.adjacentStationsList.Add(adjacentStations.Clone());
        }
        public void UpdateAdjacentStations(AdjacentStations adjacentStations)
        {
            AdjacentStations adjacentStationsToUpdate = DataSource.adjacentStationsList.FirstOrDefault(a => a.AdjacentStationsId == adjacentStations.AdjacentStationsId);

            if (adjacentStationsToUpdate == null)
            {
                throw new AdjacentStationsNotFoundException(adjacentStations.AdjacentStationsId);
            }

            //if (adjacentStationsToUpdate.IsDeleted)
            //{
            //    throw new AdjacentStationsDeletedException(adjacentStations.AdjacentStationsId, "Cannot update deleted adjacent stations");
            //}

            DataSource.adjacentStationsList.Remove(adjacentStationsToUpdate);
            DataSource.adjacentStationsList.Add(adjacentStations.Clone());
        }
        public void UpdateAdjacentStations(AdjacentStations adjacentStations, Action<AdjacentStations> update)
        {
            AdjacentStations adjacentStationsToUpdate = DataSource.adjacentStationsList.FirstOrDefault(a => a.AdjacentStationsId == adjacentStations.AdjacentStationsId);

            if (adjacentStationsToUpdate == null)
            {
                throw new AdjacentStationsNotFoundException(adjacentStations.AdjacentStationsId);
            }

            //if (adjacentStationsToUpdate.IsDeleted)
            //{
            //    throw new AdjacentStationsDeletedException(adjacentStations.AdjacentStationsId, "Cannot update deleted adjacent stations");
            //}
            update(adjacentStationsToUpdate.Clone());
        }
        public void DeleteAdjacentStations(int adjacentStationsId)
        {
            // לא ניתן למשתמש למחוק זוג תחנות עוקבות
            // מחיקת זוג תחנות תעשה רק במקרה של מחיקת תחנה
            // לכן אין צורך לטפל במחיקת קשרים שיש לישות 

            var adjacentStationsToDelete = DataSource.adjacentStationsList.FirstOrDefault(adjacentStations => !adjacentStations.IsDeleted &&
                                                                                                     adjacentStations.AdjacentStationsId == adjacentStationsId);

            if (adjacentStationsToDelete == null)
            {
                throw new AdjacentStationsNotFoundException(adjacentStationsId, $"Cannot delete adjacent Stations Id: {adjacentStationsId} because it was not found");
            }


            adjacentStationsToDelete.IsDeleted = true;

        }

        public void DeleteAdjacentStationsBy(Predicate<AdjacentStations> predicate)
        {

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
            var allBuses = DataSource.busesList/*לא להיות סתומים !!!!.Where(bus => !bus.IsDeleted)*/
                                               .Select(bus => bus.Clone());
            return allBuses;

        }
        public IEnumerable<Bus> GetAllBussesBy(Predicate<Bus> predicate)
        {
            var bussesBy = DataSource.busesList.Where(bus => !bus.IsDeleted && predicate(bus))
                                               .Select(bus => bus.Clone());
            return bussesBy;
        }

        public Bus GetBusById(int licenseNumber)
        {
            var busById = DataSource.busesList.Where(bus => bus.LicenseNumber == licenseNumber)
                                               .Select(bus => bus.Clone())
                                               .FirstOrDefault();

            if (busById == null)
            {
                throw new BusNotFoundException(licenseNumber);
            }

            //if (busById.IsDeleted)
            //{
            //    throw new BusDeletedException(licenseNumber);
            //}

            return busById;
        }

        public void AddBus(Bus bus)
        {
            var busExist = DataSource.busesList.FirstOrDefault(b => b.LicenseNumber == bus.LicenseNumber);
            if (busExist != null)
            {
                throw new BusAlreadyExistsException(bus.LicenseNumber);

            }
            DataSource.busesList.Add(bus.Clone());
        }
       
            public void UpdateBus(Bus bus)

        {
            Bus busToUpdate = DataSource.busesList.Find(b => b.LicenseNumber == bus.LicenseNumber);

            if (busToUpdate == null)
            {
                throw new BusNotFoundException(bus.LicenseNumber);
            }

            DataSource.busesList.Remove(busToUpdate);
            DataSource.busesList.Add(bus.Clone());
        }

        public void UpdateBus(Bus bus, Action<Bus> update)
        {
            Bus busToUpdate = DataSource.busesList.Find(b => b.LicenseNumber == bus.LicenseNumber);

            if (busToUpdate == null)
            {
                throw new BusNotFoundException(bus.LicenseNumber);
            }

            //if (busToUpdate.IsDeleted)
            //{
            //    throw new BusDeletedException(bus.LicenseNumber, "Cannot update deleted bus");
            //}
            update(busToUpdate.Clone());
        }
        public void DeleteBus(int licenseNumber)
        {
            // פעולת המחיקה לא באמת מוחקת אלא מעדכנת את האוטובוס

            var busToDelete = DataSource.busesList.Find(bus => !bus.IsDeleted && bus.LicenseNumber == licenseNumber);

            if (busToDelete == null)
            {
                throw new BusNotFoundException(licenseNumber, $"Cannot delete licenseNumber: {licenseNumber} because it was not found");
            }
           
            busToDelete.IsDeleted = true;

        }
        public void DeleteBusBy(Predicate<Bus> predicate)
        {
            var allBussesBy = GetAllBussesBy(predicate);
            if (allBussesBy != null)
            {
                var allBussesByList = allBussesBy.ToList();
                allBussesByList.ForEach(b => DeleteBus(b.LicenseNumber));
            }
            else
            {
                throw new BusNotFoundException(0, $"Cannot delete bus For requested predicate: {predicate}");
            }

        }


        #endregion Bus


        #region Line
        public IEnumerable<Line> GetAllLine()
        {
            var allLine = DataSource.linesList.Select(line => line.Clone());

            if (allLine == null)
            {
                throw new LineNotFoundException(0, $"No Lines found in system");
            }
            return allLine;

        }
        public IEnumerable<Line> GetAllLineBy(Predicate<Line> predicate)
        {
            var lineBy = DataSource.linesList.Where(line => !line.IsDeleted && predicate(line))
                                                             .Select(line => line.Clone());


            if (lineBy == null)
            {
                throw new LineNotFoundException(0, $"No Lines found for requested predicate: {predicate}");
            }

            return lineBy;
        }
        public Line GetLineById(int lineId)
        {
            var lineById = DataSource.linesList.Where(line => line.LineId == lineId)
                                                     .Select(line => line.Clone())
                                                     .FirstOrDefault();

            if (lineById == null)
            {
                throw new LineNotFoundException(lineId);
            }
            return lineById;
        }
        public void AddLine(Line line)
        {
            var lineExist = DataSource.linesList.FirstOrDefault(l => l.LineId == line.LineId);
            if (lineExist != null)
            {
                throw new LineAlreadyExistsException(line.LineId);

            }
            DataSource.linesList.Add(line.Clone());
        }
        public void UpdateLine(Line line)
        {
            Line lineToUpdate = DataSource.linesList.Find(l => l.LineId == line.LineId);

            if (lineToUpdate == null)
            {
                throw new LineNotFoundException(line.LineId);
            }

            DataSource.linesList.Remove(lineToUpdate);
            DataSource.linesList.Add(line.Clone());
        }
        public void UpdateLine(Line line, Action<Line> update)
        {
            Line lineToUpdate = DataSource.linesList.Find(l => l.LineId == line.LineId);

            if (lineToUpdate == null)
            {
                throw new LineNotFoundException(line.LineId);
            }

            //if (lineToUpdate.IsDeleted)
            //{
            //    throw new LineDeletedException(line.LineId, "Cannot update deleted line");
            //}
            update(lineToUpdate.Clone());
        }
        public void DeleteLine(int lineId)
        {
            var lineToDelete = DataSource.linesList.Find(l => !l.IsDeleted && l.LineId == lineId);

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
                lineTripToDeleteList.ForEach(lt => DeleteLineTrip(lt.LineTripId, false));
            }

          
            lineToDelete.IsDeleted = true;
        }
        public void DeleteLineBy(Predicate<Line> predicate)
        {
            var allLineBy = GetAllLineBy(predicate);
            if (allLineBy != null)
            {
                var allLineByList = allLineBy.ToList();
                allLineByList.ForEach(b => DeleteLine(b.LineId));
            }
            else
            {
                throw new LineNotFoundException(0, $"Cannot delete line For requested predicate: {predicate}");
            }
        }

        #endregion Line


        #region LineStation
        public IEnumerable<LineStation> GetAllLineStation()
        {
            var allLineStations = DataSource.lineStationsList.Where(lineStation => !lineStation.IsDeleted)
                                                           .Select(lineStation => lineStation.Clone());
            return allLineStations;

        }
        public IEnumerable<LineStation> GetAllLineStationBy(Predicate<LineStation> predicate)
        {
            var LineStationBy = DataSource.lineStationsList.Where(lineStation => !lineStation.IsDeleted && predicate(lineStation))
                                                                 .Select(lineStation => lineStation.Clone());
            return LineStationBy;
        }
        public LineStation GetLineStationById(int lineStationId)
        {
            var LineStationById = DataSource.lineStationsList.Where(lineStation => lineStation.LineStationId == lineStationId)
                                                         .Select(lineStation => lineStation.Clone())
                                                         .FirstOrDefault();

            if (LineStationById == null)
            {
                throw new LineStationNotFoundException(lineStationId);
            }

            //if (LineStationById.IsDeleted)
            //{
            //    throw new LineStationDeletedException(lineStationId);
            //}

            return LineStationById;
        }
        public void AddLineStation(LineStation lineStation)
        {
            var lineStationExist = DataSource.lineStationsList.FirstOrDefault(l => l.LineStationId == lineStation.LineStationId);
            if (lineStationExist != null)
            {
                throw new LineStationAlreadyExistsException(lineStation.LineStationId);

            }
            lineStation.LineStationId = ++Configuration.MaxLineStationId;
            DataSource.lineStationsList.Add(lineStation.Clone());
        }

        public void UpdateLineStation(LineStation lineStation)
        {
            LineStation lineStationToUpdate = DataSource.lineStationsList.Find(l => l.LineStationId == lineStation.LineStationId);

            if (lineStationToUpdate == null)
            {
                throw new LineStationNotFoundException(lineStation.LineStationId);
            }

            //if (lineStationToUpdate.IsDeleted)
            //{
            //    throw new LineStationDeletedException(lineStation.LineStationId, "Cannot update deleted line station Id");
            //}

            DataSource.lineStationsList.Remove(lineStationToUpdate);
            DataSource.lineStationsList.Add(lineStation.Clone());
        }


        public void UpdateLineStation(LineStation lineStation, Action<LineStation> update)
        {
            LineStation LineStationToUpdate = DataSource.lineStationsList.Find(l => l.LineStationId == lineStation.LineStationId);

            if (LineStationToUpdate == null)
            {
                throw new LineStationNotFoundException(lineStation.LineStationId);
            }

            //if (LineStationToUpdate.IsDeleted)
            //{
            //    throw new LineStationDeletedException(lineStation.LineStationId, "Cannot update deleted LineStation");
            //}
            update(LineStationToUpdate.Clone());
        }
        public void DeleteLineStation(int lineStationId, bool isForcedDelete)
        {

            var lineStationToDelete = DataSource.lineStationsList.Find(lineStation => !lineStation.IsDeleted &&
                                                                                                     lineStation.LineStationId == lineStationId);

            if (lineStationToDelete == null)
            {
                throw new LineStationNotFoundException(lineStationId, $"Cannot delete line Station id: {lineStationId} because it was not found");
            }

            if (isForcedDelete)
            {
                DataSource.lineStationsList.Remove(lineStationToDelete);
            }
            else
            {
                lineStationToDelete.IsDeleted = true;

            }
        }
        public void DeleteLineStationBy(Predicate<LineStation> predicate)
        {
            var allLineStationBy = GetAllLineStationBy(predicate);
            if (allLineStationBy != null)
            {
                var allLineStationByList = allLineStationBy.ToList();
                allLineStationByList.ForEach(ls => DeleteLineStation(ls.LineStationId, false));
            }
            else
            {
                throw new LineStationNotFoundException(0, $"Cannot delete line station For requested predicate: {predicate}");
            }
        }

        #endregion LineStationStation


        #region LineTrip
        public IEnumerable<LineTrip> GetAllLineTrip()
        {
            var allLineTrips = DataSource.lineTripsList.Where(lineTrip => !lineTrip.IsDeleted)
                                                              .Select(lineTrip => lineTrip.Clone());
            return allLineTrips;
        }
        public IEnumerable<LineTrip> GetAllLineTripBy(Predicate<LineTrip> predicate)
        {
            var lineTripBy = DataSource.lineTripsList.Where(lineTrip => predicate(lineTrip))
                                                                    .Select(lineTrip => lineTrip.Clone());
            return lineTripBy;
        }
        public LineTrip GetLineTripById(int lineTripId)
        {
            var lineTripById = DataSource.lineTripsList.Where(lineTrip => lineTrip.LineTripId == lineTripId)
                                                            .Select(lineTrip => lineTrip.Clone())
                                                            .FirstOrDefault();

            if (lineTripById == null)
            {
                throw new LineTripNotFoundException(lineTripId);
            }

            //if (lineTripById.IsDeleted)
            //{
            //    throw new LineTripDeletedException(lineTripId);
            //}

            return lineTripById;
        }
        public void AddLineTrip(LineTrip lineTrip)
        {
            //var lineTripExist = DataSource.lineTripsList.FirstOrDefault(l => l.LineTripId == lineTrip.LineTripId);
            ////if (lineTripExist != null)
            ////{
            ////    throw new LineTripAlreadyExistsException(lineTrip.LineTripId);

            ////}
            DataSource.lineTripsList.Add(lineTrip.Clone());
        }
        public void UpdateLineTrip(LineTrip lineTrip)
        {
            LineTrip lineTripToUpdate = DataSource.lineTripsList.Find(l => l.LineTripId == lineTrip.LineTripId);

            if (lineTripToUpdate == null)
            {
                throw new LineTripNotFoundException(lineTrip.LineTripId);
            }

            //if (lineTripToUpdate.IsDeleted)
            //{
            //    throw new LineTripDeletedException(lineTrip.LineTripId, "Cannot update deleted line trip Id");
            //}

            DataSource.lineTripsList.Remove(lineTripToUpdate);
            DataSource.lineTripsList.Add(lineTrip.Clone());
        }
        public void UpdateLineTrip(LineTrip lineTrip, Action<LineTrip> update)
        {
            LineTrip lineTripToUpdate = DataSource.lineTripsList.Find(l => l.LineTripId == lineTrip.LineTripId);

            if (lineTripToUpdate == null)
            {
                throw new LineTripNotFoundException(lineTrip.LineTripId);
            }

            //if (lineTripToUpdate.IsDeleted)
            //{
            //    throw new LineTripDeletedException(lineTrip.LineTripId, "Cannot update deleted line Trip");
            //}
            update(lineTripToUpdate.Clone());
        }
        public void DeleteLineTrip(int lineTripId, bool isForcedDelete)
        {
            var lineTripToDelete = DataSource.lineTripsList.Find(lt => lt.LineTripId == lineTripId);

            if (lineTripToDelete == null)
            {
                throw new LineTripNotFoundException(lineTripId, $"Cannot delete line Trip id: {lineTripId} because it was not found");
            }

            if (isForcedDelete)
            {

                DataSource.lineTripsList.Remove(lineTripToDelete);
            }
            else
            {
                lineTripToDelete.IsDeleted = true;
            }
        }
        public void DeleteLineTripBy(Predicate<LineTrip> predicate)
        {
            var allLineTripBy = GetAllLineTripBy(predicate);
            if (allLineTripBy != null)
            {
                var allLineTripByList = allLineTripBy.ToList();
                allLineTripByList.ForEach(lt => DeleteLineTrip(lt.LineTripId, false));
            }
            else
            {
                throw new LineTripNotFoundException(0, $"Cannot delete line trip For requested predicate: {predicate}");
            }
        }


        #endregion LineTrip


        #region Station
        public IEnumerable<Station> GetAllStation()
        {
            var allstations = DataSource.stationsList.Select(station => station.Clone());
            return allstations;
        }
        public IEnumerable<Station> GetAllStationBy(Predicate<Station> predicate)
        {
            var stationBy = DataSource.stationsList.Select(station => station.Clone());
            return stationBy;
        }
        public Station GetStationById(int stationId)
        {
            var stationById = DataSource.stationsList.Where(station => station.StationId == stationId)
                                                              .Select(station => station.Clone())
                                                              .FirstOrDefault();

            if (stationById == null)
            {
                throw new StationNotFoundException(stationId);
            }

            //if (stationById.IsDeleted)
            //{
            //    throw new StationDeletedException(stationId);
            //}

            return stationById;
        }
        public void AddStation(Station station)
        {
            var stationExist = DataSource.stationsList.FirstOrDefault(l => l.StationId == station.StationId);
            if (stationExist != null)
            {
                throw new StationAlreadyExistsException(station.StationId);

            }
            DataSource.stationsList.Add(station.Clone());
        }
        public void UpdateStation(Station station)
        {
            Station stationToUpdate = DataSource.stationsList.Find(l => l.StationId == station.StationId);

            if (stationToUpdate == null)
            {
                throw new StationNotFoundException(station.StationId);
            }

            //if (stationToUpdate.IsDeleted)
            //{
            //    throw new StationDeletedException(station.StationId, "Cannot update deleted station Id");
            //}

            DataSource.stationsList.Remove(stationToUpdate);
            DataSource.stationsList.Add(station.Clone());
        }
        public void UpdateStation(Station station, Action<Station> update)
        {
            Station stationToUpdate = DataSource.stationsList.Find(l => l.StationId == station.StationId);

            if (stationToUpdate == null)
            {
                throw new StationNotFoundException(station.StationId);
            }

            //if (stationToUpdate.IsDeleted)
            //{
            //    throw new StationDeletedException(station.StationId, "Cannot update deleted station");
            //}
            update(stationToUpdate.Clone());
        }
        public void DeleteStation(int stationId)
        {
            var stationToDelete = DataSource.stationsList.Find(sl => !sl.IsDeleted && sl.StationId == stationId);

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

        }
        public void DeleteAdjacentStationsAndLineStation(LineStation lineStation)
        {
            var nextLineStation = DataSource.lineStationsList.Find(ls => ls.LineId == lineStation.LineId && ls.LineStationIndex == lineStation.LineStationIndex + 1);
            var prevLineStation = DataSource.lineStationsList.Find(ls => ls.LineId == lineStation.LineId && ls.LineStationIndex == lineStation.LineStationIndex - 1);

            // אם זאת תחנה ראשונה או אחרונה אין לי תחנות צמודות להוסיף
            if (nextLineStation != null && prevLineStation != null)
            {
                var currAndNextStation = GetAllAdjacentStationsBy(ajs => ajs.StationId1 == lineStation.StationId && ajs.StationId2 == nextLineStation.StationId || ajs.StationId2 == lineStation.StationId && ajs.StationId1 == nextLineStation.StationId).FirstOrDefault();
                var currAndPrevStation = GetAllAdjacentStationsBy(ajs => ajs.StationId1 == lineStation.StationId && ajs.StationId2 == prevLineStation.StationId || ajs.StationId2 == lineStation.StationId && ajs.StationId1 == prevLineStation.StationId).FirstOrDefault();
                AdjacentStations adjacentStations = new AdjacentStations();
                adjacentStations.StationId1 = prevLineStation.LineStationId;
                adjacentStations.StationId2 = nextLineStation.LineStationId;
                adjacentStations.IsDeleted = false;
                adjacentStations.Time = currAndNextStation.Time + currAndPrevStation.Time;
                adjacentStations.Distance = currAndNextStation.Distance + currAndPrevStation.Distance;
                AddAdjacentStations(adjacentStations);
            }
            DeleteAdjacentStationsBy(ajs => ajs.StationId1 == lineStation.StationId || ajs.StationId2 == lineStation.StationId);
            DeleteLineStation(lineStation.LineStationId, false);
        }
        public void DeleteStationBy(Predicate<Station> predicate)
        {
            var allStationBy = GetAllStationBy(predicate);
            if (allStationBy != null)
            {
                var allStationByList = allStationBy.ToList();
                allStationByList.ForEach(s => DeleteStation(s.StationId));
            }
            else
            {
                throw new StationNotFoundException(0, $"Cannot delete station For requested predicate: {predicate}");
            }
        }


        #endregion Station

    }
}
