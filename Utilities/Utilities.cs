using System;
using System.Collections.Generic;
using System.Text;

namespace WSDOT_API_Project.Utilities
{
    public static class Utilities
    {
        public static bool ToInt(string number) 
        {
            int tempNumber;
            if (int.TryParse(number.Trim(), out tempNumber))
            {
                return true;
            }
            else 
            {
                return false;
            }
        }
    }
}
