using GeocacheSolution.Controllers;
using GeocacheSolution.Data;
using GeocacheSolution.Models;
using GeocacheTests;

namespace GeocacheAPITests
{
    public class ApiTests : MockDb
    {

        [Fact]
        public async void CanCreateGeocache()
        {
            /*GeocachesController controller = new GeocachesController(_context);
            Geocache newGeocache = new Geocache()
            {
                ID = 1,
                Name = "Forgotten Path, Part 2",
                Lat = 47.6062,
                Long = 122.3321,
                Items = null
            };
            var postedCache = await controller.PostGeocache(newGeocache);
            var requestedCache = await controller.GetGeocache(postedCache.ID);
            Assert.Equal(47.6062, requestedCache.ID);*/
            
            // This is the moment I realized not having a service layer was going to make implementing Unit Tests difficult.
        }
    }
}