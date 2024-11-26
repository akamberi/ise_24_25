using CarRental.DAL.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarRental.DAL.Persistence.Repositories;

public class CarBrandViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public interface ICarBrandsRepository : _IBaseRepository<CarBrand, int>
{
    IEnumerable<CarBrand> FilterByName(string name);
    IEnumerable<CarBrandViewModel> GetAllBrands();
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

    public IEnumerable<CarBrandViewModel> GetAllBrands()
    {
        return _dbSet.Select(x => new CarBrandViewModel
        {
            Id = x.Id,
            Name = x.Name
        }).ToList();
    }
}
