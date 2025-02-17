using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Core.GeoJson;
using BusPortal.DAL.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusPortal.DAL.Persistence.Repositories
{

    public interface IBookingRepository : _IBaseRepository<Booking, Guid>
    {
        Task<bool> IsSeatAvailableAsync(string startCity, string destinationCity,
            string departureTime, string seat);
        Task<bool> AddAsync(Booking booking);
        Task<IEnumerable<Booking>> GetBookingsByLineAsync(Guid Id, DateTime dateTime);
        Task<IEnumerable<Booking>> GetAsync();
        Task<int> GetCountAsync();
        Task<IEnumerable<int>> GetOccupiedSeatsAsync(string seat);
        Task<IEnumerable<string>> GetAvailableDepartureTimesAsync(string startCity, string destinationCity);
        Task<decimal> GetTotalRevenueAsync();
        Task<Dictionary<string,int>> GetTopRoutesAsync();
        Task<Dictionary<string, int>> GetDailyBookingsAsync();

    }
    internal class BookingRepository : _BaseRepository<Booking, Guid>, IBookingRepository
    {
        private readonly DALDbContext _context;

        public BookingRepository(DALDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<int> GetCountAsync()
        {
            return await _context.Bookings.CountAsync();
        }
        public async Task<decimal> GetTotalRevenueAsync()
        {
            return await _context.Bookings.SumAsync(x=> x.Price);
        }


        public async Task<Dictionary<string, int>> GetDailyBookingsAsync()
        {
            var dailyBookings =  _context.Bookings
                .GroupBy(b => b.DateTime.Date)
                 .Select(g => new
                 {
                     Date = g.Key,
                     Count = g.Count()
                 });

            return dailyBookings.ToDictionary(x => x.Date.ToString("dd-MM-yyyy"), x => x.Count); ;
        }
        public async Task<Dictionary<string,int>> GetTopRoutesAsync()
        {
            var lines = _context.Bookings.GroupBy(b => b.LineId).Select(x => new
            {
                lineId = x.Key,
                count = x.Count()
            }).Join(_context.Lines, bl => bl.lineId, l => l.Id, (bl, l) => new 
            {
                LineName = l.StartCity + "-" + l.DestinationCity,
                Count = bl.count
            });

            return lines.ToDictionary(x => x.LineName, x => x.Count); ;
        }
        public async Task<IEnumerable<Booking>> GetBookingsByLineAsync(Guid Id, DateTime dateTime)
       {
            return await _context.Bookings
               .Where(b => b.Line.Id == Id && b.DateTime == dateTime)
               .ToListAsync();
        }
        
        public async Task<IEnumerable<Booking>> GetAsync()
        {
            return await _context.Bookings.ToListAsync();
        }

        public async Task<bool> IsSeatAvailableAsync(string seat)
        {
            return !await _context.Bookings
                .AnyAsync(b =>
                     b.Seat == seat);
        }

      
        public async Task<IEnumerable<int>> GetOccupiedSeatsAsync(string seat)
        {
            var bookings = await _context.Bookings.ToListAsync();
            var occupiedSeats = bookings.Select(b => int.Parse(b.Seat));
            return occupiedSeats;
        }


        public async Task<IEnumerable<string>> GetAvailableDepartureTimesAsync(string startCity, string destinationCity)
        {
            return await _context.Bookings
                .Include(b => b.Line)
                .Where(b => b.Line.StartCity == startCity && b.Line.DestinationCity == destinationCity)
                .Select(b => b.Line.DepartureTimes)
                .Distinct()
                .ToListAsync();
        }
        public async Task<bool> AddAsync(Booking booking)
        {
            try
            {
                //if (!await IsSeatAvailableAsync(booking.Seat))
                //{
                //    return false;
                //}

                await _context.Bookings.AddAsync(booking);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Task<bool> IsSeatAvailableAsync(string startCity, string destinationCity, string departureTime, string seat)
        {
            throw new NotImplementedException();
        }
    }
}
