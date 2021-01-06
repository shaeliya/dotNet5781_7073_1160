using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLAPI
{
    public interface IBL
    {
        #region AdjacentStations
        IEnumerable<AdjacentStations> GetAllAdjacentStations();
        AdjacentStations GetAdjacentStationsById(int id);
        void AddAdjacentStations(AdjacentStations adjacentStations);
        void UpdateAdjacentStations(AdjacentStations adjacentStations);
        void DeleteAdjacentStations(AdjacentStations id);

        #endregion AdjacentStations


        #region Bus
        IEnumerable<Bus> GetAllBusses();
        Bus GetBusById(int id);
        void AddBus(Bus bus);
        void UpdateBus(Bus bus);
        void DeleteBus(Bus id);

        #endregion Bus


        #region BusOnTrip
        IEnumerable<BusOnTrip> GetAllBusOnTrip();
        BusOnTrip GetBusOnTripById(int id);
        void AddBusOnTrip(BusOnTrip busOnTrip);
        void UpdateBusOnTrip(BusOnTrip busOnTrip);
        void DeleteBusOnTrip(BusOnTrip id);

        #endregion BusOnTrip


        #region Line
        IEnumerable<Line> GetAllLine();
        Line GetLineById(int id);
        void AddLine(Line line);
        void UpdateLine(Line line);
        void DeleteLine(Line line);

        #endregion Line



        #region LineTrip
        IEnumerable<LineTrip> GetAllLineTrip();
        LineTrip GetLineTripById(int id);
        void AddLineTrip(LineTrip lineTrip);
        void UpdateLineTrip(LineTrip lineTrip);
        void DeleteLineTrip(LineTrip id);

        #endregion LineTrip


        #region Station
        IEnumerable<Station> GetAllStation();
        Station GetStationById(int id);
        void AddStation(Station station);
        void UpdateStation(Station station);
        void DeleteStation(Station id);

        #endregion Station


        #region User
        IEnumerable<User> GetAllUser();
        User GetUserById(string id);
        void AddUser(User user);
        void UpdateUser(User user);
        void DeleteUser(User id);

        #endregion User


    }
}
