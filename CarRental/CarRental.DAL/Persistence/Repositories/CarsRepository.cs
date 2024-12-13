using CarRental.DAL.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.DAL.Persistence.Repositories
{
    public interface ICarsRepository : _IBaseRepository<Car, int>
    {

    }
    internal class CarsRepository : _BaseRepository<Car, int>, ICarsRepository
    {
        public CarsRepository(CarRentalDbContext dbContext) : base(dbContext)
        {
        }
    }
}
