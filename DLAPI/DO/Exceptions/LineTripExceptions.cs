using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO.Exceptions
{

    public class LineTripAlreadyExistsException : Exception
    {
        public int LineTripId;
        public LineTripAlreadyExistsException(int lineTripId) : base() => LineTripId = lineTripId;
        public LineTripAlreadyExistsException(int lineTripId, string message) :
            base(message) => LineTripId = lineTripId;
        public LineTripAlreadyExistsException(int lineTripId, string message, Exception innerException) :
            base(message, innerException) => LineTripId = lineTripId;

        public override string ToString() => base.ToString() + $", Bus LineTripId: {LineTripId} already exists";
    }
    public class LineTripNotFoundException : Exception
    {
        public int LineTripId;
        public LineTripNotFoundException(int lineTripId) : base() => LineTripId = lineTripId;
        public LineTripNotFoundException(int lineTripId, string message) :
            base(message) => LineTripId = lineTripId;
        public LineTripNotFoundException(int lineTripId, string message, Exception innerException) :
            base(message, innerException) => LineTripId = lineTripId;

        public override string ToString() => base.ToString() + $", Bus with LineTripId: {LineTripId} Not Found";
    }


    public class LineTripDeletedException : Exception
    {
        public int LineTripId;
        public LineTripDeletedException(int lineTripId) : base() => LineTripId = lineTripId;
        public LineTripDeletedException(int lineTripId, string message) :
            base(message) => LineTripId = lineTripId;
        public LineTripDeletedException(int lineTripId, string message, Exception innerException) :
            base(message, innerException) => LineTripId = lineTripId;

        public override string ToString() => base.ToString() + $", Bus LineTripId: {LineTripId} was deleted";
    }
}

