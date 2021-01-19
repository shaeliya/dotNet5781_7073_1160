using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PL_Transportation_System.Utils
{
    public static class UtilsFunctions
    {


        public static bool IsDigitsOnly(string str)//Checks if the license number is just digits
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }
            return true;
        }
    }
}
