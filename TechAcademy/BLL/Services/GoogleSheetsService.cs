using DAL.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class GoogleSheetsService
    {
        private readonly IGoogleSheetsRepository _googleSheetsRepository;

        public GoogleSheetsService(IGoogleSheetsRepository googleSheetsRepository)
        {
            _googleSheetsRepository = googleSheetsRepository;
        }

        public async Task<IList<IList<object>>> GetData(string spreadsheetId, string range)
        {
            return await _googleSheetsRepository.GetSheetDataAsync(spreadsheetId, range);
        }

        public async Task UpdateData(string spreadsheetId, string range, IList<IList<object>> values)
        {
            await _googleSheetsRepository.UpdateSheetDataAsync(spreadsheetId, range, values);
        }

        public async Task AppendData(string spreadsheetId, string range, IList<IList<object>> values)
        {
            await _googleSheetsRepository.AppendToSheetAsync(spreadsheetId, range, values);
        }
    }
}
