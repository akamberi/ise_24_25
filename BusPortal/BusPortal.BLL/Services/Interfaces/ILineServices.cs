using BusPortal.BLL.Domain.Models;
using BusPortal.Common.Models;
using BusPortal.DAL.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusPortal.BLL.Services.Interfaces
{
    public interface ILinesService
    {
        Task AddLineAsync(AddLineViewModel viewModel);
        Task<List<Line>> GetAllLinesAsync();
        Task<Line> GetLineByIdAsync(Guid id);
        Task UpdateLineAsync(Line viewModel);
        Task DeleteLineAsync(Guid id);
        Task<IEnumerable<string>> GetAllStartCitiesAsync();
        Task<IEnumerable<string>> GetDestinationCitiesForStartCityAsync(string startCity);
        Task<Line> GetLineByRouteAsync(string startCity, string destinationCity);
    }
}
