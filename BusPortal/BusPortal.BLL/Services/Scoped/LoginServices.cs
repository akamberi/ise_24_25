//using BusPortal.DAL.Persistence;
//using BusPortal.Common.Models;
//using BusPortal.BLL.Services.Interfaces;
//using Microsoft.AspNetCore.Identity;
//using BusPortal.Domain.Models;
//using System.Linq;

//namespace BusPortal.BLL.Services.Scoped
//{
//    public class LoginServices : ILoginServices
//    {
//        private readonly DALDbContext _dbContext;
//        private readonly PasswordHasher<Client> _passwordHasher;

//        public LoginServices(DALDbContext dbContext)
//        {
//            _dbContext = dbContext;
//            _passwordHasher = new PasswordHasher<Client>(); // Using PasswordHasher for password hashing
//        }

//        public bool AuthenticateUser(LoginViewModel viewModel)
//        {
//            var user = _dbContext.Clients.FirstOrDefault(c => c.Name == viewModel.Username);

//            if (user != null)
//            {
//                // Compare the hashed password
//                var passwordVerificationResult = _passwordHasher.VerifyHashedPassword(user, user.Password, viewModel.Password);

//                if (passwordVerificationResult == PasswordVerificationResult.Success)
//                {
//                    return true;
//                }
//            }

//            return false;
//        }
//    }
//}
