using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO.Exceptions
{
    
        public class UserAlreadyExistsException : Exception
        {
            public string UserName;
            public UserAlreadyExistsException(string userName) : base() => UserName = userName;
            public UserAlreadyExistsException(string userName, string message) :
                base(message) => UserName = userName;
            public UserAlreadyExistsException(string userName, string message, Exception innerException) :
                base(message, innerException) => UserName = userName;

            public override string ToString() => base.ToString() + $", Bus UserName: {UserName} already exists";
        }
        public class UserNotFoundException : Exception
        {
            public string UserName;
            public UserNotFoundException(string userName) : base() => UserName = userName;
            public UserNotFoundException(string userName, string message) :
                base(message) => UserName = userName;
            public UserNotFoundException(string userName, string message, Exception innerException) :
                base(message, innerException) => UserName = userName;

            public override string ToString() => base.ToString() + $", Bus with UserName: {UserName} Not Found";
        }


        public class UserDeletedException : Exception
        {
            public string UserName;
            public UserDeletedException(string userName) : base() => UserName = userName;
            public UserDeletedException(string userName, string message) :
                base(message) => UserName = userName;
            public UserDeletedException(string userName, string message, Exception innerException) :
                base(message, innerException) => UserName = userName;

            public override string ToString() => base.ToString() + $", Bus UserName: {UserName} was deleted";
        }
    }

