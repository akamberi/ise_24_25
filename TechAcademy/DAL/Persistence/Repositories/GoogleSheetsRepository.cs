using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Sheets.v4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace DAL.Persistence.Repositories
{
    public class GoogleSheetsRepository : IGoogleSheetsRepository
    {
        private readonly SheetsService _sheetsService;

        // Constructor
        public GoogleSheetsRepository(IConfiguration configuration)
        {
            var credentialPath = configuration["GoogleApi:CredentialsPath"];

            if (string.IsNullOrEmpty(credentialPath))
            {
                throw new InvalidOperationException("Google API credentials path is not configured. Ensure User Secrets are set.");
            }
            if (!File.Exists(credentialPath))
            {
                throw new FileNotFoundException($"Credential file not found at: {credentialPath}");
            }

            GoogleCredential credential;
            try
            {
                using (var stream = new FileStream(credentialPath, FileMode.Open, FileAccess.Read))
                {
                    credential = GoogleCredential.FromStream(stream)
                        .CreateScoped(SheetsService.Scope.Spreadsheets);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to load Google credentials. Ensure the JSON file is correct.", ex);
            }

            _sheetsService = new SheetsService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = "Google Sheets API Integration"
            });
        }

        public async Task<IList<IList<object>>> GetSheetDataAsync(string spreadsheetId, string range)
        {
            var request = _sheetsService.Spreadsheets.Values.Get(spreadsheetId, range);
            var response = await request.ExecuteAsync();
            return response.Values;
        }

        public async Task UpdateSheetDataAsync(string spreadsheetId, string range, IList<IList<object>> values)
        {
            var valueRange = new ValueRange { Values = values };
            var updateRequest = _sheetsService.Spreadsheets.Values.Update(valueRange, spreadsheetId, range);
            updateRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.RAW;
            await updateRequest.ExecuteAsync();
        }

        public async Task AppendToSheetAsync(string spreadsheetId, string range, IList<IList<object>> values)
        {
            var valueRange = new ValueRange { Values = values };
            var appendRequest = _sheetsService.Spreadsheets.Values.Append(valueRange, spreadsheetId, range);
            appendRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.RAW;
            await appendRequest.ExecuteAsync();
        }
    }
}
