using CarRental.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.BLL.Services.Scoped;

public interface ICarService
{
    void Add(Domain.Models.Car carAddDTO);
}

internal class CarService : ICarService
{
    private readonly ICarBrandService _carBrandService;
    public void Add(Car carAddDTO)
    {
        //var existsCb = _carBrandService.GetById(carAddDTO.BrandId);
    }
}
