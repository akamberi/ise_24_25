using CarRental.BLL.Services.Scoped;
using CarRental.DTO.Models;
using CarRental.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Controllers
{
    public class CarsController : Controller
    {
        private readonly ICarBrandService _carBrandService;
        public CarsController(ICarBrandService carBrandService)
        {
            _carBrandService = carBrandService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            //ViewData["Brands"] = _carBrandService.GetCarBrands();
            ViewBag.Brands = _carBrandService.GetCarBrands()
                                             .Select(x => new OptionListElement<int>
                                             {
                                                 Id = x.Id,
                                                 Name = x.Name
                                             }).ToList()
            ;
            return View(new CarAddDTO());
        }
    }
}
