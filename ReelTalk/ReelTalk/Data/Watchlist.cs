using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;

namespace ReelTalk.Data
{
    public class Watchlist
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public ICollection<WatchlistProduction>? WatchlistProductions { get; set; }
    }
}