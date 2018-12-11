namespace Accessible.Core.Utils
{
    public class Coordinate
    {
        public Coordinate(double lat, double lng)
        {
            Lat = lat;
            Lng = lng;
        }

        public double Lat { get; set; }
        public double Lng { get; set; }
    }
}
