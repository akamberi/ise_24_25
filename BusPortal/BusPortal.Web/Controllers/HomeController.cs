
using BusPortal.BLL.Domain.Models;
using BusPortal.Web.Models;
using BusPortal.Web.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using Stripe;
using System.Diagnostics;
using Microsoft.Extensions.Options;
using BusPortal.DAL.Persistence.Repositories;

namespace BusPortal.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly StripeSettings _stripeSettings;
        private readonly ILineRepository _lineRepository;

        public HomeController(ILogger<HomeController> logger, IOptions<StripeSettings> stripeSettings, ILineRepository lineRepository)
        {
            _logger = logger;
            _stripeSettings = stripeSettings.Value;
            _lineRepository = lineRepository;
        }

        public IActionResult Index()
        {
            var clientData = Request.Cookies["ClientData"];
            if (clientData != null)
            {
                return RedirectToAction("Add", "Bookings");
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

      

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}







