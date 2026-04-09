namespace FjordLineBooking.Models;

public class Booking
{
    public Guid BookingId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int PassengerCount { get; set; }
    public bool HasVehicle { get; set; }
}