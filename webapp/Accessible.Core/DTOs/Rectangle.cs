namespace Accessible.Core.DTOs
{
    public class Rectangle
    {
        public Rectangle(double minLat, double maxLat, double minLong, double maxLong)
        {
            MinLat = minLat;
            MaxLat = maxLat;
            MinLong = minLong;
            MaxLong = maxLong;
        }

        public double MinLat { get; set; }
        public double MaxLat { get; set; }
        public double MinLong { get; set; }
        public double MaxLong { get; set; }

        public bool Contains(double Lat, double Long)
        {
            return Lat >= MinLat && Lat <= MaxLat && Long >= MinLong && Long <= MaxLong;
        }
    }
}
