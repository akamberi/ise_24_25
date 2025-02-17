using BusPortal.DAL.Persistence.Entities;
using BusPortal.DAL.Persistence.Repositories;
using BusPortal.Common.Models;
using System;
using BusPortal.BLL.Services.Interfaces;

namespace BusPortal.BLL.Services.Scoped
{
    public class BookingServices : IBookingServices
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IClientRepository _clientRepository;
        private readonly ILineRepository _lineRepository;



        public BookingServices(

          IBookingRepository bookingRepository,
           IClientRepository clientRepository,
           ILineRepository lineRepository)
        {
            _bookingRepository = bookingRepository;
            _clientRepository = clientRepository;
            _lineRepository = lineRepository;
        }

        public async Task<int> GetTotalBookingsAsync()
        {
            return await _bookingRepository.GetCountAsync();
        }
        public async Task<decimal> GetTotalRevenueAsync()
        {
            return await _bookingRepository.GetTotalRevenueAsync();
        }
        public async Task<Dictionary<string,int>> GetTopRoutesAsync()
        {
            return await _bookingRepository.GetTopRoutesAsync();
        }
        public async Task<Dictionary<string, int>> GetDailyBookingsAsync()
        {
            return await _bookingRepository.GetDailyBookingsAsync();
        }


        public async Task<List<string>> GetOccupiedSeatsAsync(string lineId, string dateSelected, string timeSelected)
        {
            var datetimeParsed = DateTime.Parse(dateSelected + " " + timeSelected);
            var lineIdParsed = Guid.Parse(lineId);
            var bookings = await _bookingRepository.GetBookingsByLineAsync(lineIdParsed, datetimeParsed);
            var occupiedSeats = new List<string>();

            foreach (var booking in bookings)
            {
                occupiedSeats.Add(booking.Seat);
            }
       
            return occupiedSeats;
        }
          
       
        public async Task<IEnumerable<string>> GetAvailableDepartureTimesAsync(string startCity, string destinationCity)
        {
            var booking = await _bookingRepository.GetAsync();
            var availableTimes = booking
                .Where(b => b.Line.StartCity == startCity && b.Line.DestinationCity == destinationCity)
                .Select(b => b.Line.DepartureTimes)
                .Distinct();
            return availableTimes;
        }

        //add seat selection system
        public (bool Success, string? ErrorMessage) AddBooking(Guid clientId, Guid lineId, string seat, DateTime dateTime, decimal price)
        {
            try
            {
                var client = _clientRepository.GetById(clientId);
                var line = _lineRepository.GetById(lineId);
                var booking = new Booking
                {
                    Id = Guid.NewGuid(),
                    Client = client,
                    Line = line,
                    DateTime = dateTime,
                    Seat = seat,
                    Price = price
                };
                _bookingRepository.AddAsync(booking);
                return (true, null);

            }
            catch (Exception ex)
            {
                return (false, "An unexpected error occurred while creating the booking.");
            }



        }

 
    }
}
