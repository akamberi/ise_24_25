using CarRental.Common.Exceptions;
using CarRental.DAL.Persistence.Repositories;
using CarRental.Domain.Models;

namespace CarRental.BLL.Services.Scoped;

public interface ICarBrandService
{
    IEnumerable<CarBrand> GetCarBrands();
    void AddBrand(Domain.Models.CarBrand carBrand);
    CarBrand GetById(int id);
    void EditBrand(Domain.Models.CarBrand carBrand);
    void RemoveBrand(int id);
}

internal class CarBrandService : ICarBrandService
{
    private readonly ICarBrandsRepository carBrandsRepository;
    public CarBrandService(ICarBrandsRepository repository)
    {
        carBrandsRepository = repository;
    }

    public void AddBrand(CarBrand carBrand)
    {
        var existingBrand = carBrandsRepository.GetByName(carBrand.Name);
        if (existingBrand != null)
        {
            throw new CarRentalException("Brand already exists");
        }
        var carBrandToAdd = new CarRental.DAL.Persistence.Entities.CarBrand
        {
            Name = carBrand.Name,
            Logo = carBrand.LogoPath
        };
        carBrandsRepository.Add(carBrandToAdd);
        carBrandsRepository.SaveChanges();
    }

    public IEnumerable<CarBrand> GetCarBrands()
    {
        var carBrands = carBrandsRepository.GetAll()
                                           .ToList();
        return carBrands.Select(brand => new CarBrand
        {
            Id = brand.Id,
            Name = brand.Name,
            LogoPath = brand.Logo
        }).ToList();
    }

    public CarBrand GetById(int id)
    {
        var carBrand = carBrandsRepository.GetById(id);
        return
            new CarBrand
            {
                Id = carBrand.Id,
                Name = carBrand.Name,
                LogoPath = carBrand.Logo
            };
    }

    public void EditBrand(CarBrand carBrand)
    {
        var existingBrand = carBrandsRepository.GetById(carBrand.Id);

        if (existingBrand == null)
        {
            throw new CarRentalException("Brand does not exist");
        }

        existingBrand.Name = carBrand.Name;
        carBrandsRepository.SaveChanges();
    }

    public void RemoveBrand(int id)
    {

    }
}
