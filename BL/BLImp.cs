using BLAPI;
using BO;
using BO.Exceptions;
using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
 
    public class BLImp : IBL
    {
        IDal dl = DLFactory.GetDL();

        #region Bus
        public IEnumerable<Bus> GetAllBusses()
        {
            try
            {
                var allBusses = dl.GetAllBusses();
                var allBussesDo = allBusses.Select(b => BusDoBoAdapter(b)).ToList();
                return allBussesDo;
            }
            catch (DO.Exceptions.BusNotFoundException exDO)
            {
                throw new BusNotFoundException(0, "No Busses found in system", exDO);
            }
            catch (GeneralException ex)

            {

                throw new GeneralException(MethodBase.GetCurrentMethod().Name, "General Error", ex);
            }
        }
        Bus BusDoBoAdapter(DO.Bus busDO)
        {
            Bus busBO = new Bus();

            // BO-לישות ב DO-הדומים מהישות ב Properties-נעתיק את כל ה
            busDO.CopyPropertiesTo(busBO);
            return busBO;
        }

        public Bus GetBusById(int licenseNumber)
        {
            try
            {
                Bus busBo = BusDoBoAdapter(dl.GetBusById(licenseNumber));

                return busBo;
            }
            catch (DO.Exceptions.BusNotFoundException exDO)
            {
                throw new BusNotFoundException(licenseNumber, exDO.Message, exDO);
            }
            catch (DO.Exceptions.BusDeletedException exDO)
            {
                throw new BusDeletedException(licenseNumber, exDO.Message, exDO);
            }
            catch (GeneralException ex)

            {

                throw new GeneralException(MethodBase.GetCurrentMethod().Name, "General Error", ex);
            }
        }

        public void AddBus(Bus bus)
        {
            DO.Bus busDO = new DO.Bus();

            // 1. נוסיף את הקו עצמו
            bus.CopyPropertiesTo(busDO);
            dl.AddBus(busDO);

        }
        public void UpdateBus(Bus bus)
        {
            DO.Bus busDO = new DO.Bus();
            bus.CopyPropertiesTo(busDO);
            dl.UpdateBus(busDO);
        }
        public void DeleteBus(int licenseNumber)
        {
            dl.DeleteBus(licenseNumber);
        }

        #endregion Bus


        #region BusOnTrip
        public IEnumerable<BusOnTrip> GetAllBusOnTrip()
        {
            var allBusesOnTrip = DataSource.busOnTripsList.Where(busOnTrip => !busOnTrip.IsDeleted)
                                                   .Select(busOnTrip => busOnTrip.Clone());
            return allBusesOnTrip;

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
        public void DeleteBusOnTrip(int id)
        {
            dl.DeleteBusOnTrip(id);
        }

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

            lineBO.StationsList = lineStations.Select(ls => CreateStationOfLine(ls));


            // 3. של הקו LineTrip-נמצא את כל ה
            var lineTrip = dl.GetAllLineTripBy(lt => lt.LineId == lineDO.LineId);

            // 4. המתאים לו LineTrip-נביא עבור הקו שלנו את ה 
            lineBO.LineTripList = lineTrip.Select(s => (LineTrip)s.CopyPropertiesToNew(typeof(LineTrip)));

            return lineBO;
        }

        private StationOfLine CreateStationOfLine(DO.LineStation lineStation)
        {

            // DO-מביאים את התחנה מה            
            DO.Station station = dl.GetStationById(lineStation.StationId);

            StationOfLine sol = (StationOfLine)station.CopyPropertiesToNewAndUnion(typeof(StationOfLine), lineStation);

            var nextLineStation = dl.GetAllLineStationBy(ls => ls.LineId == lineStation.LineId &&
                                                               ls.LineStationIndex == lineStation.LineStationIndex + 1).FirstOrDefault();
            var currAndNextStation = dl.GetAllAdjacentStationsBy(ajs => ajs.StationId1 == lineStation.StationId &&
                                                                        ajs.StationId2 == nextLineStation.StationId ||
                                                                        ajs.StationId2 == lineStation.StationId &&
                                                                        ajs.StationId1 == nextLineStation.StationId).FirstOrDefault();
            sol.TimeToNextStation = currAndNextStation.Time;
            sol.DistanceToNextStation = currAndNextStation.Distance;

            return sol;

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
            catch (GeneralException ex)

            {

                throw new GeneralException(MethodBase.GetCurrentMethod().Name, "General Error", ex);
            }
        }

        public Line GetLineById(int lineId)
        {
            try
            {
                Line lineBo = LineDoBoAdapter(dl.GetLineById(lineId));
                return lineBo;
            }

            catch (DO.Exceptions.LineNotFoundException exDO)
            {
                throw new LineNotFoundException(0, "No Lines found in system", exDO);
            }
            catch (DO.Exceptions.LineDeletedException exDO)
            {
                throw new LineDeletedException(lineId, exDO.Message, exDO);
            }
            catch (GeneralException ex)

            {

                throw new GeneralException(MethodBase.GetCurrentMethod().Name, "General Error", ex);
            }
        }
            public void AddLine(Line line)
        {
            DO.Line lineDO = new DO.Line();

            // 1. נוסיף את הקו עצמו
            line.CopyPropertiesTo(lineDO);

            dl.AddLine(lineDO);
            AddStationsFromLine(line);            
            line.LineTripList.ToList().ForEach(lt => AddLineTripFromLine(lt));
        }

        private void AddStationsFromLine(Line line)
        {
            // 2. נוסיף את התחנות שלו
            line.StationsList.ToList().ForEach(s => AddLineStation(s, line));

            // 3. נוסיף זוגות של תחנות צמודות
            var stationsOrderedList = line.StationsList.OrderBy(o => o.LineStationIndex);
            foreach (var (x, y) in Utilities.Pairwise(stationsOrderedList))
            {
                /*
                 1.	להוסיף לסטיישן אוף ליין מרחק לתחנה הבאה או תחנה קודמת, וכל מידע אחר שניצטרך בשביל התחנות עוקבות
                 
                 */
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
                catch (GeneralException ex)

                {

                    throw new GeneralException(MethodBase.GetCurrentMethod().Name, "General Error", ex);
                }
            }
        }


        private void AddLineTripFromLine(LineTrip lt)
        {
            DO.LineTrip lineTripDO = new DO.LineTrip();

            lt.CopyPropertiesTo(lineTripDO);

            dl.AddLineTrip(lineTripDO);
        }

        private void AddLineStation(StationOfLine stationOfLine, Line line)
        {
            DO.LineStation ls = (DO.LineStation)stationOfLine.CopyPropertiesToNewAndUnion(typeof(DO.LineStation), line);

            dl.AddLineStation(ls);

        }

        /// <summary>
        /// יעשה בפונקציה נפרדת IEnumerable-עדכון שדות הקו בלבד - עדכון ה
        /// </summary>
        /// <param name="line"></param>
        public void UpdateLine(Line line)
        {
            DO.Line lineDO = new DO.Line();
            line.CopyPropertiesTo(lineDO);
            dl.UpdateLine(lineDO);
        }

        /// <summary>
        /// עדכון תחנות הקו
        /// </summary>
        /// <param name="line"></param>
        public void UpdateLineStations(Line line)
        {
            line.StationsList.ToList().ForEach(s => DeleteLineStation(s, line));
            AddStationsFromLine(line);
        }

        private void DeleteLineStation(StationOfLine stationOfLine, Line line)
        {
            DO.LineStation ls = (DO.LineStation)stationOfLine.CopyPropertiesToNewAndUnion(typeof(DO.LineStation), line);
            dl.DeleteLineStation(ls.LineStationId);
        }

        /// <summary>
        /// עדכון יציאות הקו
        /// </summary>
        /// <param name="line"></param>
        public void UpdateLineTrips(Line line)
        {
            line.LineTripList.ToList().ForEach(lt => DeleteLineTrips(lt, line));
            AddStationsFromLine(line);
        }

        private void DeleteLineTrips(LineTrip lineTrip, Line line)
        {
            DO.LineTrip lt = new DO.LineTrip();
            lineTrip.CopyPropertiesTo(lt);
            dl.DeleteLineTrip(lt.LineTripId);
        }
        public void DeleteLine(int lineId) 
        {
            dl.DeleteLine(lineId);
        }

        #endregion Line


        #region Station
       

        Station StationDoBoAdapter(DO.Station stationDO)
        {
            Station stationBO = new Station();

            // BO-לישות ב DO-הדומים מהישות ב Properties-נעתיק את כל ה
            stationDO.CopyPropertiesTo(stationBO);

            // נטפל ברשימה של הקווים הנמצאים בתוך התחנה

            // 1. נמצא את כל הקווים בתחנה
          
            var stationLines = dl.GetAllLineStationBy(sl => sl.StationId == stationDO.StationId);

            // 2. LineOfStation לאובייקט של StationLine + Line נמיר את האובייקט של 

            stationBO.LinesList = from sl in stationLines
                                  let line = dl.GetLineById(sl.LineId) // DO-מביאים את הקו מה
                                  select (LineOfStation)line.CopyPropertiesToNewAndUnion(typeof(LineOfStation), sl);

            return stationBO;
        }
      

        public IEnumerable<Station> GetAllStation()
        {
            try
            {
                var allStation = dl.GetAllStation();
                var allStationDo = allStation.Select(s => StationDoBoAdapter(s)).ToList();
                return allStationDo;
            }
            catch (DO.Exceptions.StationNotFoundException exDO)
            {
                throw new LineNotFoundException(0, "No Stations found in system", exDO);
            }
            catch (GeneralException ex)

            {

                throw new GeneralException(MethodBase.GetCurrentMethod().Name, "General Error", ex);
            }
        }
      
        public Station GetStationById(int stationId)
        {
            try
            {
                Station stationBo  = StationDoBoAdapter(dl.GetStationById(stationId));

                return stationBo;

            }

            catch (DO.Exceptions.StationNotFoundException exDO)
            {
                throw new StationNotFoundException(0, "No Stations found in system", exDO);
            }
            catch (DO.Exceptions.StationDeletedException exDO)
            {
                throw new StationDeletedException(stationId, exDO.Message, exDO);
            }
            catch (GeneralException ex)

            {
                throw new GeneralException(MethodBase.GetCurrentMethod().Name, "General Error", ex);
            }
        }
       
        public void AddStation(Station station)
        {
            DO.Station stationDO = new DO.Station();

            // 1.נוסיף את התחנה עצמה
            station.CopyPropertiesTo(stationDO);

            dl.AddStation(stationDO);
          
        }

        public void UpdateStation(Station station)
        {
            DO.Station stationDO = new DO.Station();
            station.CopyPropertiesTo(stationDO);
            dl.UpdateStation(stationDO);
        }
        public void DeleteStation(int stationId)
        {
            dl.DeleteStation(stationId);
        }

        #endregion Station    

    }
}
