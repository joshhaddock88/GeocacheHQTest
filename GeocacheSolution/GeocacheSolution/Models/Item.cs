using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeocacheSolution.Models
{
    public class Item
    {
        public int ID { get; set; }
        [StringLength(50)]
        [RegularExpression("^[A-Za-z 0-9]+$")]
        [Required]
        public string Name { get; set; }
        public bool Active { get; set; }
        public int? GeocacheId { get; set; }
        public DateTime FirstActive { get; set; }
        public DateTime LastActive { get; set; }
    }
}
