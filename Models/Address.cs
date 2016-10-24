using System;
using System.Linq;
using System.Threading.Tasks;

namespace ALK.Core.Models
{

    public class Address
    {
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string County { get; set; }
        public string Country { get; set; }
        public object SPLC { get; set; }
        public int CountryPostalFilter { get; set; }
        public int AbbreviationFormat { get; set; }
    }
}
