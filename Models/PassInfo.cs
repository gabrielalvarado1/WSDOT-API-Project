using System;
using System.Collections.Generic;
using System.Text;

namespace WSDOT_API_Project.Models
{
    /*public class RestrictionOne
    {
        public string RestrictionText { get; set; }
        public string TravelDirection { get; set; }
    }

    public class RestrictionTwo
    {
        public string RestrictionText { get; set; }
        public string TravelDirection { get; set; }
    }*/

    // created these classes by using jsontoc# website.
    public class PassInfo
    {
        public DateTime DateUpdated { get; set; }
        public int ElevationInFeet { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int MountainPassId { get; set; }
        public string MountainPassName { get; set; }
        public RestrictionOne RestrictionOne { get; set; }
        public RestrictionTwo RestrictionTwo { get; set; }
        public string RoadCondition { get; set; }
        public int? TemperatureInFahrenheit { get; set; }
        public bool TravelAdvisoryActive { get; set; }
        public string WeatherCondition { get; set; }
    }
}
