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

            lineBO.StationsList = lineStations.Select(ls => CreateStationOfLine(ls)).OrderBy(o=>o.LineStationIndex);


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

            var nextLineStation = dl.GetAllLineStationBy(ls => ls.LineId == lineStation.LineId &&ls.LineStationIndex == lineStation.LineStationIndex + 1).FirstOrDefault();
            if (nextLineStation == null)
            {
                sol.TimeToNextStation = TimeSpan.Zero;
                sol.DistanceToNextStation = 0;

            }
            else
            {
                var currAndNextStation = dl.GetAllAdjacentStationsBy(ajs => (ajs.StationId1 == lineStation.StationId &&
                                                                            ajs.StationId2 == nextLineStation.StationId) ||
                                                                            (ajs.StationId2 == lineStation.StationId &&
                                                                            ajs.StationId1 == nextLineStation.StationId)).FirstOrDefault();
                sol.TimeToNextStation = currAndNextStation.Time;
                sol.DistanceToNextStation = currAndNextStation.Distance;
            }
            return sol;

        }

        public IEnumerable<Line> GetAllLine()
        {
            try
            {
                var allLine = dl.GetAllLine();
                var allLineDo = allLine.Select(l => LineDoBoAdapter(l)).ToList();
                return allLineDo.OrderBy(l => l.LineNumber).ToList();
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
            //line.LineTripList.ToList().ForEach(lt => AddLineTripFromLine(lt));
        }

        public void MoveLineStationUp(Line line, StationOfLine stationOfLine)
        {
            int currIndex = stationOfLine.LineStationIndex;
            int prevIndex = stationOfLine.LineStationIndex - 1;


            if (prevIndex != 0)
            {
                // נבדוק שהאינדקס באמת קיים - כי אולי מחקנו אותו
                var prevLineStation = dl.GetAllLineStationBy(ls => ls.LineId == line.LineId && ls.LineStationIndex == prevIndex).FirstOrDefault();
                if(prevLineStation!=null)
                {
                    var currStation = line.StationsList.Where(ls => ls.LineStationIndex == currIndex).FirstOrDefault();
                    var prevStation = line.StationsList.Where(ls => ls.LineStationIndex == prevIndex).FirstOrDefault();
                    currStation.LineStationIndex = prevIndex;
                    prevStation.LineStationIndex = currIndex;

                    UpdateLineStations(line);

                }
            }

        }
        public void MoveLineStationDown(Line line, StationOfLine stationOfLine)
        {
            int currIndex = stationOfLine.LineStationIndex;
            int nextIndex = stationOfLine.LineStationIndex + 1;
            //נמצא את האינדקס האחרון ברשימה

            var lastLineStation = dl.GetAllLineStationBy(ls => ls.LineId == line.LineId).OrderByDescending(o => o.LineStationIndex).FirstOrDefault();
            int lastIndex = lastLineStation.LineStationIndex;

            // נבדוק שהאינדקס באמת קיים - כי אולי אנחנו כבר בסוף
            if (currIndex != lastIndex)
            {
                var nextLineStation = dl.GetAllLineStationBy(ls => ls.LineId == line.LineId && ls.LineStationIndex == nextIndex).FirstOrDefault();

                if(nextLineStation!=null)
                {
                    var currStation = line.StationsList.Where(ls => ls.LineStationIndex == currIndex).FirstOrDefault();
                    var nextStation = line.StationsList.Where(ls => ls.LineStationIndex == nextIndex).FirstOrDefault();
                    currStation.LineStationIndex = nextIndex;
                    nextStation.LineStationIndex = currIndex;

                    UpdateLineStations(line);

                }
            }

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
                aj.Distance = x.DistanceToNextStation;
                aj.Time = x.TimeToNextStation.Duration();

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
        public void AddLineStationToLine(Line line, StationOfLine stationOfLine)
        {
            line.StationsList = line.StationsList.Where(sol => sol.LineStationIndex >= stationOfLine.LineStationIndex).
                Select(sol =>
                {
                    sol.LineStationIndex += 1;
                    return sol;
                });



            line.StationsList = line.StationsList.Append(stationOfLine);
            UpdateLineStations(line);

        }

        /// <summary>
        /// עדכון תחנות הקו
        /// </summary>
        /// <param name="line"></param>
        public void UpdateLineStations(Line line)
        {
          var lineStationsForLine = dl.GetAllLineStationBy(ls => ls.LineId == line.LineId).ToList();
            lineStationsForLine.ToList().ForEach(ls => dl.DeleteLineStation(ls.LineStationId, true));
            AddStationsFromLine(line);

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
                return allStationDo.OrderBy(s => s.StationId);
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


        public List<LineTiming> GetAllCurrentLinesForStation(Station station)
        {
            List<LineTiming> lineTimingList = new List<LineTiming>();

            // 1. נמצא את כל תחנות-הקווים העוברים בתחנה
            var allLineStations = dl.GetAllLineStationBy(ls => !ls.IsDeleted &&
                                                              ls.StationId == station.StationId);

            if (allLineStations != null)
            {
                // 2. נמצא את הקווים עצמם
                var allLines = allLineStations.Select(ls => LineDoBoAdapter(dl.GetLineById(ls.LineId)));

                // 3. שמגיעים לתחנה שלנו בתוך חצי שעה LineTrip-נמצא את כל ה

                List<LineTiming> allLineTimingsWithin30Minutes = FindAllLineTimingsWithin30Minutes(station, allLines);
                var lineTrip = allLineTimingsWithin30Minutes.FirstOrDefault();
            }

            return lineTimingList;
        }

        /// <summary>
        /// שמגיעים לתחנה שלנו בתוך חצי שעה LineTrip-פונקציה שמחזירה את כל ה
        /// LineTiming-הפונקציה  ממירה אותם ל
        /// </summary>
        /// <param name="station"></param>
        /// <param name="allLines"></param>
        /// <returns></returns>
        private List<LineTiming> FindAllLineTimingsWithin30Minutes(Station station, IEnumerable<Line> allLines)
        {
            List<LineTiming> allLineTripsWithin30Minutes = new List<LineTiming>();

            allLines.ToList().ForEach(line =>
            {
                // של הקו LineTrip-נמצא את כל ה
                var allLineTrips = dl.GetAllLineTripBy(lt => lt.LineId == line.LineId);
                allLineTrips.ToList().ForEach(lt =>
                {
                    double lineTimeFromStartToStation;
                    bool isLineTripInStationWithin30Minutes = CheckIfLineTripInStationWithin30Minutes(lt, line, station, out lineTimeFromStartToStation);
                    if (isLineTripInStationWithin30Minutes)
                    {
                        LineTiming lineTiming = new LineTiming()
                        {

                            LineId = line.LineId,
                            LineNumber = line.LineNumber,
                            LastStation = line.StationsList.OrderByDescending(s => s.LineStationIndex).FirstOrDefault().Name,
                            TripStart = lt.StartAt,
                            ExpectedTimeTillArrive = DateTime.Today.TimeOfDay + TimeSpan.FromMinutes(lineTimeFromStartToStation)
                        };
                        allLineTripsWithin30Minutes.Add(lineTiming);
                    }
                });                
            });

            return allLineTripsWithin30Minutes;
        }

        /// <summary>
        /// true הפונקציה מחזירה 
        /// אם הקו יגיע לתחנה תוך 30 דקות מעכשיו
        /// </summary>
        /// <param name="lineTrip"></param>
        /// <param name="line"></param>
        /// <param name="station"></param>
        /// <returns></returns>
        private bool CheckIfLineTripInStationWithin30Minutes(DO.LineTrip lineTrip, Line line, Station station, out double lineTimeFromStartToStation)
        {
            lineTimeFromStartToStation = GetLineTimeFromStartToStation(line, station);
            TimeSpan timeToStation = lineTrip.StartAt + TimeSpan.FromMinutes(lineTimeFromStartToStation);
            TimeSpan nowPlus30Minutes = DateTime.Now.TimeOfDay + TimeSpan.FromMinutes(30);

            if(timeToStation >= DateTime.Now.TimeOfDay &&
               timeToStation <= nowPlus30Minutes)
            {
                return true;
            }

            return false;
        }



        /// <summary>
        /// פונקציה שמחשבת כמה זמן לוקח לקו להגיע מתחנת ההתחלה לתחנה רצויה
        /// </summary>
        /// <param name="line"></param>
        /// <param name="station"></param>
        /// <returns></returns>
        private double GetLineTimeFromStartToStation(Line line, Station station)
        {
            var currentStationInLine = line.StationsList.Where(s => s.StationId == station.StationId).FirstOrDefault();
            double timeUntilCuurentStation = line.StationsList.OrderBy(s => s.LineStationIndex).
                                                               Where(s => s.LineStationIndex < currentStationInLine.LineStationIndex).
                                                               Sum(s => s.TimeToNextStation.TotalMinutes);
            return timeUntilCuurentStation;

        }

    }
}
