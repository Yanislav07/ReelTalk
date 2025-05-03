using System.ComponentModel;

namespace ReelTalk.Data
{
    public class Production
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int ReleaseYear { get; set; }
        public int Duration { get; set; } // in minutes
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public string Type { get; set; } // "Movie" or "Series"

        [DisplayName("Genre")]
        public int GenreId { get; set; }
        public virtual Genre? Genre { get; set; }

        public ICollection<Comment>? Comments { get; set; }
        public ICollection<WatchlistProduction>? WatchlistProductions { get; set; }
    }
}
