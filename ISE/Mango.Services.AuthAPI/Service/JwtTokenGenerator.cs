using Mango.Services.AuthAPI.Models;  // Ensure these models contain necessary properties like Email, Id, and UserName
using Mango.Services.AuthAPI.Service.IService;  // Ensure the interface IJwtTokenGenerator is correctly defined
using Microsoft.Extensions.Options;  // For injecting options
using Microsoft.IdentityModel.Tokens;  // For token handling
using System.IdentityModel.Tokens.Jwt;  // For JWT handling
using System.Security.Claims;  // For claims handling
using System.Text;  // For encoding

namespace Mango.Services.AuthAPI.Service
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly JwtOptions _jwtOptions;

        // Constructor to inject JwtOptions
        public JwtTokenGenerator(IOptions<JwtOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
        }

        // Method to generate token
        public string GenerateToken(ApplicationUser applicationUser, IEnumerable<string> roles)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtOptions.Secret);  // Ensure _jwtOptions.Secret is properly configured

            // List of claims
            var claimList = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, applicationUser.Email),
                new Claim(JwtRegisteredClaimNames.Sub, applicationUser.Id),
                new Claim(JwtRegisteredClaimNames.Name, applicationUser.UserName)
            };

            // Adding roles to claims
            claimList.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            // Token descriptor with all necessary information
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = _jwtOptions.Audience,
                Issuer = _jwtOptions.Issuer,
                Subject = new ClaimsIdentity(claimList),
                Expires = DateTime.UtcNow.AddDays(7),  // Token expiration time
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)  // Signing credentials
            };

            // Create and return the token
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
