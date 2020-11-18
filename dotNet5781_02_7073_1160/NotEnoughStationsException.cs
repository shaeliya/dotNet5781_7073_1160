using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace dotNet5781_02_7073_1160
{

    [Serializable]
    public class NotEnoughStationsException : Exception
    {
        public string BusLineNumber { get; private set; }

        public NotEnoughStationsException() : base() { }
        public NotEnoughStationsException(string message) : base(message) { }
        public NotEnoughStationsException(string message, Exception inner) : base(message, inner) { }
        protected NotEnoughStationsException(SerializationInfo info, StreamingContext context)
     : base(info, context) { }
        public NotEnoughStationsException(string busLineNumber, string message) : base(message)
        { 
            BusLineNumber = busLineNumber; 
        }

        override public string ToString()
        { 
            return "NotEnoughStationsException in BusLineNumber:" + BusLineNumber + "\n" + Message;
        }
    }

}
