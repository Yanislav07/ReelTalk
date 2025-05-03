namespace ReelTalk.Data
{
    public class WatchlistProduction
    {
        public int WatchListId { get; set; }
        public Watchlist Watchlist { get; set; }

        public int ProductionId { get; set; }
        public Production Production { get; set; }

        public DateTime AddedDate { get; set; }

        public WatchlistProduction()
        {
            AddedDate = DateTime.Now;
        }
    }
}
