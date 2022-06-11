namespace GeocacheSolution.Models
{
    public class GeocacheItem
    {
        public int GeocacheId { get; set; }
        public int ItemId { get; set; }
        public Item Item { get; set; }
        public Geocache Geocache { get; set; }
    }
}
