using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace dotNet5781_02_7073_1160
{

    [Serializable]
    public class ItemAlreadyExistsException : Exception
    {
        public string ItemName { get; private set; }

        public ItemAlreadyExistsException() : base() { }
        public ItemAlreadyExistsException(string message) : base(message) { }
        public ItemAlreadyExistsException(string message, Exception inner) : base(message, inner) { }
        protected ItemAlreadyExistsException(SerializationInfo info, StreamingContext context)
     : base(info, context) { }
        // special constructor for our custom exception
        public ItemAlreadyExistsException(string itemName, string message) : base(message)
        { 
            ItemName = itemName; 
        }

        override public string ToString()
        { 
            return "ItemAlreadyExistsException in ItemName:" + ItemName + "\n" + Message;
        }
    }

}
