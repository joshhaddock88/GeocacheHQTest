namespace GeocacheSolution.Models
{
    public class ActiveDates
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public ActiveDates(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
        }
    }
}
