﻿using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLAPI
{
   // ממשק של שכבת הבי אל
    public interface IBL
    {
        #region Bus
        IEnumerable<Bus> GetAllBusses();
        Bus GetBusById(int id);
        void AddBus(Bus bus);
        void UpdateBus(Bus bus);
        void DeleteBus(int id);

        #endregion Bus

        #region Line
        IEnumerable<Line> GetAllLine();
        Line GetLineById(int id);
        void AddLine(Line line);
        void UpdateLine(Line line);
        void AddLineStationToLine(Line line, StationOfLine stationOfLine, double distanceFromPrevStation, TimeSpan timeFromPrevStation);
        void UpdateLineStations(Line line);
        void UpdateLineTrips(Line line);
        void DeleteLine(int line);
        void MoveLineStationUp(Line line, StationOfLine stationOfLine);
        void MoveLineStationDown(Line line, StationOfLine stationOfLine);
        void DeleteLineStation(Line line, StationOfLine stationOfLine);

        #endregion Line

        #region LineTrip 
        void AddLineTrip(LineTrip lineTrip);
        void DeleteLineTrip(int id, bool isForcedDelete);
        void AddLineTripToLine(Line line, LineTrip lineTrip);
        IEnumerable<LineTrip> GetAllLineTrips();
        #endregion LineTrip

        #region Station
        IEnumerable<Station> GetAllStation();
        Station GetStationById(int id);
        void AddStation(Station station);
        void UpdateStation(Station station);
        void DeleteStation(int id);

        #endregion Station
        //  סימן שאלה ליד טייפ מאפשר לי להחזיר או את הטייפ או נאל  
        TimeSpan?  GetTimeBetweenStations(int stationId1, int stationId2);
        double? GetDistanceBetweenStations(int stationId1, int stationId2);
        List<LineTiming> GetAllCurrentLinesForStation(Station station, TimeSpan timeSpan);
    }
}
