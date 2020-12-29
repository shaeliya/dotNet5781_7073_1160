using DalApi;
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

        }
        public IEnumerable<AdjacentStations> GetAllAdjacentStationsBy(Predicate<AdjacentStations> predicate){}
        public AdjacentStations GetAdjacentStationsById(int id){}
        public void AddAdjacentStations(AdjacentStations adjacentStations)
        {
            adjacentStations.AdjacentStationsId = ++Configuration.MaxAdjacentStationsId;
            DataSource.adjacentStationsList.Add(adjacentStations.Clone());
        }
        public void UpdateAdjacentStations(AdjacentStations adjacentStations){}
        public void UpdateAdjacentStations(int id, Action<AdjacentStations> update){} //method that knows to update specific fields in Bus
        public void DeleteAdjacentStations(int id){}

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
            DataSource.busesList.Add(bus);
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
            update(busToUpdate);
        } 
        public void DeleteBus(Bus bus)
        {
            // פעולת המחיקה לא באמת מוחקת אלא מעדכנת את האוטובוס

            bus.IsDeleted = true;
            UpdateBus(bus);
        }

        #endregion Bus


        #region BusOnTrip
        public IEnumerable<BusOnTrip> GetAllBusOnTrip(){}
        public IEnumerable<BusOnTrip> GetAllBusOnTripBy(Predicate<BusOnTrip> predicate){}
        public BusOnTrip GetBusOnTripById(int id){}
        public void AddBusOnTrip(BusOnTrip busOnTrip){}
        public void UpdateBusOnTrip(BusOnTrip busOnTrip){}
        public void UpdateBusOnTrip(int id, Action<BusOnTrip> update){} //method that knows to update specific fields in Bus
        public void DeleteBusOnTrip(int id){}

        #endregion BusOnTrip


        #region Line
        public IEnumerable<Line> GetAllLine(){}
        public IEnumerable<Line> GetAllLineBy(Predicate<Line> predicate){}
        public Line GetLineById(int id){}
        public void AddLine(Line line){}
        public void UpdateLine(Line line){}
        public void UpdateLine(int id, Action<Line> update){} //method that knows to update specific fields in Bus
        public void DeleteLine(int id){}

        #endregion Line


        #region LineStation
        public IEnumerable<LineStation> GetAllLineStation(){}
        public IEnumerable<LineStation> GetAllLineStationBy(Predicate<LineStation> predicate){}
        public LineStation GetLineStationById(int id){}
        public void AddLineStation(LineStation lineStation){}
        public void UpdateLineStation(LineStation lineStation){}
        public void UpdateLineStation(int id, Action<LineStation> update){} //method that knows to update specific fields in Bus
        public void DeleteLineStation(int id){}

        #endregion LineStation


        #region LineTrip
        public IEnumerable<LineTrip> GetAllLineTrip(){}
        public IEnumerable<LineTrip> GetAllLineTripBy(Predicate<LineTrip> predicate){}
        public LineTrip GetLineTripById(int id){}
        public void AddLineTrip(LineTrip lineTrip){}
        void UpdateLineTrip(LineTrip lineTrip){}
        void UpdateLineTrip(int id, Action<LineTrip> update){} //method that knows to update specific fields in Bus
        void DeleteLineTrip(int id){}

        #endregion LineTrip


        #region Station
        public IEnumerable<Station> GetAllStation(){}
        public IEnumerable<Station> GetAllStationBy(Predicate<Station> predicate){}
        public Station GetStationById(int id){}
        public void AddStation(Station station){}
        void UpdateStation(Station station){}
        void UpdateStation(int id, Action<Station> update){} //method that knows to update specific fields in Bus
        void DeleteStation(int id){}

        #endregion Station


        #region User
        public IEnumerable<User> GetAllUser(){}
        public IEnumerable<User> GetAllUserBy(Predicate<User> predicate){}
        public User GetUserById(int id){}
        public void AddUser(User user){}
        void UpdateUser(User user){}
        void UpdateUser(int id, Action<User> update){} //method that knows to update specific fields in Bus
        void DeleteUser(int id){}

        #endregion User

    }
}
