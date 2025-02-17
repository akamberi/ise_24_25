using Microsoft.AspNetCore.Mvc;
using BusPortal.BLL.Services.Interfaces;

public class DashboardController : Controller
{
    private readonly IBookingServices _bookingsService;
    private readonly ILinesService _lineService;

    public DashboardController(IBookingServices bookingService, ILinesService lineService)
    {
        _bookingsService = bookingService;
        _lineService = lineService;
    }

    public async Task<IActionResult> Index()
    {
        var totalBookings = await _bookingsService.GetTotalBookingsAsync();
        var totalRevenue = await _bookingsService.GetTotalRevenueAsync();
        var topRoutes = await _bookingsService.GetTopRoutesAsync();
        var dailyBookings = await _bookingsService.GetDailyBookingsAsync();

        ViewBag.TotalBookings = totalBookings;
        ViewBag.TotalRevenue = totalRevenue;
        ViewBag.TopRoutes = topRoutes;
        ViewBag.DailyBookings = dailyBookings;

        return View();
    }
}