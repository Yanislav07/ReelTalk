namespace ReelTalk.Data
{
    public class MovieOrShow
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Type { get; set; } // "Movie" or "Series"

        public ICollection<Comment> Comments { get; set; }
        public ICollection<Watchlist> WatchlistedBy { get; set; }
    }
}
