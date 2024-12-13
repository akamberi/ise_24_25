using CarRental.DAL.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarRental.DAL.Persistence.Repositories;

public interface ICarBrandsRepository : _IBaseRepository<CarBrand, int>
{
    IEnumerable<CarBrand> FilterByName(string name);
    CarBrand? GetByName(string name);
}

internal class CarBrandsRepository : _BaseRepository<CarBrand, int>, ICarBrandsRepository
{
    public CarBrandsRepository(CarRentalDbContext dbContext) : base(dbContext)
    {
    }
    public new CarBrand GetById(int id)
    {
        return base.GetById(id);
    }
    public IEnumerable<CarBrand> FilterByName(string name)
    {
        return _dbSet.Where(x => x.Name.ToLower().Contains(name.ToLower())).ToList();
    }

    public CarBrand? GetByName(string name)
    {
        return _dbSet.FirstOrDefault(x => x.Name.ToLower() == name.ToLower());
    }
}
