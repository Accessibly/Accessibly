using Accessible.Core.Utils;

namespace Accessible.Core.DTOs
{
    public class Rectangle
    {
        public Rectangle()
        { }

        public Rectangle(Coordinate southWest, Coordinate northEast)
        {
            SouthWest = southWest;
            NorthEast = northEast;
        }

        public Rectangle(double minLat, double minLong, double maxLat, double maxLong)
            : this(new Coordinate(minLat, minLong), new Coordinate(maxLat, maxLong))
        { }

        public Coordinate SouthWest { get; set; }
        public Coordinate NorthEast { get; set; }

        public bool Contains(Coordinate point)
        {
            return point.Lat >= SouthWest.Lat && point.Lat <= NorthEast.Lat && point.Lng >= SouthWest.Lng && point.Lng <= NorthEast.Lng;
        }
    }
}
