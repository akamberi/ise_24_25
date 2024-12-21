using ISEPay.DAL.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace ISEPay.DAL.Persistence.Repositories
{
    public interface IUsersRepository : _IBaseRepository<User, Guid>
    {
        IEnumerable<User> FilterByName(string name);
        User? GetByName(string name);
        User? GetByPhoneNumber(string phoneNumber); // Added method for phone number
        User? FindByEmail(string email); // Added method for email
        User? FindById(Guid id); // Added method for id
    }

    internal class UsersRepository : _BaseRepository<User, Guid>, IUsersRepository
    {
        public UsersRepository(ISEPayDBContext dbContext) : base(dbContext)
        {
        }

        public new User GetById(Guid id)
        {
            return base.GetById(id);
        }

        public User? FindById(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Id cannot be empty.", nameof(id));

            return _dbSet.FirstOrDefault(u => u.Id == id);
        }

        public IEnumerable<User> FilterByName(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Name cannot be null or empty.", nameof(name));

            return _dbSet.Where(x => EF.Functions.Like(x.FullName, $"%{name}%")).ToList();
        }

        public User? GetByName(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Name cannot be null or empty.", nameof(name));

            return _dbSet.FirstOrDefault(x => x.FullName == name);
        }

        public User? GetByPhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber))
                throw new ArgumentException("Phone number cannot be null or empty.", nameof(phoneNumber));

            return _dbSet.FirstOrDefault(u => u.PhoneNumber == phoneNumber);
        }

        // Implement the FindByEmail method
        public User? FindByEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                throw new ArgumentException("Email cannot be null or empty.", nameof(email));

            return _dbSet.FirstOrDefault(u => u.Email == email);
        }
    }
}
