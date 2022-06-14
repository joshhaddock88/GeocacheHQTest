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
                new Geocache{Name="Forgotten Path", Lat=47.6062, Long=122.3321, ItemCount=2},
                new Geocache{Name="Place Beyond the Pines", Lat=47.6162, Long=122.3421, ItemCount=0},
                new Geocache{Name="That One Time At Bernies", Lat=47.6262, Long=122.3521, ItemCount=1},
                new Geocache{Name="Under Flowers", Lat=47.6362, Long=122.3621, ItemCount=3},
                new Geocache{Name="Cat vs Dog", Lat=47.6462, Long=122.3721, ItemCount=0},
                new Geocache{Name="It's a Terrarium", Lat=47.6562, Long=122.3821,  ItemCount=0},
                new Geocache{Name="Yonder There Tree", Lat=47.6662, Long=122.3921, ItemCount=1},
                new Geocache{Name="CooCoo for... Fiddleheads", Lat=47.6762, Long=122.3421, ItemCount=2}
            };
            foreach(Geocache g in geocaches)
            {
                context.Geocaches.Add(g);
            }
            context.SaveChanges();

            var items = new Item[]
            {
                new Item{Name="Original Can of Beans", Active=true, GeocacheId=1, FirstActive=DateTime.Parse("2000-05-02"), LastActive=DateTime.Today},
                new Item{Name="Tin Case", Active=false, GeocacheId=1, FirstActive=DateTime.Parse("2000-05-02"), LastActive=DateTime.Today},
                new Item{Name="Panda Figurine", Active=true, GeocacheId=3, FirstActive=DateTime.Parse("2000-05-02"), LastActive=DateTime.Today},
                new Item{Name="Sunglasses", Active=true, GeocacheId=4, FirstActive=DateTime.Parse("2000-05-02"), LastActive=DateTime.Today},
                new Item{Name="Silly Putty", Active=true, GeocacheId=4, FirstActive=DateTime.Parse("2000-05-02"), LastActive=DateTime.Today},
                new Item{Name="A Shoe", Active=true, GeocacheId=4, FirstActive=DateTime.Parse("2000-05-02"), LastActive=DateTime.Today},
                new Item{Name="A beautiful leaf", GeocacheId=7, Active=false, FirstActive=DateTime.Parse("2000-05-02"), LastActive=DateTime.Today},
                new Item{Name="measuring tape", Active=true, GeocacheId=8, FirstActive=DateTime.Parse("2000-05-02"), LastActive=DateTime.Today},
                new Item{Name="A tomato timer", Active=true, GeocacheId=8, FirstActive=DateTime.Parse("2000-05-02"), LastActive=DateTime.Today}
            };
            foreach(Item i in items)
            {
                context.Items.Add(i);
            }
            context.SaveChanges();
        }
    }
}
