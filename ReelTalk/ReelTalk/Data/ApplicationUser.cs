using Microsoft.AspNetCore.Identity;

namespace ReelTalk.Data
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Comment> Comments { get; set; }
        public Watchlist Watchlist { get; set; }
    }
}
