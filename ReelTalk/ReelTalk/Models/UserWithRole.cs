
namespace ReelTalk.Models
{
    public class UserWithRole
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public string CurrentRole { get; set; }
        public string NewRole { get; set; }
        public IEnumerable<char>? Role { get; set; }

        public UserWithRole(string userId, string email, string currentRole)
        {
            UserId = userId;
            Email = email;
            CurrentRole = currentRole;
        }
    }
}
