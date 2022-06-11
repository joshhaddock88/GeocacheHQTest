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

            var geocaches = new Geocache[]
            {
                new Geocache{Name="Forgotten Path", Lat=47.6062, Long=122.3321},
                new Geocache{Name="Place Beyond the Pines", Lat=47.6162, Long=122.3421},
                new Geocache{Name="That One Time At Bernies", Lat=47.6262, Long=122.3521},
                new Geocache{Name="Under Flowers", Lat=47.6362, Long=122.3621},
                new Geocache{Name="Cat vs Dog", Lat=47.6462, Long=122.3721},
                new Geocache{Name="It's a Terrarium", Lat=47.6562, Long=122.3821},
                new Geocache{Name="Yonder There Tree", Lat=47.6662, Long=122.3921},
                new Geocache{Name="CooCoo for... Fiddleheads", Lat=47.6762, Long=122.3421}
            };
            foreach(Geocache g in geocaches)
            {
                context.Geocaches.Add(g);
            }
            context.SaveChanges();

            var items = new Item[]
            {
                new Item{Name="Original Can of Beans", Active=true, FirstActive=DateTime.Parse("2000-05-02"), LastActive=DateTime.Now},
                new Item{Name="Tin Case", Active=false, FirstActive=DateTime.Parse("2000-05-02"), LastActive=DateTime.Now},
                new Item{Name="Panda Figurine", Active=true, FirstActive=DateTime.Parse("2000-05-02"), LastActive=DateTime.Now},
                new Item{Name="Sunglasses", Active=true, FirstActive=DateTime.Parse("2000-05-02"), LastActive=DateTime.Now},
                new Item{Name="Silly Putty", Active=true, FirstActive=DateTime.Parse("2000-05-02"), LastActive=DateTime.Now},
                new Item{Name="A Shoe", Active=true, FirstActive=DateTime.Parse("2000-05-02"), LastActive=DateTime.Now},
                new Item{Name="A beautiful leaf", Active=false, FirstActive=DateTime.Parse("2000-05-02"), LastActive=DateTime.Now},
                new Item{Name="measuring tape", Active=true, FirstActive=DateTime.Parse("2000-05-02"), LastActive=DateTime.Now},
                new Item{Name="A tomato timer", Active=true, FirstActive=DateTime.Parse("2000-05-02"), LastActive=DateTime.Now}
            };
            foreach(Item i in items)
            {
                context.Items.Add(i);
            }
            context.SaveChanges();

            var geocacheitems = new GeocacheItem[]
            {
                new GeocacheItem{GeocacheId=1, ItemId=1},
                new GeocacheItem{GeocacheId=1, ItemId=2},
                new GeocacheItem{GeocacheId=3, ItemId=3},
                new GeocacheItem{GeocacheId=4, ItemId=4},
                new GeocacheItem{GeocacheId=4, ItemId=5},
                new GeocacheItem{GeocacheId=4, ItemId=6},
                new GeocacheItem{GeocacheId=7, ItemId=7},
                new GeocacheItem{GeocacheId=8, ItemId=8},
                new GeocacheItem{GeocacheId=8, ItemId=9}
            };
            foreach(GeocacheItem gi in geocacheitems)
            {
                context.GeocacheItems.Add(gi);
            }
            context.SaveChanges();
        }
    }
}
