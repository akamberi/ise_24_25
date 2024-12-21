using ISEPay.DAL.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ISEPay.DAL.Persistence.Repositories
{
    public interface IPermissionRepository : _IBaseRepository<Permission, Guid>
    {
        IEnumerable<Permission> FilterByName(string name);
        Permission? GetByName(string name);
    }

    internal class PermissionRepository : _BaseRepository<Permission, Guid>, IPermissionRepository
    {
        public PermissionRepository(ISEPayDBContext dbContext) : base(dbContext)
        {
        }

        public new Permission GetById(Guid id)
        {
            return base.GetById(id);
        }

        public IEnumerable<Permission> FilterByName(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Name cannot be null or empty.", nameof(name));

            return _dbSet.Where(x => EF.Functions.Like(x.Name, $"%{name}%")).ToList();
        }

        public Permission? GetByName(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Name cannot be null or empty.", nameof(name));

            return _dbSet.FirstOrDefault(x => x.Name == name);
        }
    }
}
