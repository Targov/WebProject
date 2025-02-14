using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using eTickets.Data;
using eTickets.Models;
using System.Threading.Tasks;
using System.Linq;

namespace eTickets.Controllers
{
    public class AdminResetController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminResetController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet("ResetAdminPassword")]
        public async Task<IActionResult> ResetAdminPassword()
        {
            var adminUser = await _userManager.FindByEmailAsync("admin@etickets.com");
            if (adminUser != null)
            {
                var newPassword = "NewSecurePassword123!";
                var token = await _userManager.GeneratePasswordResetTokenAsync(adminUser);
                var result = await _userManager.ResetPasswordAsync(adminUser, token, newPassword);

                if (result.Succeeded)
                {
                    return Content($"Admin password successfully reset to: {newPassword}");
                }
                else
                {
                    return Content("Error resetting admin password: " + string.Join(", ", result.Errors.Select(e => e.Description)));
                }
            }

            return Content("Admin user not found.");
        }
    }
}
