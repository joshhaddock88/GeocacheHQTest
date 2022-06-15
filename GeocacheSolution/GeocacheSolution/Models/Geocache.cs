using System.ComponentModel.DataAnnotations;

namespace GeocacheSolution.Models
{
    public class Geocache
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [RegularExpression(@"\d+(\.\d{1,4})?", ErrorMessage = "Too many decimal spaces.")]
        public double Lat { get; set; }
        [Required]
        public double Long { get; set; }
        public ICollection<Item> Items { get; set; }
    }
}
