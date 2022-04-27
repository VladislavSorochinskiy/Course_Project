using DataLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Models.AdminModels;

namespace PresentationLayer.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly SignInManager<User> signInManager;

        public AdminController(RoleManager<IdentityRole> roleManager, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View(userManager.Users.ToList());
        }

        public async Task<IActionResult> Edit(string userId)
        {
            User user = await userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var userRoles = await userManager.GetRolesAsync(user);
                var allRoles = roleManager.Roles.ToList();
                ChangeRoleViewModel model = new ChangeRoleViewModel
                {
                    UserId = user.Id,
                    UserEmail = user.Email,
                    UserRoles = userRoles,
                    AllRoles = allRoles
                };
                return View(model);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string userId, List<string> roles)
        {
            User user = await userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var userRoles = await userManager.GetRolesAsync(user);
                var addedRoles = roles.Except(userRoles);
                var removedRoles = userRoles.Except(roles);
                await userManager.AddToRolesAsync(user, addedRoles);
                await userManager.RemoveFromRolesAsync(user, removedRoles);

                return RedirectToAction("Index", "Admin");
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string[] selectedUsers)
        {
            foreach (var id in selectedUsers)
            {
                User user = await userManager.FindByIdAsync(id);
                await SignOutUser(user);
                await userManager.DeleteAsync(user);
            }

            return RedirectToAction("Index", "Admin");
        }

        [HttpPost]
        public async Task<IActionResult> Block(string[] selectedUsers)
        {
            foreach (var id in selectedUsers)
            {
                User user = await userManager.FindByIdAsync(id);
                await SetBlockedStatus(user, true);
                await SignOutUser(user);
            }

            return RedirectToAction("Index", "Admin");
        }

        [HttpPost]
        public async Task<IActionResult> Unlock(string[] selectedUsers)
        {
            foreach (var id in selectedUsers)
            {
                User user = await userManager.FindByIdAsync(id);
                await SetBlockedStatus(user, false);
            }

            return RedirectToAction("Index", "Admin");
        }

        private async Task SetBlockedStatus(User user, bool isBlocked)
        {
            user.IsBlocked = isBlocked;
            await userManager.UpdateAsync(user);
        }

        private async Task SignOutUser(User user)
        {
            User currentUser = await userManager.GetUserAsync(HttpContext.User);

            if (user.Id.Equals(currentUser.Id))
            {
                await signInManager.SignOutAsync();
            }
            else
            {
                await userManager.UpdateSecurityStampAsync(user);
            }
        }
    }
}