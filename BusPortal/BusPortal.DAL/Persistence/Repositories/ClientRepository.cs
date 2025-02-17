using System;
using System.Linq;
using System.Threading.Tasks;
using BusPortal.DAL.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusPortal.DAL.Persistence.Repositories
{
    public interface IClientRepository : _IBaseRepository<Client, Guid>
    {
        Client? FindByName(string name);
    }

    internal class ClientRepository : _BaseRepository<Client, Guid>, IClientRepository
    {
        private readonly DALDbContext _dbContext;
        public ClientRepository(DALDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public Client? FindByName(string name)
        {
            return _dbContext.Set<Client>().FirstOrDefault(c => c.Name == name);
        }
    }
}
