using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO.Exceptions
{
    
        public class LineStationAlreadyExistsException : Exception
        {
            public int LineStationId;
            public LineStationAlreadyExistsException(int lineStationId) : base() => LineStationId = lineStationId;
            public LineStationAlreadyExistsException(int lineStationId, string message) :
                base(message) => LineStationId = lineStationId;
            public LineStationAlreadyExistsException(int lineStationId, string message, Exception innerException) :
                base(message, innerException) => LineStationId = lineStationId;

            public override string ToString() => base.ToString() + $", Bus LineStationId: {LineStationId} already exists";
        }
        public class LineStationNotFoundException : Exception
        {
            public int LineStationId;
            public LineStationNotFoundException(int lineStationId) : base() => LineStationId = lineStationId;
            public LineStationNotFoundException(int lineStationId, string message) :
                base(message) => LineStationId = lineStationId;
            public LineStationNotFoundException(int lineStationId, string message, Exception innerException) :
                base(message, innerException) => LineStationId = lineStationId;

            public override string ToString() => base.ToString() + $", Bus with LineStationId: {LineStationId} Not Found";
        }


        public class LineStationDeletedException : Exception
        {
            public int LineStationId;
            public LineStationDeletedException(int lineStationId) : base() => LineStationId = lineStationId;
            public LineStationDeletedException(int lineStationId, string message) :
                base(message) => LineStationId = lineStationId;
            public LineStationDeletedException(int lineStationId, string message, Exception innerException) :
                base(message, innerException) => LineStationId = lineStationId;

            public override string ToString() => base.ToString() + $", Bus LineStationId: {LineStationId} was deleted";
        }
    }
