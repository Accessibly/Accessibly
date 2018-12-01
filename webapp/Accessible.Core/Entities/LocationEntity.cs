using Accessible.Core.Utils;
using System;

namespace Accessible.Core.Entities
{
    class LocationEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public Category Category { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
    }
}
