using CarRental.BLL.Services.Scoped;
using CarRental.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Controllers;

public class CarBrandsController : Controller
{
    private readonly ICarBrandService _carBrandService;
    public CarBrandsController(ICarBrandService carBrandService)
    {
        _carBrandService = carBrandService;
    }

    public IActionResult Index()
    {
        var carBrands = _carBrandService.GetCarBrands()
                                       .Select(x => new CarBrandDTO
                                       {
                                           Id = x.Id,
                                           Name = x.Name,
                                       }).ToList()
            ;
        return View(carBrands);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View(new CarBrandAddDTO());
    }

    [HttpPost]
    public IActionResult Create(CarBrandAddDTO model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        _carBrandService.AddBrand(new Domain.Models.CarBrand
        {
            LogoPath = "",
            Name = model.Name
        });
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Edit(int? id)
    {
        var carBrand = _carBrandService.GetById(id.Value);

        return View(new CarBrandEditDTO
        {
            Name = carBrand.Name,
            Thumbnail = carBrand.LogoPath
        });
    }

    [HttpPost]
    public IActionResult Edit(int id, CarBrandEditDTO model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        _carBrandService.EditBrand(new Domain.Models.CarBrand
        {
            LogoPath = model.Thumbnail,
            Name = model.Name,
            Id = id
        });

        return RedirectToAction(nameof(Index));
    }
}
