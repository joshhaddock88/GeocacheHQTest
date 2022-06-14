using System.ComponentModel.DataAnnotations;

namespace GeocacheSolution.Models
{
    public class Geocache
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double Lat { get; set; }
        [Required]
        public double Long { get; set; }
        public ICollection<Item> Items { get; set; }
    }
}
