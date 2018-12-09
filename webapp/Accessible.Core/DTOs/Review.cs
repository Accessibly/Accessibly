using System;
using System.Collections.Generic;

namespace Accessible.Core.DTOs
{
    class Review
    {
        public Guid Id { get; set; }
        public DateTime Timestamp { get; set; }
        public string Content { get; set; }
        public IEnumerable<Feature> PresentFeatures { get; } = new List<Feature>();
        public IEnumerable<Feature> AbsentFeatures { get; } = new List<Feature>();
        public User User { get; set; }
        public Location Location { get; set; }
    }
}
