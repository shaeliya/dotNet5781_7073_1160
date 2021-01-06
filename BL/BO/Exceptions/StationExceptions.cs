using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO.Exceptions
{
   
        public class StationAlreadyExistsException : Exception
        {
            public int StationId;
            public StationAlreadyExistsException(int stationId) : base() => StationId = stationId;
            public StationAlreadyExistsException(int stationId, string message) :
                base(message) => StationId = stationId;
            public StationAlreadyExistsException(int stationId, string message, Exception innerException) :
                base(message, innerException) => StationId = stationId;

            public override string ToString() => base.ToString() + $", Bus StationId: {StationId} already exists";
        }
        public class StationNotFoundException : Exception
        {
            public int StationId;
            public StationNotFoundException(int stationId) : base() => StationId = stationId;
            public StationNotFoundException(int stationId, string message) :
                base(message) => StationId = stationId;
            public StationNotFoundException(int stationId, string message, Exception innerException) :
                base(message, innerException) => StationId = stationId;

            public override string ToString() => base.ToString() + $", Bus with StationId: {StationId} Not Found";
        }


        public class StationDeletedException : Exception
        {
            public int StationId;
            public StationDeletedException(int stationId) : base() => StationId = stationId;
            public StationDeletedException(int stationId, string message) :
                base(message) => StationId = stationId;
            public StationDeletedException(int stationId, string message, Exception innerException) :
                base(message, innerException) => StationId = stationId;

            public override string ToString() => base.ToString() + $", Bus StationId: {StationId} was deleted";
        }
    }

