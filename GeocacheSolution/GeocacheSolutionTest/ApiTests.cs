using Microsoft.AspNetCore.Mvc.Testing;
using GeocacheSolution;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using GeocacheSolution.Models;

namespace GeocacheSolutionTest
{
    public class ApiTests : IClassFixture<WebApplicationFactory<Program>>
    {

        [Fact]
        public async Task GET_Retrieves_Geocaches()
        {
            var api = new GeocacheWebApplicationFactory();
            var geocaches = await api.CreateClient().GetAsync("/Items");
            Assert.True(geocaches.StatusCode.Equals(200));
            // Integration test failing. Returning 404.
            // There must be something wrong with the wiring, packages, etc.
            // Due to time constraints I am unable to finish writing automated tests.
        }
    }
}