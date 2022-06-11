namespace GeocacheSolution.Models
{
    public class Item
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
        public DateTime FirstActive { get; set; }
        public DateTime LastActive { get; set; }
    }
}
