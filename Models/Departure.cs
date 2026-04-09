namespace FjordLineBooking.Models;

public class Departure
{
    public Guid Id { get; set; }
    public List<String> Route { get; set; } = new();
    public DateTime DepartureTime { get; set; }
    public int Capacity { get; set; }
    public List<Booking> Bookings { get; set; } = new();
}