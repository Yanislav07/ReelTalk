namespace ReelTalk.Data
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }

        public ICollection<Comment> Comments { get; set; }
        public ICollection<Watchlist> Watchlist { get; set; }
    }
}
