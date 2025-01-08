using ISEPay.BLL.ISEPay.Domain.Models;
using ISEPay.Common.Enums;
using ISEPay.Domain;
using ISEPay.DAL.Persistence.Entities;
using ISEPay.DAL.Persistence.Repositories;
using Microsoft.AspNetCore.Identity;
using ISEPay.Domain.Models;
using System.Text;

namespace ISEPay.BLL.Services.Scoped
{
    public interface IUserService
    {
        void CreateUser(UserDTO user);

      //  void CreateUserWithRole(UserDTO user, RoleDto role);
        void CreateAdminUser(UserDTO user);
        void CreateAgentUser(UserDTO user);
        void DeleteUser(Guid userId);
        void ApproveUser(Guid userId); 
        void RejectUser(Guid userId);
        UserResponse GetUser(Guid id);
        List<UserResponse> GetAllUsers();

        List<UserResponse> GetUsersByStatus(UserStatus status);

        bool CheckEmail(string email);
        //void AddAdress(Guid userId , AddressDto addressDto);
    }

    internal class UserService : IUserService
    {
        private readonly IUsersRepository userRepository;
        private readonly IRolesRepository roleRepository; // Add roleRepository as a dependency
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IAddressRepository addressRepository;
        private readonly IOtpService otpService;
        private readonly IAccountService accountService;
        public UserService(IUsersRepository userRepository,
            IRolesRepository roleRepository, IPasswordHasher<User> passwordHasher,
            IAddressRepository addressRepository, IOtpService otpService,
            IAccountService accountService) //, IPasswordEncoder passwordEncoder)
        {
            this.userRepository = userRepository;
            this.roleRepository = roleRepository;
            _passwordHasher = passwordHasher;
            this.addressRepository = addressRepository;
            this.otpService = otpService;
            this.accountService = accountService;
                //this.passwordEncoder = passwordEncoder;
        }


        public void CreateUser(UserDTO user)
        {

            var existingUser = userRepository.GetByPhoneNumber(user.PhoneNumber);
            if (existingUser != null)
            {
                throw new InvalidOperationException("User cannot register with the same number");
            }
           /* bool otpValidation = otpService.ValidateOTP(user.Otp);
            if (!otpValidation)
            {
                throw new InvalidOperationException("Otp excpired");

            }*/
            // Create user entity and hash the password
            var userToAdd = new User
            {
                FullName = user.FullName,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                Status = Common.Enums.UserStatus.PENDING,
                Gender =user.Gender,
                CreatedAt = DateTime.Now
            };

            // Hash the password before saving it
            userToAdd.Password = _passwordHasher.HashPassword(userToAdd, user.password);

            // Optionally, assign a role (if required)
            var role = roleRepository.GetByName("User");
            userToAdd.RoleID = role.Id;

            userRepository.Add(userToAdd);
            userRepository.SaveChanges();
        }




        public void CreateAdminUser(UserDTO user)
        {
            var existingUser = userRepository.GetByPhoneNumber(user.PhoneNumber);
            if (existingUser != null)
            {
                throw new InvalidOperationException("User cannot register with the same number");
            }

            // Create user entity and hash the password
            var userToAdd = new User
            {
                FullName = user.FullName,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                Status = Common.Enums.UserStatus.APPROVED,
                CreatedAt = DateTime.Now
            };

            // Hash the password before saving it
            userToAdd.Password = _passwordHasher.HashPassword(userToAdd, user.password);

            // Optionally, assign a role (if required)
            var role = roleRepository.GetByName("Admin");
            userToAdd.RoleID = role.Id;

            userRepository.Add(userToAdd);
            userRepository.SaveChanges();
        }

        public void CreateAgentUser(UserDTO user)
        {
            var existingUser = userRepository.GetByPhoneNumber(user.PhoneNumber);
            if (existingUser != null)
            {
                throw new InvalidOperationException("User cannot register with the same number");
            }

            // Create user entity and hash the password
            var userToAdd = new User
            {
                FullName = user.FullName,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                Status = Common.Enums.UserStatus.PENDING,
                CreatedAt = DateTime.Now
            };

            // Hash the password before saving it
            userToAdd.Password = _passwordHasher.HashPassword(userToAdd, user.password);

            // Optionally, assign a role (if required)
            var role = roleRepository.GetByName("AGENT");
            userToAdd.RoleID = role.Id;

            userRepository.Add(userToAdd);
            userRepository.SaveChanges();
        }


        public bool CheckEmail(string email)
        {
            var existingEmail = userRepository.FindByEmail(email);
            if (existingEmail != null)
            {
                return false;
            }else
                return true;
        }



        public UserResponse GetUser(Guid userId)
        {
            // Retrieve the user from the repository using the provided userId
            var user = userRepository.FindById(userId);

            // If no user is found, throw an exception
            if (user == null)
            {
                throw new KeyNotFoundException("User does not exist");
            }

            // Map the user entity to a UserResponse DTO and return it
            var userResponse = new UserResponse(
                user.Id,                 // userID
                user.FullName,           // name
                user.Email,              // email
                user.PhoneNumber         // phoneNumber
            );

            return userResponse;  // Return the UserResponse
        }


        public List<UserResponse> GetAllUsers()
        {
            var users = userRepository.GetAll();

            if (users == null || !users.Any())
            {
                throw new KeyNotFoundException("No users found.");
            }

            var userDTOs = users.Select(user => new UserResponse(
                 user.Id,                  // userID
                 user.FullName,            // name
                 user.Email,               // email
                 user.PhoneNumber          // phoneNumber
             )).ToList();


            return userDTOs;
        }

        public List<UserResponse> GetUsersByStatus(UserStatus status)
        {
            // Retrieve all users from the repository
            var users = userRepository.GetAll();

            if (users == null || !users.Any())
            {
                throw new KeyNotFoundException("No users found");
            }

            // Filter users by the provided status
            var filteredUsers = users.Where(user => user.Status == status).ToList();

            if (filteredUsers == null || !filteredUsers.Any())
            {
                throw new KeyNotFoundException($"No users found with status {status}");
            }

            // Map filtered users to UserDTOs
            var userDTOs = filteredUsers.Select(user => new UserResponse(
                 user.Id,                  // userID
                 user.FullName,            // name
                 user.Email,               // email
                 user.PhoneNumber          // phoneNumber
             )).ToList();

            return userDTOs;
        }






        public void ApproveUser(Guid userId)
        {
            var user = userRepository.FindById(userId);
            if (user == null)
            {
                throw new KeyNotFoundException("User does not exist");
            }
            var userStatus = user.Status;
            if(userStatus == UserStatus.APPROVED)
            {
                throw new Exception("User is already approved");

            }
            if(userStatus == UserStatus.PENDING)
            {
                accountService.CreateDefaultAccount(userId);

            }
            user.Status = UserStatus.APPROVED;
            userRepository.SaveChanges();
        }


        public void RejectUser(Guid userId)
        {
            var user = userRepository.FindById(userId);
            if (user == null)
            {
                throw new KeyNotFoundException("User does not exist");
            }
            user.Status = UserStatus.REJECTED;
            userRepository.SaveChanges();
        }


        public void DeleteUser(Guid userId)
        {
            var user = userRepository.FindById(userId);
            if (user == null)
            {
                throw new KeyNotFoundException("User does not exist");
            }
            user.Status = UserStatus.Frozen;
            userRepository.SaveChanges();
        }




          // to be developed 
    /*     public void AddAddress(Guid userId, AddressDto addressDto)
        {
            // Retrieve the user by Id
            var user = userRepository.FindById(userId);

            // Check if the user exists
            if (user == null)
            {
                throw new KeyNotFoundException("User does not exist");
            }

            // Check if an address already exists for the user
            var existingAddress = addressRepository
                .FirstOrDefault(a => a.UserId == userId && a.Country == addressDto.Country &&
                                     a.City == addressDto.City && a.Street == addressDto.Street &&
                                     a.Zipcode == addressDto.Zipcode);

            if (existingAddress != null)
            {
                throw new InvalidOperationException("This address already exists for the user.");
            }

            // Map AddressDto to Address entity
            var address = new Address
            {
                Country = addressDto.Country,
                City = addressDto.City,
                Street = addressDto.Street,
                Zipcode = addressDto.Zipcode
            };

            // Add the new address to the address repository
            addressRepository.Add(address);

            // Save changes to the address table to get the generated AddressId
            addressRepository.SaveChanges();

            // Update the user's AddressId to link to the newly created address
            user.AdressID = address.Id;  // Assign the AddressId to the user

            // Save the changes to the user
            userRepository.SaveChanges();
        }*/


    }
}
