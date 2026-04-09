namespace FjordLineBooking.DTOs;

public class CreateBookingRequest
{
    public string Name { get; set; }  = string.Empty;
    public int PassengerCount { get; set; }
    public bool HasVehicle { get; set; }
}