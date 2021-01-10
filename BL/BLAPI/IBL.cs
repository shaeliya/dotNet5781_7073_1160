﻿using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLAPI
{
    public interface IBL
    {

        #region Bus
        IEnumerable<Bus> GetAllBusses();
        Bus GetBusById(int id);
        void AddBus(Bus bus);
        void UpdateBus(Bus bus);
        void DeleteBus(int id);

        #endregion Bus


        #region BusOnTrip
        IEnumerable<BusOnTrip> GetAllBusOnTrip();
        BusOnTrip GetBusOnTripById(int id);
        void AddBusOnTrip(BusOnTrip busOnTrip);
        void UpdateBusOnTrip(BusOnTrip busOnTrip);
        void DeleteBusOnTrip(int id);

        #endregion BusOnTrip


        #region Line
        IEnumerable<Line> GetAllLine();
        Line GetLineById(int id);
        void AddLine(Line line);
        void UpdateLine(Line line);
        void UpdateLineStations(Line line);
        void UpdateLineTrips(Line line);
        void DeleteLine(int line);

        #endregion Line


        #region Station
        IEnumerable<Station> GetAllStation();
        Station GetStationById(int id);
        void AddStation(Station station);
        void UpdateStation(Station station);
        void DeleteStation(int id);

        #endregion Station
    

    }
}
