using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReelTalk.Models;

namespace ReelTalk.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdministrationController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public AdministrationController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            var usersWithRole = new List<UserWithRole>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var userWithRole = new UserWithRole(user.Id, user.Email, roles.FirstOrDefault());
                usersWithRole.Add(userWithRole);
            }

            return View(usersWithRole);
        }
            
        public async Task<IActionResult> AlterRole(string UserId, string role)
        {
            IdentityUser user = null;

            if (!string.IsNullOrEmpty(UserId))
            {
                user = await _userManager.FindByIdAsync(UserId);
            }

            if (user != null)
            {
                var currentRoles = await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRoleAsync(user, currentRoles.FirstOrDefault());

                await _userManager.AddToRoleAsync(user, role);
            }

            return RedirectToAction("Index");
        }
    }
}
