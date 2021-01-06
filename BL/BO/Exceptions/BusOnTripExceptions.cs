using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO.Exceptions
{
    
    
        public class BusOnTripAlreadyExistsException : Exception
        {
            public int BusOnTripId;
            public BusOnTripAlreadyExistsException(int busOnTripId) : base() => BusOnTripId = busOnTripId;
            public BusOnTripAlreadyExistsException(int busOnTripId, string message) :
                base(message) => BusOnTripId = busOnTripId;
            public BusOnTripAlreadyExistsException(int busOnTripId, string message, Exception innerException) :
                base(message, innerException) => BusOnTripId = busOnTripId;

            public override string ToString() => base.ToString() + $", Bus BusOnTripId: {BusOnTripId} already exists";
        }
        public class BusOnTripNotFoundException : Exception
        {
            public int BusOnTripId;
            public BusOnTripNotFoundException(int busOnTripId) : base() => BusOnTripId = busOnTripId;
            public BusOnTripNotFoundException(int busOnTripId, string message) :
                base(message) => BusOnTripId = busOnTripId;
            public BusOnTripNotFoundException(int busOnTripId, string message, Exception innerException) :
                base(message, innerException) => BusOnTripId = busOnTripId;

            public override string ToString() => base.ToString() + $", Bus with BusOnTripId: {BusOnTripId} Not Found";
        }


        public class BusOnTripDeletedException : Exception
        {
            public int BusOnTripId;
            public BusOnTripDeletedException(int busOnTripId) : base() => BusOnTripId = busOnTripId;
            public BusOnTripDeletedException(int busOnTripId, string message) :
                base(message) => BusOnTripId = busOnTripId;
            public BusOnTripDeletedException(int busOnTripId, string message, Exception innerException) :
                base(message, innerException) => BusOnTripId = busOnTripId;

            public override string ToString() => base.ToString() + $", Bus BusOnTripId: {BusOnTripId} was deleted";
        }
    }

