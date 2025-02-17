//using BusPortal.DAL.Persistence.Entities;
//using BusPortal.Web.Models.DTO;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;

//namespace BusPortal.Web.Controllers
//{
//    public class AccountController : Controller
//    {
//        //private readonly SignInManager<ApplicationUser> _signInManager;
//        //private readonly UserManager<ApplicationUser> _userManager;

//        //public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
//        //{
//        //    _signInManager = signInManager;
//        //    _userManager = userManager;
//        //}

//        [HttpGet]
//        public IActionResult Login()
//        {
//            return View();
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Login(LoginViewModel model)
//        {
//            if (ModelState.IsValid)
//            {
//                // Find the user by username (or email)
//                var user = await _userManager.FindByNameAsync(model.Username);
//                if (user == null)
//                {
//                    ModelState.AddModelError("", "Invalid login attempt.");
//                    return View(model);
//                }

//                // Use PasswordSignInAsync to check if credentials are valid
//                var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
//                if (result.Succeeded)
//                {
//                    // If login is successful, redirect to home or dashboard
//                    return RedirectToAction("Index", "Home");
//                }
//                else
//                {
//                    // If login fails, show an error message
//                    ModelState.AddModelError("", "Invalid login attempt.");
//                    return View(model);
//                }
//            }

//            return View(model);
//        }

//        // Other actions like Logout, Register, etc.
//    }
//}
