
using Microsoft.Extensions.Hosting;

namespace CarRental.BLL.Services.Scoped;

public delegate void ReservationCreatedEvent(int id);

internal class ReservationsService
{
    public static event ReservationCreatedEvent? OnReservationCreated;

    private static int _id = 0;

    public static void CreateReservation()
    {
        // Create reservation
        OnReservationCreated?.Invoke(_id++);
    }
}

internal class MailSenderService : IHostedService
{
    public Task StartAsync(CancellationToken cancellationToken)
    {
        ReservationsService.OnReservationCreated += ReservationsService_OnReservationCreated;
        ReservationsService.OnReservationCreated += ReservationsService_OnReservationCreated2;
        ReservationsService.OnReservationCreated += ReservationsService_OnReservationCreated3;

        ReservationsService.OnReservationCreated -= ReservationsService_OnReservationCreated3;
        return Task.CompletedTask;
    }

    private void ReservationsService_OnReservationCreated(int id)
    {
        Console.WriteLine($"Reservation 1 created with id: {id}");
    }

    private void ReservationsService_OnReservationCreated2(int id)
    {
        Console.WriteLine($"Reservation 2 created with id: {id}");
    }

    private void ReservationsService_OnReservationCreated3(int id)
    {
        Console.WriteLine($"Reservation 3 created with id: {id}");
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
