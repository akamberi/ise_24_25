using ISEPay.DAL.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ISEPay.DAL.Persistence.Repositories
{
    
    public interface IAddressRepository : _IBaseRepository<Address, Guid>
    {
        Address? GetById(Guid userId); 
        IEnumerable<Address> FilterByCity(string city); 
    }

    internal class AddressRepository : _BaseRepository<Address, Guid>, IAddressRepository
    {
        private readonly ISEPayDBContext _context;

        
        
        public AddressRepository(ISEPayDBContext dbContext) : base(dbContext)
        {
            _context = dbContext;

        }

        public new Address GetById(Guid id)
        {
            return base.GetById(id);
        }

        public IEnumerable<Address> FilterByCity(string city)
        {
            if (string.IsNullOrEmpty(city))
                throw new ArgumentException("City cannot be null or empty.", nameof(city));

            return _dbSet.Where(x => EF.Functions.Like(x.City, $"%{city}%")).ToList();
        }

        public Address? GetByCity(string city)
        {
            if (string.IsNullOrEmpty(city))
                throw new ArgumentException("City cannot be null or empty.", nameof(city));

            return _dbSet.FirstOrDefault(x => x.City == city);
        }
    }
}
