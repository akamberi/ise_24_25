using ISEPay.BLL.ISEPay.Domain.Models;
using ISEPay.Common.Enums;
using ISEPay.DAL.Persistence.Entities;
using ISEPay.DAL.Persistence.Repositories;

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

        UserDTO GetUser(Guid id);
        List<UserDTO> GetAllUsers();

        List<UserDTO> GetUsersByStatus(UserStatus status);

        bool CheckEmail(string email);
        //void AddAdress(Guid userId , AddressDto addressDto);
    }

    internal class UserService : IUserService
    {
        private readonly IUsersRepository userRepository;
        private readonly IRolesRepository roleRepository; // Add roleRepository as a dependency
       // private readonly IPasswordEncoder passwordEncoder; // Assuming an encoder interface

        public UserService(IUsersRepository userRepository, IRolesRepository roleRepository) //, IPasswordEncoder passwordEncoder)
        {
            this.userRepository = userRepository;
            this.roleRepository = roleRepository;
            //this.passwordEncoder = passwordEncoder;
        }

        public void CreateUser(UserDTO user)
        {
            var existingUser = userRepository.GetByPhoneNumber(user.PhoneNumber);
            if (existingUser != null)
            {
               // Console.WriteLine("You cannot register with the same number");
               
                throw new InvalidOperationException("User cannot register with the same number");
            }

            var userToAdd = new User
            {
                FullName = user.FullName,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                Password =  user.password,    //  passwordEncoder.Encode(user.Password), // do perdorim nje encoder per kte
                Status = Common.Enums.UserStatus.PENDING,
                CreatedAt= DateTime.Now
            };

            // Optionally, assign a role (if required)
            var role = roleRepository.GetByName("User");
            userToAdd.RoleID= role.Id;

            userRepository.Add(userToAdd);
            userRepository.SaveChanges();
        }




        public void CreateAdminUser(UserDTO user)
        {
            var existingUser = userRepository.GetByPhoneNumber(user.PhoneNumber);
            if (existingUser != null)
            {
                // Console.WriteLine("You cannot register with the same number");

                throw new InvalidOperationException("User cannot register with the same number");
            }

            var userToAdd = new User
            {
                FullName = user.FullName,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                Password = user.password,    //  passwordEncoder.Encode(user.Password), // do perdorim nje encoder per kte
                Status = Common.Enums.UserStatus.APPROVED,
                CreatedAt = DateTime.Now

            };

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
                // Console.WriteLine("You cannot register with the same number");

                throw new InvalidOperationException("User cannot register with the same number");
            }

            var userToAdd = new User
            {
                FullName = user.FullName,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                Password = user.password,    //  passwordEncoder.Encode(user.Password), // do perdorim nje encoder per kte
                Status = Common.Enums.UserStatus.PENDING,
                CreatedAt = DateTime.Now
            };

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



        public UserDTO GetUser(Guid userId)
        {
            var user = userRepository.FindById(userId);
            if( user == null)
            {
                throw new KeyNotFoundException("User does not exist");
            }

            return new UserDTO
            {
                FullName = user.FullName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            };
        }

        public List<UserDTO> GetAllUsers()
        {
            var users = userRepository.GetAll(); 

            if (users == null || !users.Any())
            {
                throw new KeyNotFoundException("No users found");
            }

            var userDTOs = users.Select(user => new UserDTO
            {
                FullName = user.FullName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            }).ToList();

            return userDTOs;
        }
        public List<UserDTO> GetUsersByStatus(UserStatus status)
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
            var userDTOs = filteredUsers.Select(user => new UserDTO
            {
                FullName = user.FullName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            }).ToList();

            return userDTOs;
        }






        public void ApproveUser(Guid userId)
        {
            var user = userRepository.FindById(userId);
            if (user == null)
            {
                throw new KeyNotFoundException("User does not exist");
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
            user.Status = UserStatus.BLACKLIST;
            userRepository.SaveChanges();
        }




        // logjika per te shtuar adresen e nje useri duhet test kur te kemi lidhjen me db

        /* public void AddAddress(Guid userId, AddressDto addressDto)
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

             // Associate address with user
             user.Address = address;

             // Save changes to the database
             userRepository.SaveChanges();
         }
        */
    }
}
