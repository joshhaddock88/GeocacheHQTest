namespace GeocacheSolution.Models
{
    public class Cordinate
    {
        public double Lat { get; set; }
        public double Long { get; set; }

        public Cordinate(double lat, double @long)
        {
            Lat = lat;
            Long = @long;
        }
    }
}
