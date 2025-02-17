using AutoMapper; 
using BusPortal.BLL.Services.Interfaces;
using BusPortal.BLL.Services.Scoped;
using BusPortal.Common.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BusPortal.Web.Controllers
{
    public class ClientsController : Controller
    {
        private readonly IClientService _clientService;
        private readonly UserService _userService;  

        public ClientsController(IClientService clientService, IMapper mapper, UserService userService)
        {
            _clientService = clientService ?? throw new ArgumentNullException(nameof(clientService));
            _userService = userService;
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();  
        }

        
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.RegisterUserAsync(viewModel.Email, viewModel.Password, viewModel.Name);
                if (result.Succeeded)
                {
                    await _clientService.RegisterClient(viewModel);
                    return RedirectToAction("Login", "Clients");
                }
                return View(viewModel);
            }

            return View(viewModel); 
        }

        
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(BusPortal.Web.Models.DTO.LoginViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var result = await _userService.LoginUserAsync(viewModel.Username, viewModel.Password, viewModel.RememberMe);

            var client = await _clientService.FindByName(viewModel.Username);

            SaveClientDataInCookie("ClientData", client);

            if (result.Succeeded && client.Admin == false)
            {
                return RedirectToAction("Add", "Bookings");
            }
            if (result.Succeeded && client.Admin == true)
            {
                return RedirectToAction("List", "Lines");
            }

            if (result.IsLockedOut)
            {
                ModelState.AddModelError(string.Empty, "This account has been locked out.");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _userService.LogoutUserAsync();

            Response.Cookies.Delete("ClientData");

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var user = await _userService.FindByEmailAsync(viewModel.Email);

            if (user != null)
            {
                await _userService.SendPasswordResetEmailAsync(viewModel.Email);
            }

            return View("ForgotPasswordConfirmation");
        }

        [HttpGet]
        public IActionResult ResetPassword(string token, string email)
        {
            if (token == null || email == null)
            {
                return BadRequest("Invalid password reset token.");
            }
            string decodedToken = Uri.UnescapeDataString(token);
            var model = new ResetPasswordViewModel { Token = decodedToken, Email = email };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var user = await _userService.FindByEmailAsync(viewModel.Email);

            if (user != null)
            {

                var isTokenValid = await _userService.VerifyPasswordResetTokenAsync(user, viewModel.Token);

                if (!isTokenValid)
                {
                    ModelState.AddModelError(string.Empty, "Invalid or expired token.");
                    return View(viewModel);
                }
                var result = await _userService.ResetUserPasswordAsync(user, viewModel.Token, viewModel.Password);

                if (result.Succeeded)
                {
                    return View("ResetPasswordConfirmation");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View("ResetPasswordConfirmation");
        }
        public void SaveClientDataInCookie(string clientName, BLL.Domain.Models.Client client)
        {
            var clientData = JsonConvert.SerializeObject(client);
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                Expires = DateTime.Now.AddDays(30)
            };

            Response.Cookies.Append(clientName, clientData, cookieOptions);
        }

    }
}
