using System.Collections.Generic;

namespace ALK.Core.Models
{
    public class ReverseGeoCodeResponse
    {
        public Address Address { get; set; }
        public Coords Coords { get; set; }
        public int Region { get; set; }
        public string Label { get; set; }
        public string PlaceName { get; set; }
        public string TimeZone { get; set; }
        public List<object> Errors { get; set; }
        public object SpeedLimitInfo { get; set; }
        public string ConfidenceLevel { get; set; }
        public double DistanceFromRoad { get; set; }
        public object CrossStreet { get; set; }
    }
}