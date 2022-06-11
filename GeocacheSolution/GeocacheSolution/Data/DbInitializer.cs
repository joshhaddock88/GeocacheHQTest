using GeocacheSolution.Models;

namespace GeocacheSolution.Data
{
    public class DbInitializer
    {
        public static void Initialize(GeocacheContext context)
        {
            context.Database.EnsureCreated();

            // Look for dtabase existence.
            if(context.Geocaches.Any())
            {
                return; // DB has already been seeded
            }

            var geocaches = new List<Geocache>()
            {
                new Geocache{Name="Twin Peaks", Cordinate=new Cordinate(47.6062, 122.3321), ItemCount=0},
                new Geocache{Name="Place Beyond the Pines", Cordinate=new Cordinate(44.7062, 123.3321), ItemCount=0},
                new Geocache{Name="That One Time At Bernies", Cordinate=new Cordinate(40.6067, 121.7321), ItemCount=0}
            };

            var items = new List<Item>()
            {
                new Item{Name="Original Can of Beans", 
                    Active=true, 
                    ActiveDates=new ActiveDates(new DateTime(2000, 05, 02), DateTime.Now)}
            };

            var geocacheitems = new List<GeocacheItem>()
            {
                new GeocacheItem{GeocacheId=1, ItemId=1}
            };
        }
    }
}
