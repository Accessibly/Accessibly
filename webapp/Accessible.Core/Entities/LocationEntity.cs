using System;

namespace Accessible.Core.Entities
{
    class LocationEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Colour { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
    }
}
