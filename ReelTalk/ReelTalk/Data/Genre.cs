namespace ReelTalk.Data
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Production>? Productions { get; set; }
    }
}
