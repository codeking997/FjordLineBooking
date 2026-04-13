namespace FjordLineBooking.DTOs;

public class BookingResponse
{
    public Guid Id { get; init; } 
    public string Name { get; init; }  = string.Empty;
    public int  PassengerCount { get; init; }
    public bool HasVehicle { get; init; }
    
    
}