using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AuthProject.Models;
using AuthProject.ViewModels;

namespace AuthProject.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminController(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET: /Admin/Users
        public async Task<IActionResult> Users()
        {
            var users = await _userManager.Users.ToListAsync();
            var model = new List<UserWithRolesViewModel>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                model.Add(new UserWithRolesViewModel
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    Age = user.Age,
                    Email = user.Email ?? "",
                    Roles = roles.ToList(),
                    IsLockedOut = user.LockoutEnd.HasValue &&
                                  user.LockoutEnd > DateTimeOffset.UtcNow
                });
            }

            return View(model);
        }

        // GET: /Admin/EditUser/id
        public async Task<IActionResult> EditUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            var userRoles = await _userManager.GetRolesAsync(user);
            var allRoles = _roleManager.Roles.Select(r => r.Name!).ToList();

            var model = new EditUserViewModel
            {
                Id = user.Id,
                FullName = user.FullName,
                Age = user.Age,
                Email = user.Email ?? "",
                AllRoles = allRoles,
                SelectedRoles = userRoles.ToList()
            };

            return View(model);
        }

        // POST: /Admin/EditUser
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(EditUserViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.Id);
            if (user == null) return NotFound();

            // Update name
            user.FullName = model.FullName;
            user.Age = model.Age;
            await _userManager.UpdateAsync(user);

            // Update roles
            var currentRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, currentRoles);

            if (model.SelectedRoles != null && model.SelectedRoles.Any())
                await _userManager.AddToRolesAsync(user, model.SelectedRoles);

            TempData["Success"] = $"{user.Email} updated successfully.";
            return RedirectToAction(nameof(Users));
        }

        // POST: /Admin/LockUser
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LockUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            await _userManager.SetLockoutEndDateAsync(
                user, DateTimeOffset.UtcNow.AddYears(100));

            TempData["Success"] = $"{user.Email} has been locked.";
            return RedirectToAction(nameof(Users));
        }

        // POST: /Admin/UnlockUser
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UnlockUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            await _userManager.SetLockoutEndDateAsync(user, null);
            await _userManager.ResetAccessFailedCountAsync(user);

            TempData["Success"] = $"{user.Email} has been unlocked.";
            return RedirectToAction(nameof(Users));
        }

        // POST: /Admin/DeleteUser
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            // Prevent deleting yourself
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser?.Id == id)
            {
                TempData["Error"] = "You cannot delete your own account.";
                return RedirectToAction(nameof(Users));
            }

            await _userManager.DeleteAsync(user);
            TempData["Success"] = $"{user.Email} deleted.";
            return RedirectToAction(nameof(Users));
        }
    }
}