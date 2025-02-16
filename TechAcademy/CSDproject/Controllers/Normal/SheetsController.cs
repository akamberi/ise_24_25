using BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace CSDproject.Controllers.Normal
{
    public class SheetsController : Controller
    {
        private readonly GoogleSheetsService _googleSheetsService;

        public SheetsController(GoogleSheetsService googleSheetsService)
        {
            _googleSheetsService = googleSheetsService;
        }

        public async Task<IActionResult> Index()
        {
            // Example usage
            string spreadsheetId = "1nqF8WoWHiDvGGoeeFfRy_0-bdV7m5to6qopzwkBm-rw";
            string range = "'Form Responses 1'!A:E";
            var data = await _googleSheetsService.GetData(spreadsheetId, range);

            // Pass the data to the view or handle it as needed
            return View(data);
        }
    }
}
