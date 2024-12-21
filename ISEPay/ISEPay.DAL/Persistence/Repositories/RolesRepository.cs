using ISEPay.DAL.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ISEPay.DAL.Persistence.Repositories
{
    public interface IRolesRepository : _IBaseRepository<Role, Guid>
    {
        IEnumerable<Role> FilterByName(string name);
        Role? GetByName(string name);
    }

    internal class RolesRepository : _BaseRepository<Role, Guid>, IRolesRepository
    {
        public RolesRepository(ISEPayDBContext dbContext) : base(dbContext)
        {
        }

        public new Role GetById(Guid id)
        {
            return base.GetById(id);
        }

        public IEnumerable<Role> FilterByName(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Name cannot be null or empty.", nameof(name));

            return _dbSet.Where(x => EF.Functions.Like(x.Name, $"%{name}%")).ToList();
        }

        public Role? GetByName(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Name cannot be null or empty.", nameof(name));

            return _dbSet.FirstOrDefault(x => x.Name == name);
        }
    }
}
