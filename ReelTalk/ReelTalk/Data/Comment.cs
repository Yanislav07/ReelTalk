namespace ReelTalk.Data
{
    public class Comment
    {
        public int CommentId { get; set; }
        public int UserId { get; set; }
        public int MovieOrShowId { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
