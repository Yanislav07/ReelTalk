namespace ReelTalk.Data
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }

        public int ProductionId { get; set; }
        public virtual Production? Production { get; set; }

        public int UserId { get; set; }
        public ApplicationUser User { get; set; }

        public Comment()
        {
            CreatedDate = DateTime.Now;
        }
    }
}
