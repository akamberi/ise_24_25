using BusPortal.DAL.Persistence.Entities;
using BusPortal.BLL.Services;
using BusPortal.DAL.Persistence.Repositories;
using Microsoft.AspNetCore.Identity;

namespace BusPortal.BLL.Services.Scoped
{
    public class UserService
    {
        private readonly UserRepository _userRepository;
        private readonly EmailService _emailService;

        public UserService(UserRepository userRepository, EmailService emailService)
        {
            _userRepository = userRepository;
            _emailService = emailService;
        }

        public async Task<IdentityResult> RegisterUserAsync(string email, string password, string name)
        {
            var existingUser = await _userRepository.FindByEmailAsync(email);
            if (existingUser != null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "Email is already registered." });
            }

            var user = new IdentityUser
            {
                Email = email,
                UserName = name
            };

            return await _userRepository.CreateUserAsync(user, password);
        }

        public async Task<IdentityUser> FindByEmailAsync(string email)
        {
            return await _userRepository.FindByEmailAsync(email);
        }

        public async Task<SignInResult> LoginUserAsync(string email, string password, bool rememberMe)
        {
            return await _userRepository.LoginAsync(email, password, rememberMe);
        }

        public async Task LogoutUserAsync()
        {
            await _userRepository.LogoutAsync();
        }
        public async Task<string> GeneratePasswordResetTokenAsync(IdentityUser user)
        {
            return await _userRepository.GeneratePasswordResetTokenAsync(user);
        }

        public async Task<IdentityResult> ResetUserPasswordAsync(IdentityUser user, string token, string newPassword)
        {
            return await _userRepository.ResetPasswordAsync(user, token, newPassword);
        }

        public async Task SendPasswordResetEmailAsync(string email)
        {
            var user = await _userRepository.FindByEmailAsync(email);

            if (user != null)
            {
                var token = await _userRepository.GeneratePasswordResetTokenAsync(user);
                var resetLink = $"http://localhost:5078/Clients/ResetPassword?token={Uri.EscapeDataString(token)}&email={Uri.EscapeDataString(email)}";

                await _emailService.SendEmailAsync(
                    email,
                    "Reset Your Password",
                    $"<p>Click <a href='{resetLink}'>here</a> to reset your password.</p>"
                );
            }
        }
        public async Task<bool> VerifyPasswordResetTokenAsync(IdentityUser user, string token)
        {
            return await _userRepository.VerifyPasswordResetTokenAsync(user, token);
        }
    }
}
