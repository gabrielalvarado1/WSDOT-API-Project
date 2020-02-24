using System;
using System.Collections.Generic;
using System.Text;

namespace WSDOT_API_Project.Utilities
{
    public class URLBuilder
    {
        public static string BuildURL(string accessCode)
        {
            string url = "http://www.wsdot.wa.gov/Traffic/api/MountainPassConditions/MountainPassConditionsREST.svc/GetMountainPassConditionsAsJson?AccessCode={" + accessCode + "}";
            return url;
        }
    }
}
