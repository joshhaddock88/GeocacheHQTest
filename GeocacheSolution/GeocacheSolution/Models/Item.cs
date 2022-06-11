using System.ComponentModel.DataAnnotations;

namespace GeocacheSolution.Models
{
    public class Item
    {
        [Key]
        public int ItemId { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
        public ActiveDates ActiveDates { get; set; }
    }
}
