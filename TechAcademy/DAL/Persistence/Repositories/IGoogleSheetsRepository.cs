using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Persistence.Repositories
{
    public interface IGoogleSheetsRepository
    {
        Task<IList<IList<object>>> GetSheetDataAsync(string spreadsheetId, string range);
        Task UpdateSheetDataAsync(string spreadsheetId, string range, IList<IList<object>> values);
        Task AppendToSheetAsync(string spreadsheetId, string range, IList<IList<object>> values);
    }
}
