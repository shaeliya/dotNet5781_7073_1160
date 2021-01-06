using BLAPI;
using BO;
using BO.Exceptions;
using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{

    public class BLImp :IBL
    {
        IDal dl = DLFactory.GetDL();


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
        
        public void DeleteAdjacentStations(int adjacentStationsId)
        {
            // לא ניתן למשתמש למחוק זוג תחנות עוקבות
            // מחיקת זוג תחנות תעשה רק במקרה של מחיקת תחנה
            // לכן אין צורך לטפל במחיקת קשרים שיש לישות 

            var adjacentStationsToDelete = DataSource.adjacentStationsList.Find(adjacentStations => !adjacentStations.IsDeleted &&
                                                                                                     adjacentStations.AdjacentStationsId == adjacentStationsId);

            if (adjacentStationsToDelete == null)
            {
                throw new AdjacentStationsNotFoundException(adjacentStationsId, $"Cannot delete adjacent Stations Id: {adjacentStationsId} because it was not found");
            }


            adjacentStationsToDelete.IsDeleted = true;

        }

        public void DeleteAdjacentStationsBy(Predicate<AdjacentStations> predicate)
        {
            int deletedAdjacentStations = DataSource.adjacentStationsList.RemoveAll(predicate);
            if (deletedAdjacentStations == 0)
            {
                throw new AdjacentStationsNotFoundException(0, $"Cannot delete adjacent Stations For requested predicate: {predicate}");
            }
        }

        #endregion AdjacentStations 


        #region Bus
        public IEnumerable<Bus> GetAllBusses()
        {
            var allBuses = DataSource.busesList.Where(bus => !bus.IsDeleted)
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
        public void DeleteBusOnTrip(int id) { }

        #endregion BusOnTrip


        #region Line
        Line LineDoBoAdapter(DO.Line lineDO)
        {
            Line lineBO = new Line();

            // BO-לישות ב DO-הדומים מהישות ב Properties-נעתיק את כל ה
            lineDO.CopyPropertiesTo(lineBO);
            
            // נטפל ברשימה של התחנות הנמצאת בתוך הקו

            // 1. נמצא את כל תחנות הקו
            var lineStations = dl.GetAllLineStationBy(ls => ls.LineId == lineDO.LineId);
            

            // 2. StationOfLine לאובייקט של LineStation + Station נמיר את האובייקט של 

            lineBO.StationsList = from ls in lineStations
                                  let station = dl.GetStationById(ls.StationId) // DO-מביאים את התחנה מה
                                  select (StationOfLine)station.CopyPropertiesToNewAndUnion(typeof(StationOfLine), ls);

            return lineBO;
        }

        public IEnumerable<Line> GetAllLine()
        {
            try
            {
                var allLine = dl.GetAllLine();
                var allLineDo = allLine.Select(l => LineDoBoAdapter(l)).ToList();
                return allLineDo;
            }
            catch (DO.Exceptions.LineNotFoundException exDO)
            {
                throw new LineNotFoundException(0, "No Lines found in system", exDO);
            }

        }

        public Line GetLineById(int lineId)
        {
            try
            {
                var lineById = dl.GetLineById(lineId);

                if (lineById == null)
                {
                    throw new LineNotFoundException(lineId);
                }

                return LineDoBoAdapter(lineById);
            }

            catch (DO.Exceptions.LineNotFoundException exDO)
            {
                throw new LineNotFoundException(0, "No Lines found in system", exDO);
            }
        }
        public void AddLine(Line line)
        {
            DO.Line lineBO = new DO.Line();

            // 1. נוסיף את הקו עצמו
            line.CopyPropertiesTo(lineBO);

            dl.AddLine(lineBO);
            AddStationsFromLine(line);

        }

        private void AddStationsFromLine(Line line)
        {
            // 2. נוסיף את התחנות שלו
            line.StationsList.ToList().ForEach(s => AddLineStation(s, line));

            // 3. נוסיף זוגות של תחנות צמודות
            var stationsOrderedList = line.StationsList.OrderBy(o => o.LineStationIndex);
            foreach (var (x, y) in Utilities.Pairwise(stationsOrderedList))
            {
                DO.AdjacentStations aj = new DO.AdjacentStations();
                aj.StationId1 = x.StationId;
                aj.StationId2 = y.StationId;

                try
                {
                    dl.AddAdjacentStations(aj);
                }
                catch (DO.Exceptions.AdjacentStationsAlreadyExistsException exDo)
                {
                    // לא נעשה כלום, כי אם כבר קיים זה לא משנה לנו ונמשיך הלאה
                }

            }
        }

        private void AddLineStation(StationOfLine stationOfLine, Line line)
        {
            DO.LineStation ls = (DO.LineStation)stationOfLine.CopyPropertiesToNewAndUnion(typeof(DO.LineStation), line);

            dl.AddLineStation(ls);

        }

        /// <summary>
        /// עדכון שדות הקו בלבד - עדכון התחנות יעשה בפונקציה נפרדת
        /// </summary>
        /// <param name="line"></param>
        public void UpdateLine(Line line)
        {
            DO.Line lineBO = new DO.Line();
            line.CopyPropertiesTo(lineBO);
            dl.UpdateLine(lineBO);
        }

        /// <summary>
        /// עדכון תחנות הקו
        /// </summary>
        /// <param name="line"></param>
        private void UpdateLinesStation(Line line)
        {
            line.StationsList.ToList().ForEach(s => DeleteLineStation(s, line));
            AddStationsFromLine(line);
        }

        private void DeleteLineStation(StationOfLine stationOfLine, Line line)
        {
            DO.LineStation ls = (DO.LineStation)stationOfLine.CopyPropertiesToNewAndUnion(typeof(DO.LineStation), line);
            dl.DeleteLineStation(ls);
        }
        public void DeleteLine(Line line) 
        {
            DO.Line lineBO = new DO.Line();
            line.CopyPropertiesTo(lineBO);
            dl.DeleteLine(lineBO);
        }

        #endregion Line




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
        void DeleteLineTrip(int id) { }

        #endregion LineTrip


        #region Station

        Station StationDoBoAdapter(DO.Station stationDO)
        {
            Station stationBO = new Station();

            stationDO.CopyPropertiesTo(stationBO);
            var lineStations = dl.GetAllLineStationBy(ls => ls.StationId == stationDO.StationId).ToList();

            var lineIdList = from ls in lineStations
                                let lineId = ls.LineId
                                select dl.GetStationById(lineId);


            stationBO.LinesList = lineIdList.Select()

            return stationBO;
        }
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
        void DeleteStation(int id) { }

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
        void DeleteUser(int id) { }

        #endregion User


    }
}
