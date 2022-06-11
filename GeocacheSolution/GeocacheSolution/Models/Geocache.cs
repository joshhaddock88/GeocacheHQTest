using System.ComponentModel.DataAnnotations;

namespace GeocacheSolution.Models
{
    public class Geocache
    {
        [Key]
        public int GeocacheId { get; set; }
        public string Name { get; set; }
        public Cordinate Cordinate { get; set; }
        public ICollection<GeocacheItem> GeocacheItems { get; set; }
    }
}
