namespace FjordLineBooking.Models;

public class Departure
{
    public Guid Id { get; init; }
    public List<String> Route { get; init; } = new();
    public DateTime DepartureTime { get; init; }
    public int Capacity { get; init; }
    public List<Booking> Bookings { get; init; } = new();
}