using DalApi;
using DO;
using DO.Exceptions;
using DS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//יש מחיקה של מופע של ישות שגורר בעקבותיו מחיקה של ישויות נוספות שמקושרות אליו.

namespace DL
{
    sealed class DLObject :IDal
    {
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

            if (adjacentStationsById.IsDeleted)
            {
                throw new AdjacentStationsDeletedException(adjacentStationsId);
            }

            return adjacentStationsById;
        } 
        public void AddAdjacentStations(AdjacentStations adjacentStations)
        {
            var adjacentStationsExist = DataSource.adjacentStationsList.FirstOrDefault(a => a.AdjacentStationsId == adjacentStations.AdjacentStationsId);
            if (adjacentStationsExist != null)
            {
                throw new AdjacentStationsAlreadyExistsException(adjacentStations.AdjacentStationsId);

            }
            DataSource.adjacentStationsList.Add(adjacentStations.Clone());
        }
        public void UpdateAdjacentStations(AdjacentStations adjacentStations)
        {
            AdjacentStations adjacentStationsToUpdate = DataSource.adjacentStationsList.Find(a => a.AdjacentStationsId == adjacentStations.AdjacentStationsId);

            if (adjacentStationsToUpdate == null)
            {
                throw new AdjacentStationsNotFoundException(adjacentStations.AdjacentStationsId);
            }

            if (adjacentStationsToUpdate.IsDeleted)
            {
                throw new AdjacentStationsDeletedException(adjacentStations.AdjacentStationsId, "Cannot update deleted adjacent stations");
            }

            DataSource.adjacentStationsList.Remove(adjacentStationsToUpdate);
            DataSource.adjacentStationsList.Add(adjacentStations.Clone());
        }
        public void UpdateAdjacentStations(AdjacentStations adjacentStations, Action<AdjacentStations> update)
        {
            AdjacentStations adjacentStationsToUpdate = DataSource.adjacentStationsList.Find(a => a.AdjacentStationsId == adjacentStations.AdjacentStationsId);

            if (adjacentStationsToUpdate == null)
            {
                throw new AdjacentStationsNotFoundException(adjacentStations.AdjacentStationsId);
            }

            if (adjacentStationsToUpdate.IsDeleted)
            {
                throw new AdjacentStationsDeletedException(adjacentStations.AdjacentStationsId, "Cannot update deleted adjacent stations");
            }
            update(adjacentStationsToUpdate.Clone());
        } 
        public void DeleteAdjacentStations(int id)
        {
           
        }//לעשות פונקמית מחיקה

        #endregion AdjacentStations 


        #region Bus
        public IEnumerable<Bus> GetAllBusses()
        {            
            var allBuses = DataSource.busesList.Where(bus=>!bus.IsDeleted)
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

            if (busById.IsDeleted)
            {
                throw new BusDeletedException(licenseNumber);
            }

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

            if (busToUpdate.IsDeleted)
            {
                throw new BusDeletedException(bus.LicenseNumber, "Cannot update deleted bus");
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

            if (busToUpdate.IsDeleted)
            {
                throw new BusDeletedException(bus.LicenseNumber, "Cannot update deleted bus");
            }
            update(busToUpdate.Clone());
        } 
        public void DeleteBus(Bus bus)
        {
            // פעולת המחיקה לא באמת מוחקת אלא מעדכנת את האוטובוס

            bus.IsDeleted = true;
            UpdateBus(bus);
        }

        #endregion Bus


        #region BusOnTrip
        public IEnumerable<BusOnTrip> GetAllBusOnTrip()
        {
            var allBusesOnTrip = DataSource.busOnTripsList.Where(busOnTrip => !busOnTrip.IsDeleted)
                                                   .Select(busOnTrip => busOnTrip.Clone());
            return allBusesOnTrip;

        }
        public IEnumerable<BusOnTrip> GetAllBusOnTripBy(Predicate<BusOnTrip> predicate)
        {
            var busOnTripsBy = DataSource.busOnTripsList.Where(busOnTrip => !busOnTrip.IsDeleted && predicate(busOnTrip))
                                                  .Select(busOnTrip => busOnTrip.Clone());
            return busOnTripsBy;
        }
        public BusOnTrip GetBusOnTripById(int busOnTripId)
        {
            var busOnTripById = DataSource.busOnTripsList.Where(busOnTrip => busOnTrip.BusOnTripId == busOnTripId)
                                                  .Select(busOnTrip => busOnTrip.Clone())
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
            var busOnTripExist = DataSource.busOnTripsList.FirstOrDefault(b => b.BusOnTripId == busOnTrip.BusOnTripId);
            if (busOnTripExist != null)
            {
                throw new BusOnTripAlreadyExistsException(busOnTrip.BusOnTripId);

            }
            DataSource.busOnTripsList.Add(busOnTrip.Clone());
        }
        public void UpdateBusOnTrip(BusOnTrip busOnTrip)
        {
            BusOnTrip busOnTripToUpdate = DataSource.busOnTripsList.Find(b => b.BusOnTripId == busOnTrip.BusOnTripId);

            if (busOnTripToUpdate == null)
            {
                throw new BusOnTripNotFoundException(busOnTrip.BusOnTripId);
            }

            if (busOnTripToUpdate.IsDeleted)
            {
                throw new BusOnTripDeletedException(busOnTrip.BusOnTripId, "Cannot update deleted bus On Trip");
            }

            DataSource.busOnTripsList.Remove(busOnTripToUpdate);
            DataSource.busOnTripsList.Add(busOnTrip.Clone());
        }
        public void UpdateBusOnTrip(BusOnTrip busOnTrip, Action<BusOnTrip> update)
        {
            BusOnTrip busOnTripToUpdate = DataSource.busOnTripsList.Find(b => b.BusOnTripId == busOnTrip.BusOnTripId);

            if (busOnTripToUpdate == null)
            {
                throw new BusOnTripNotFoundException(busOnTrip.BusOnTripId);
            }

            if (busOnTripToUpdate.IsDeleted)
            {
                throw new BusOnTripDeletedException(busOnTrip.BusOnTripId, "Cannot update deleted bus on trip");
            }
            update(busOnTripToUpdate.Clone());
        } 
        public void DeleteBusOnTrip(int id){}

        #endregion BusOnTrip


        #region Line
        public IEnumerable<Line> GetAllLine()
        {
            var allLine = DataSource.linesList.Where(line => !line.IsDeleted)
                                                       .Select(line => line.Clone());
            return allLine;

        }
        public IEnumerable<Line> GetAllLineBy(Predicate<Line> predicate)
        {
            var LineBy = DataSource.linesList.Where(line => !line.IsDeleted && predicate(line))
                                                             .Select(line => line.Clone());
            return LineBy;
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

            if (lineById.IsDeleted)
            {
                throw new LineDeletedException(lineId);
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

            if (lineToUpdate.IsDeleted)
            {
                throw new LineDeletedException(line.LineId, "Cannot update deleted line");
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

            if (lineToUpdate.IsDeleted)
            {
                throw new LineDeletedException(line.LineId, "Cannot update deleted line");
            }
            update(lineToUpdate.Clone());
        } 
        public void DeleteLine(int id){}

        #endregion Line


        #region LineStation
        public IEnumerable<LineStation> GetAllLineStation()
        {
            var allLineStations = DataSource.lineStationsList.Where(lineStation => !lineStation.IsDeleted)
                                                           .Select(lineStation => lineStation.Clone());
            return allLineStations;

        }
        public IEnumerable<LineStation> GetAllLineStationStationBy(Predicate<LineStation> predicate)
        {
            var LineStationBy = DataSource.lineStationsList.Where(lineStation => !lineStation.IsDeleted && predicate(lineStation))
                                                                 .Select(lineStation => lineStation.Clone());
            return LineStationBy;
        }
        public LineStation GetLineStationStationById(int lineStationId)
        {
            var LineStationById = DataSource.lineStationsList.Where(lineStation => lineStation.LineStationId == lineStationId)
                                                         .Select(lineStation => lineStation.Clone())
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
            var lineStationExist = DataSource.lineStationsList.FirstOrDefault(l => l.LineStationId == lineStation.LineStationId);
            if (lineStationExist != null)
            {
                throw new LineStationAlreadyExistsException(lineStation.LineStationId);

            }
            DataSource.lineStationsList.Add(lineStation.Clone());
        }
        public void UpdateLineStation(LineStation lineStation)
        {
            LineStation lineStationToUpdate = DataSource.lineStationsList.Find(l => l.LineStationId == lineStation.LineStationId);

            if (lineStationToUpdate == null)
            {
                throw new LineStationNotFoundException(lineStation.LineStationId);
            }

            if (lineStationToUpdate.IsDeleted)
            {
                throw new LineStationDeletedException(lineStation.LineStationId, "Cannot update deleted line station Id");
            }

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

            if (LineStationToUpdate.IsDeleted)
            {
                throw new LineStationDeletedException(lineStation.LineStationId, "Cannot update deleted LineStation");
            }
            update(LineStationToUpdate.Clone());
        }
        public void DeleteLineStationStation(int id){}

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
            var lineTripBy = DataSource.lineTripsList.Where(lineTrip => !lineTrip.IsDeleted && predicate(lineTrip))
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

            if (lineTripById.IsDeleted)
            {
                throw new LineTripDeletedException(lineTripId);
            }

            return lineTripById;
        }
        public void AddLineTrip(LineTrip lineTrip)
        {
            var lineTripExist = DataSource.lineTripsList.FirstOrDefault(l => l.LineTripId == lineTrip.LineTripId);
            if (lineTripExist != null)
            {
                throw new LineTripAlreadyExistsException(lineTrip.LineTripId);

            }
            DataSource.lineTripsList.Add(lineTrip.Clone());
        }
        void UpdateLineTrip(LineTrip lineTrip)
        {
            LineTrip lineTripToUpdate = DataSource.lineTripsList.Find(l => l.LineTripId == lineTrip.LineTripId);

            if (lineTripToUpdate == null)
            {
                throw new LineTripNotFoundException(lineTrip.LineTripId);
            }

            if (lineTripToUpdate.IsDeleted)
            {
                throw new LineTripDeletedException(lineTrip.LineTripId, "Cannot update deleted line trip Id");
            }

            DataSource.lineTripsList.Remove(lineTripToUpdate);
            DataSource.lineTripsList.Add(lineTrip.Clone());
        }
        void UpdateLineTrip(LineTrip lineTrip, Action<LineTrip> update)
        {
            LineTrip lineTripToUpdate = DataSource.lineTripsList.Find(l => l.LineTripId == lineTrip.LineTripId);

            if (lineTripToUpdate == null)
            {
                throw new LineTripNotFoundException(lineTrip.LineTripId);
            }

            if (lineTripToUpdate.IsDeleted)
            {
                throw new LineTripDeletedException(lineTrip.LineTripId, "Cannot update deleted line Trip");
            }
            update(lineTripToUpdate.Clone());
        } 
        void DeleteLineTrip(int id){}

        #endregion LineTrip


        #region Station
        public IEnumerable<Station> GetAllStation()
        {
            var allstations = DataSource.stationsList.Where(station => !station.IsDeleted)
                                                                 .Select(station => station.Clone());
            return allstations;
        }
        public IEnumerable<Station> GetAllStationBy(Predicate<Station> predicate)
        {
            var stationBy = DataSource.stationsList.Where(station => !station.IsDeleted && predicate(station))
                                                                       .Select(station => station.Clone());
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

            if (stationById.IsDeleted)
            {
                throw new StationDeletedException(stationId);
            }

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
        void UpdateStation(Station station)
        {
            Station stationToUpdate = DataSource.stationsList.Find(l => l.StationId == station.StationId);

            if (stationToUpdate == null)
            {
                throw new StationNotFoundException(station.StationId);
            }

            if (stationToUpdate.IsDeleted)
            {
                throw new StationDeletedException(station.StationId, "Cannot update deleted station Id");
            }

            DataSource.stationsList.Remove(stationToUpdate);
            DataSource.stationsList.Add(station.Clone());
        }
        void UpdateStation(Station station, Action<Station> update)
        {
            Station stationToUpdate = DataSource.stationsList.Find(l => l.StationId == station.StationId);

            if (stationToUpdate == null)
            {
                throw new StationNotFoundException(station.StationId);
            }

            if (stationToUpdate.IsDeleted)
            {
                throw new StationDeletedException(station.StationId, "Cannot update deleted station");
            }
            update(stationToUpdate.Clone());
        } 
        void DeleteStation(int id){}

        #endregion Station


        #region User
        public IEnumerable<User> GetAllUser()
        {
            var allUsers = DataSource.usersList.Where(user => !user.IsDeleted)
                                                                   .Select(user => user.Clone());
            return allUsers;
        }
        public IEnumerable<User> GetAllUserBy(Predicate<User> predicate)
        {
            var userBy = DataSource.usersList.Where(user => !user.IsDeleted && predicate(user))
                                                                          .Select(user => user.Clone());
            return userBy;
        }
        public User GetUserById(string userName)
        {

            var userById = DataSource.usersList.Where(user => user.UserName == userName)
                                                              .Select(user => user.Clone())
                                                              .FirstOrDefault();

            if (userById == null)
            {
                throw new UserNotFoundException(userName);
            }

            if (userById.IsDeleted)
            {
                throw new UserDeletedException(userName);
            }

            return userById;
        }
        public void AddUser(User user)
        {
            var userExist = DataSource.usersList.FirstOrDefault(l => l.UserName == user.UserName);
            if (userExist != null)
            {
                throw new UserAlreadyExistsException(user.UserName);

            }
            DataSource.usersList.Add(user.Clone());
        }
        void UpdateUser(User user)
        {
            User userToUpdate = DataSource.usersList.Find(l => l.UserName == user.UserName);

            if (userToUpdate == null)
            {
                throw new UserNotFoundException(user.UserName);
            }

            if (userToUpdate.IsDeleted)
            {
                throw new UserDeletedException(user.UserName, "Cannot update deleted user name");
            }

            DataSource.usersList.Remove(userToUpdate);
            DataSource.usersList.Add(user.Clone());
        }
        void UpdateUser(User user, Action<User> update)
        {
            User userToUpdate = DataSource.usersList.Find(l => l.UserName == user.UserName);

            if (userToUpdate == null)
            {
                throw new UserNotFoundException(user.UserName);
            }

            if (userToUpdate.IsDeleted)
            {
                throw new UserDeletedException(user.UserName, "Cannot update deleted user name");
            }
            update(userToUpdate.Clone());
        }
        void DeleteUser(int id){}

        #endregion User

    }
}
