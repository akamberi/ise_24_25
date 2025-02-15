using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using ISEPay.BLL.ISEPay.Domain.Models;
using ISEPay.DAL.Persistence.Entities;
using ISEPay.DAL.Persistence.Repositories;
using Microsoft.AspNetCore.Http;
public interface IAuthenticationService
{
    AuthenticationResponse Authenticate(AuthenticationRequest authenticationRequest);
    AuthenticationResponse AuthenticateAdmin(AuthenticationRequest authenticationRequest);
    AuthenticationResponse AuthenticateAgent(AuthenticationRequest authenticationRequest);
}

internal class AuthenticationService : IAuthenticationService
{
    private readonly IConfiguration _configuration;
    private readonly IUsersRepository _userRepository; // Replace with your actual repository interface
    private readonly IRolesRepository _roleRepository; // Replace with your actual role repository interface
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IHttpContextAccessor _httpContextAccessor;


    public AuthenticationService(
        IConfiguration configuration,
        IUsersRepository userRepository,
        IRolesRepository roleRepository,
        IPasswordHasher<User> passwordHasher,
        IHttpContextAccessor httpContextAccessor)
    {
        _configuration = configuration;
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _passwordHasher = passwordHasher;
        _httpContextAccessor = httpContextAccessor;
    }

    public AuthenticationResponse Authenticate(AuthenticationRequest authenticationRequest)
    {
        // Validate inputs
        if (string.IsNullOrWhiteSpace(authenticationRequest.Email) ||
            string.IsNullOrWhiteSpace(authenticationRequest.Password))
        {
            throw new ArgumentException("Email and password are required.");
        }

        // Fetch user from repository based on email
        var user = _userRepository.GetAll().FirstOrDefault(u => u.Email == authenticationRequest.Email);

        if (user == null)
        {
            throw new UnauthorizedAccessException("Invalid email or password.");
        }

        // Verify password
        var result = _passwordHasher.VerifyHashedPassword(user, user.Password, authenticationRequest.Password);
        if (result != PasswordVerificationResult.Success)
        {
            throw new UnauthorizedAccessException("Invalid email or password.");
        }

        // Retrieve role name based on RoleId
        var role = _roleRepository.FindById(user.RoleID); // Ensure this method exists in your role repository
        if (role == null)
        {
            throw new Exception("Role not found.");
        }

        // Generate a JWT token
        var token = GenerateToken(user.FullName, role.Name);

        // Return the AuthenticationResponse with user details and token
        return new AuthenticationResponse
        {
            UserID = user.Id,
            Name = user.FullName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            AccessToken = token
        };
    }

    public AuthenticationResponse AuthenticateAdmin(AuthenticationRequest authenticationRequest)
    {
        // Validate inputs
        if (string.IsNullOrWhiteSpace(authenticationRequest.Email) ||
            string.IsNullOrWhiteSpace(authenticationRequest.Password))
        {
            throw new ArgumentException("Email and password are required.");
        }

        // Fetch user from repository based on email
        var user = _userRepository.GetAll().FirstOrDefault(u => u.Email == authenticationRequest.Email);

        if (user == null)
        {
            throw new UnauthorizedAccessException("Invalid email or password.");
        }

        // Verify password
        var result = _passwordHasher.VerifyHashedPassword(user, user.Password, authenticationRequest.Password);
        if (result != PasswordVerificationResult.Success)
        {
            throw new UnauthorizedAccessException("Invalid email or password.");
        }

        // Retrieve role name based on RoleId
        var role = _roleRepository.FindById(user.RoleID);
        if (role == null)
        {
            throw new Exception("Role not found.");
        }

        // Ensure user is an Admin
        if (!role.Name.Equals("Admin", StringComparison.OrdinalIgnoreCase))
        {
            throw new UnauthorizedAccessException("Access denied. Only admins can log in.");
        }

        // Generate a JWT token
        var token = GenerateToken(user.FullName, role.Name);

        // Return the AuthenticationResponse with user details and token
        return new AuthenticationResponse
        {
            UserID = user.Id,
            Name = user.FullName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            AccessToken = token
        };
    }


    public AuthenticationResponse AuthenticateAgent(AuthenticationRequest authenticationRequest)
    {
        // Validate inputs
        if (string.IsNullOrWhiteSpace(authenticationRequest.Email) ||
            string.IsNullOrWhiteSpace(authenticationRequest.Password))
        {
            throw new ArgumentException("Email and password are required.");
        }

        // Fetch user from repository based on email
        var user = _userRepository.GetAll().FirstOrDefault(u => u.Email == authenticationRequest.Email);

        if (user == null)
        {
            throw new UnauthorizedAccessException("Invalid email or password.");
        }

        // Verify password
        var result = _passwordHasher.VerifyHashedPassword(user, user.Password, authenticationRequest.Password);
        if (result != PasswordVerificationResult.Success)
        {
            throw new UnauthorizedAccessException("Invalid email or password.");
        }

        // Retrieve role name based on RoleId
        var role = _roleRepository.FindById(user.RoleID);
        if (role == null)
        {
            throw new Exception("Role not found.");
        }

        // Ensure user is an Agent
        if (!role.Name.Equals("Agent", StringComparison.OrdinalIgnoreCase))
        {
            throw new UnauthorizedAccessException("Access denied. Only agents can log in.");
        }

        // Generate a JWT token
        var token = GenerateToken(user.FullName, role.Name);

        // Return the AuthenticationResponse with user details and token
        return new AuthenticationResponse
        {
            UserID = user.Id,
            Name = user.FullName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            AccessToken = token
        };
    }

    private string GenerateToken(string username, string role)
    {
        var jwtSettings = _configuration.GetSection("Jwt");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.Role, role)
        };

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: jwtSettings["Issuer"],
            audience: jwtSettings["Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(int.Parse(jwtSettings["ExpiresInMinutes"])),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public User GetCurrentUser()
    {
        var userPrincipal = _httpContextAccessor.HttpContext?.User;

        if (userPrincipal == null || !userPrincipal.Identity.IsAuthenticated)
        {
            throw new UnauthorizedAccessException("User not authenticated");
        }

        var email = userPrincipal.FindFirstValue(ClaimTypes.Email); // Extract email claim

        if (string.IsNullOrEmpty(email))
        {
            throw new UnauthorizedAccessException("User email not found in token");
        }

        return _userRepository.GetAll().FirstOrDefault(u => u.Email == email)
               ?? throw new UnauthorizedAccessException("User not found in the system");
    }
}
