using CSDproject.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSDproject.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AdminController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        // GET: Admin/Index
        public IActionResult Index()
        {
            return View();
        }

        // GET: Admin/RoleManagement
        public IActionResult RoleManagement()
        {
            var roles = _roleManager.Roles.ToList();
            return View(roles);
        }

        // POST: Admin/CreateRole
        [HttpPost]
        public async Task<IActionResult> CreateRole(string roleName)
        {
            if (!string.IsNullOrWhiteSpace(roleName))
            {
                var roleExists = await _roleManager.RoleExistsAsync(roleName);
                if (!roleExists)
                {
                    await _roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
            return RedirectToAction(nameof(RoleManagement));
        }

        // GET: Admin/ManageUsers
        public async Task<IActionResult> ManageUsers()
        {
            var users = _userManager.Users.ToList();
            var roles = _roleManager.Roles.Select(r => r.Name).ToList(); // Get all role names
            var userRoleViewModels = new List<UserRoleViewModel>();

            foreach (var user in users)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                userRoleViewModels.Add(new UserRoleViewModel
                {
                    UserId = user.Id,
                    Email = user.Email,
                    Roles = userRoles.ToList() // Assign as a list
                });
            }

            ViewBag.AllRoles = roles; // Pass roles to the view
            return View(userRoleViewModels);
        }



        // POST: Admin/AssignRole
        [HttpPost]
        public async Task<IActionResult> AssignRole(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null && !string.IsNullOrWhiteSpace(roleName))
            {
                await _userManager.AddToRoleAsync(user, roleName);
            }
            return RedirectToAction(nameof(ManageUsers));
        }

        // POST: Admin/RemoveRole
        [HttpPost]
        public async Task<IActionResult> RemoveRole(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null && !string.IsNullOrWhiteSpace(roleName))
            {
                await _userManager.RemoveFromRoleAsync(user, roleName);
            }
            return RedirectToAction(nameof(ManageUsers));
        }

        // POST: Admin/EditRole
        [HttpPost]
        public async Task<IActionResult> EditRole(string roleId, string newRoleName)
        {
            if (!string.IsNullOrWhiteSpace(roleId) && !string.IsNullOrWhiteSpace(newRoleName))
            {
                var role = await _roleManager.FindByIdAsync(roleId);
                if (role != null)
                {
                    role.Name = newRoleName;
                    await _roleManager.UpdateAsync(role);
                }
            }
            return RedirectToAction(nameof(RoleManagement));
        }

        // POST: Admin/DeleteRole
        [HttpPost]
        public async Task<IActionResult> DeleteRole(string roleId)
        {
            if (!string.IsNullOrWhiteSpace(roleId))
            {
                var role = await _roleManager.FindByIdAsync(roleId);
                if (role != null)
                {
                    await _roleManager.DeleteAsync(role);
                }
            }
            return RedirectToAction(nameof(RoleManagement));
        }

        // POST: Admin/DeleteUser
        [HttpPost]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }
            return RedirectToAction(nameof(ManageUsers));
        }

    }
}


