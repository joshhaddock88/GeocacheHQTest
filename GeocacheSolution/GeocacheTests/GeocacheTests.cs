using GeocacheSolution.Models;
using GeocacheSolution.Controllers;
using GeocacheSolution.Data;

namespace GeocacheTests
{
    public class GeocacheTests
    {
        private readonly GeocacheContext _context;
        
        [Fact]
        public void Test1()
        {
            Geocache cache = new Geocache { ID=1, Name = "Forgotten Path", Lat = 47.6062, Long = 122.3321, ItemCount = 2 };
            Item item = new Item { Name = "Original Can of Beans", Active = true, GeocacheId = 1, FirstActive = DateTime.Parse("2000-05-02"), LastActive = DateTime.Today };
            ItemsController itemsController = new ItemsController(_context);
            itemsController.MoveItem(item);
        }
    }
}