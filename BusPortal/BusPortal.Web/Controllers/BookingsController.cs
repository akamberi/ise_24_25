using BusPortal.BLL.Services.Interfaces;
using BusPortal.Common.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using BusPortal.BLL.Domain.Models;
using Stripe.Checkout;
using Stripe;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Mono.TextTemplating;

namespace BusPortal.Web.Controllers
{
    public class BookingsController : Controller
    {
        private readonly IBookingServices _bookingServices;
        private readonly ILinesService _linesService;
        private readonly StripeSettings _stripeSettings;

        public BookingsController(IBookingServices bookingServices, ILinesService linesService, IOptions<StripeSettings> stripeSettings)
        {
            _bookingServices = bookingServices;
            _linesService = linesService;
            _stripeSettings = stripeSettings.Value;
        }
        [HttpGet]
        public async Task<IActionResult> GetOccupiedSeats(string lineId, string dateSelected, string timeSelected)
        {
            var availableSeats = await _bookingServices.GetOccupiedSeatsAsync(lineId, dateSelected, timeSelected);
            return Json(availableSeats);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var clientData = Request.Cookies["ClientData"];
            if (clientData == null)
            {
                return RedirectToAction("Login", "Clients");
            }
            var startCities = await _linesService.GetAllStartCitiesAsync();  // Using _linesService
            ViewBag.StartCities = new SelectList(startCities);


            return View();
        }

     
        [HttpGet]
        public async Task<IActionResult> GetDestinationCities(string startCity)
        {
            var destinationCities = await _linesService.GetDestinationCitiesForStartCityAsync(startCity);  // Using _linesService
            return Json(destinationCities);
        }


        public async Task<IActionResult> GetDepartureTimes(string startCity, string destinationCity)
        {
            var line = await _linesService.GetLineByRouteAsync(startCity, destinationCity);
            if (line != null && !string.IsNullOrEmpty(line.DepartureTimes))
            {
                // Split semicolon-separated times into an array
                var times = line.DepartureTimes.Split(';', StringSplitOptions.RemoveEmptyEntries);
                return Json(times); // Returns JSON array like ["12:00", "13:00"]
            }
            return Json(Array.Empty<string>()); // Return empty array
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddBookingViewModel model)
        {

            return View(model);
        }
        public async Task<IActionResult> Success(string session_id)
        {

            if (string.IsNullOrEmpty(session_id))
            {
                return BadRequest("Session ID is missing.");
            }

            var service = new SessionService();
            Session session = service.Get(session_id);

        // Extract metadata
            var price = session.Metadata.ContainsKey("price") ? session.Metadata["price"] : "Unknown";
            var lineId = session.Metadata.ContainsKey("lineId") ? session.Metadata["lineId"] : "Unknown";
            var departureDate = session.Metadata.ContainsKey("departureDate") ? session.Metadata["departureDate"] : "Unknown";
            var departureTime = session.Metadata.ContainsKey("departureTime") ? session.Metadata["departureTime"] : "Unknown";
            var seatNrs = session.Metadata.ContainsKey("seatNrs") ? session.Metadata["seatNrs"] : "Unknown";
            var clientData = Request.Cookies["ClientData"];
            if (clientData == null)
            {
                return RedirectToAction("Index", "Home");
            }
            dynamic ?clientDataParsed = JsonConvert.DeserializeObject(clientData);
            var clientId = Guid.Parse(clientDataParsed.Id.Value);

            var dateTime = DateTime.Parse(departureDate + " " + departureTime);
            var parsedLineId = Guid.Parse(lineId);
            decimal parsedPrice = decimal.Parse(price);

            //add
            var result = _bookingServices.AddBooking(clientId,  parsedLineId,  seatNrs,  dateTime,  parsedPrice);
            var line =  await _linesService.GetLineByIdAsync(parsedLineId);

                ViewBag.success = new { clientName= clientDataParsed.Name, price, departureDate, departureTime, seatNrs, startingCity=line.StartCity,destinationCity=line.DestinationCity };

            return View();
        }

        public IActionResult Cancel()
        {

            return View();
        }
        public async Task<IActionResult> CreateCheckoutSession(string startCity, string destinationCity, decimal price, string departureDate, string departureTime, string lineId, string seatNrs)
        {
            try
            {
                var currency = "usd";
                var successUrl = Url.Action("Success", "Bookings", null, Request.Scheme);
                successUrl += "?session_id={CHECKOUT_SESSION_ID}";
                var cancelUrl = Url.Action("Cancel", "Bookings", null, Request.Scheme);
                StripeConfiguration.ApiKey = _stripeSettings.SecretKey;

                var options = new SessionCreateOptions
                {
                    PaymentMethodTypes = new List<string> { "card" },
                    LineItems = new List<SessionLineItemOptions>
            {
                new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = currency,
                        UnitAmount = (long)(price * 100),
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = $"Bus Ticket from {startCity} to {destinationCity}"
                        }
                    },
                    Quantity = 1
                }
            },
                    Mode = "payment",
                    SuccessUrl = successUrl,
                    CancelUrl = cancelUrl,
                    Metadata = new Dictionary<string, string>
                        {
                            { "startCity", startCity},
                            { "destinationCity", destinationCity },
                            { "price", price.ToString()},
                            { "departureDate", departureDate },
                            { "departureTime", departureTime },
                            { "lineId", lineId },
                            { "seatNrs", seatNrs}
                        }
                };

                var service = new SessionService();
                var session = service.Create(options);

                return Redirect(session.Url);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
        }

    }
}
