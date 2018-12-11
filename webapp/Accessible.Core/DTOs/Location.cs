using Accessible.Core.Utils;
using System;
using System.Collections.Generic;

namespace Accessible.Core.DTOs
{
    public class Location
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Colour { get; set; }
        public Coordinate Coordinate { get; set; }
        public LocationType Type { get; set; }
        public IEnumerable<Feature> Features { get; } = new List<Feature>();
        public string Description { get; set; }
        public ContactDetails ContactDetails { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
    }

    public enum LocationType
    {
        Unknown = 0,
        Food,
        Social,
        Natural,
        Educational
    }
}
