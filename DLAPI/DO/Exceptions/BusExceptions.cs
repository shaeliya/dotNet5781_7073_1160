using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO.Exceptions
{

    public class BusAlreadyExistsException : Exception
    {
        public int LicenseNumber;
        public BusAlreadyExistsException(int licenseNumber) : base() => LicenseNumber = licenseNumber;
        public BusAlreadyExistsException(int licenseNumber, string message) :
            base(message) => LicenseNumber = licenseNumber;
        public BusAlreadyExistsException(int licenseNumber, string message, Exception innerException) :
            base(message, innerException) => LicenseNumber = licenseNumber;

        public override string ToString() => base.ToString() + $", Bus LicenseNumber: {LicenseNumber} already exists";
    }
    public class BusNotFoundException : Exception
    {
        public int LicenseNumber;
        public BusNotFoundException(int licenseNumber) : base() => LicenseNumber = licenseNumber;
        public BusNotFoundException(int licenseNumber, string message) :
            base(message) => LicenseNumber = licenseNumber;
        public BusNotFoundException(int licenseNumber, string message, Exception innerException) :
            base(message, innerException) => LicenseNumber = licenseNumber;

        public override string ToString() => base.ToString() + $", Bus with LicenseNumber: {LicenseNumber} Not Found";
    }


    public class BusDeletedException : Exception
    {
        public int LicenseNumber;
        public BusDeletedException(int licenseNumber) : base() => LicenseNumber = licenseNumber;
        public BusDeletedException(int licenseNumber, string message) :
            base(message) => LicenseNumber = licenseNumber;
        public BusDeletedException(int licenseNumber, string message, Exception innerException) :
            base(message, innerException) => LicenseNumber = licenseNumber;

        public override string ToString() => base.ToString() + $", Bus LicenseNumber: {LicenseNumber} was deleted";
    }

}
