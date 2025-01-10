#nullable disable

using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.AspNetCore.Authentication;

namespace CSDproject.Areas.Identity.Pages.Account
{
    public class ConfirmEmailModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public ConfirmEmailModel(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [TempData]
        public string StatusMessage { get; set; }

        // Properties to store the userId and code received in the URL
        public string UserId { get; set; }
        public string Code { get; set; }

        public async Task<IActionResult> OnGetAsync(string userId, string code)
        {
            // Validate input parameters
            if (userId == null || code == null)
            {
                return RedirectToPage("/Index");
            }

            // Find the user by their ID
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                // If the user is not found, return an error message
                return NotFound($"Unable to load user with ID '{userId}'.");
            }

            try
            {
                // Decode the confirmation code
                code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            }
            catch (Exception)
            {
                // If there is an issue decoding the code, show an error message
                StatusMessage = "Error decoding the confirmation code.";
                return Page();
            }

            // Confirm the user's email using the decoded code
            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
            {
                // Sign the user in after email confirmation
                await _signInManager.SignInAsync(user, isPersistent: false);

                StatusMessage = "Thank you for confirming your email. You are now signed in.";
                return RedirectToPage("/Index"); // Redirect to home or any page after sign-in
            }
            else
            {
                StatusMessage = "Error confirming your email.";
                return Page();
            }
        }
    }
}
