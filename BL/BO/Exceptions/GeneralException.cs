using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BO.Exceptions
{
    public class GeneralException : Exception
    {
        public string FuncName;
        public GeneralException(string funcName) : base() => FuncName = funcName;
        public GeneralException(string funcName, string message) :
            base(message) => FuncName = funcName;
        public GeneralException(string funcName, string message, Exception innerException) :
            base(message, innerException) => FuncName = funcName;
        public override string ToString() => base.ToString() + $",  FuncName: {FuncName} General Error";
    }
}
