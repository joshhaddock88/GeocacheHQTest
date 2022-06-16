using GeocacheSolution.Data;
using GeocacheSolution.Models;
using GeocacheSolution.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeocacheTests
{
    public class GeocacheSolutionTests : MockDb
    {
        [Fact]
        public async void CanCreateGeocache()
        {
            GeocachesController controller = new GeocachesController(_context);
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
            Assert.Equal(47.6062, requestedCache.ID);
        }
    }
}
