using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{

    //CRUD Logic:
    // Create - add new instance
    // Request - ask for an instance or for a collection
    // Update - update properties of an instance
    // Delete - delete an instance
    public interface IDal
    {

        #region AdjacentStations
        IEnumerable<AdjacentStations> GetAllAdjacentStations();
        IEnumerable<AdjacentStations> GetAllAdjacentStationsBy(Predicate<AdjacentStations> predicate);
        AdjacentStations GetAdjacentStationsById(int id);
        void AddAdjacentStations(AdjacentStations adjacentStations);
        void UpdateAdjacentStations(AdjacentStations adjacentStations);
        void UpdateAdjacentStations(AdjacentStations id, Action<AdjacentStations> update); //method that knows to update specific fields in Bus
        void DeleteAdjacentStations(AdjacentStations id);

        #endregion AdjacentStations


        #region Bus
        IEnumerable<Bus> GetAllBusses();
        IEnumerable<Bus> GetAllBussesBy(Predicate<Bus> predicate);
        Bus GetBusById(int id);
        void AddBus(Bus bus);
        void UpdateBus(Bus bus);
        void UpdateBus(Bus id, Action<Bus> update); //method that knows to update specific fields in Bus
        void DeleteBus(Bus id);

        #endregion Bus


        #region BusOnTrip
        IEnumerable<BusOnTrip> GetAllBusOnTrip();
        IEnumerable<BusOnTrip> GetAllBusOnTripBy(Predicate<BusOnTrip> predicate);
        BusOnTrip GetBusOnTripById(int id);
        void AddBusOnTrip(BusOnTrip busOnTrip);
        void UpdateBusOnTrip(BusOnTrip busOnTrip);
        void UpdateBusOnTrip(BusOnTrip id, Action<BusOnTrip> update); //method that knows to update specific fields in Bus
        void DeleteBusOnTrip(BusOnTrip id);

        #endregion BusOnTrip


        #region Line
        IEnumerable<Line> GetAllLine();
        IEnumerable<Line> GetAllLineBy(Predicate<Line> predicate);
        Line GetLineById(int id);
        void AddLine(Line line);
        void UpdateLine(Line line);
        void UpdateLine(Line id, Action<Line> update); //method that knows to update specific fields in Bus
        void DeleteLine(Line id);

        #endregion Line


        #region LineStation
        IEnumerable<LineStation> GetAllLineStation();
        IEnumerable<LineStation> GetAllLineStationBy(Predicate<LineStation> predicate);
        LineStation GetLineStationById(int id);
        void AddLineStation(LineStation lineStation);
        void UpdateLineStation(LineStation lineStation);
        void UpdateLineStation(LineStation id, Action<LineStation> update); //method that knows to update specific fields in Bus
        void DeleteLineStation(LineStation id);

        #endregion LineStation


        #region LineTrip
        IEnumerable<LineTrip> GetAllLineTrip();
        IEnumerable<LineTrip> GetAllLineTripBy(Predicate<LineTrip> predicate);
        LineTrip GetLineTripById(int id);
        void AddLineTrip(LineTrip lineTrip);
        void UpdateLineTrip(LineTrip lineTrip);
        void UpdateLineTrip(LineTrip id, Action<LineTrip> update); //method that knows to update specific fields in Bus
        void DeleteLineTrip(LineTrip id);

        #endregion LineTrip


        #region Station
        IEnumerable<Station> GetAllStation();
        IEnumerable<Station> GetAllStationBy(Predicate<Station> predicate);
        Station GetStationById(int id);
        void AddStation(Station station);
        void UpdateStation(Station station);
        void UpdateStation(Station id, Action<Station> update); //method that knows to update specific fields in Bus
        void DeleteStation(Station id);

        #endregion Station


        #region User
        IEnumerable<User> GetAllUser();
        IEnumerable<User> GetAllUserBy(Predicate<User> predicate);
        User GetUserById(string id);
        void AddUser(User user);
        void UpdateUser(User user);
        void UpdateUser(User id, Action<User> update); //method that knows to update specific fields in Bus
        void DeleteUser(User id);

        #endregion User


    }
}
