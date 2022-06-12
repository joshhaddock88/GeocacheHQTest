namespace GeocacheSolution.Models
{
    public class Geocache
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
        public ICollection<Item> Items { get; set; }
    }
}
