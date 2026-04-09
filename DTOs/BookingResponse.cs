namespace FjordLineBooking.DTOs;

public class BookingResponse
{
    public Guid Id { get; set; } 
    public string Name { get; set; }  = string.Empty;
    public int  PassengersCount { get; set; }
    public bool HasVehicle { get; set; }
    
    
}