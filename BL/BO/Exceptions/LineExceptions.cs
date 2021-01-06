using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO.Exceptions
{

    public class LineAlreadyExistsException : Exception
    {
        public int LineId;
        public LineAlreadyExistsException(int lineId) : base() => LineId = lineId;
        public LineAlreadyExistsException(int lineId, string message) :
            base(message) => LineId = lineId;
        public LineAlreadyExistsException(int lineId, string message, Exception innerException) :
            base(message, innerException) => LineId = lineId;

        public override string ToString() => base.ToString() + $", Bus LineId: {LineId} already exists";
    }
    public class LineNotFoundException : Exception
    {
        public int LineId;
        public LineNotFoundException(int lineId) : base() => LineId = lineId;
        public LineNotFoundException(int lineId, string message) :
            base(message) => LineId = lineId;
        public LineNotFoundException(int lineId, string message, Exception innerException) :
            base(message, innerException) => LineId = lineId;

        public override string ToString() => base.ToString() + $", Bus with LineId: {LineId} Not Found";
    }


    public class LineDeletedException : Exception
    {
        public int LineId;
        public LineDeletedException(int lineId) : base() => LineId = lineId;
        public LineDeletedException(int lineId, string message) :
            base(message) => LineId = lineId;
        public LineDeletedException(int lineId, string message, Exception innerException) :
            base(message, innerException) => LineId = lineId;

        public override string ToString() => base.ToString() + $", Bus LineId: {LineId} was deleted";
    }
}
