namespace ReelTalk.Data
{
    public class Watchlist
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int MovieOrShowId { get; set; }
        public MovieOrShow MovieOrShow { get; set; }

        public string Status { get; set; } = "Planned";
        public DateTime AddedAt { get; set; } = DateTime.Now;
    }
}
