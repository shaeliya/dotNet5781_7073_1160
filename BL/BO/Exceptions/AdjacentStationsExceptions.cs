using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO.Exceptions
{
        public class AdjacentStationsAlreadyExistsException : Exception
        {
            public int AdjacentStationsId;
            public AdjacentStationsAlreadyExistsException(int adjacentStationsId) : base() => AdjacentStationsId = adjacentStationsId;
            public AdjacentStationsAlreadyExistsException(int adjacentStationsId, string message) :
                base(message) => AdjacentStationsId = adjacentStationsId;
            public AdjacentStationsAlreadyExistsException(int adjacentStationsId, string message, Exception innerException) :
                base(message, innerException) => AdjacentStationsId = adjacentStationsId;

            public override string ToString() => base.ToString() + $", Bus AdjacentStationsId: {AdjacentStationsId} already exists";
        }
        public class AdjacentStationsNotFoundException : Exception
        {
            public int AdjacentStationsId;
            public AdjacentStationsNotFoundException(int adjacentStationsId) : base() => AdjacentStationsId = adjacentStationsId;
            public AdjacentStationsNotFoundException(int adjacentStationsId, string message) :
                base(message) => AdjacentStationsId = adjacentStationsId;
            public AdjacentStationsNotFoundException(int adjacentStationsId, string message, Exception innerException) :
                base(message, innerException) => AdjacentStationsId = adjacentStationsId;

            public override string ToString() => base.ToString() + $", Bus with AdjacentStationsId: {AdjacentStationsId} Not Found";
        }


        public class AdjacentStationsDeletedException : Exception
        {
            public int AdjacentStationsId;
            public AdjacentStationsDeletedException(int adjacentStationsId) : base() => AdjacentStationsId = adjacentStationsId;
            public AdjacentStationsDeletedException(int adjacentStationsId, string message) :
                base(message) => AdjacentStationsId = adjacentStationsId;
            public AdjacentStationsDeletedException(int adjacentStationsId, string message, Exception innerException) :
                base(message, innerException) => AdjacentStationsId = adjacentStationsId;

            public override string ToString() => base.ToString() + $", Bus AdjacentStationsId: {AdjacentStationsId} was deleted";
        }
    }

