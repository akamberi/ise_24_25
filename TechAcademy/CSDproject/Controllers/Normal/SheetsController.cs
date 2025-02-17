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
            string spreadsheetId = "1YhmxdBJ3apIPir4ETzTQenaa9bLgqCC8ar-BIVmOOQ0";
            string range = "'Form Responses 1'!A:E";
            var data = await _googleSheetsService.GetData(spreadsheetId, range);

            // Pass the data to the view or handle it as needed
            return View(data);
        }
       
    }
}
    
