using System.ComponentModel.DataAnnotations;

namespace FjordLineBooking.DTOs;

public class CreateBookingRequest
{
    public string Name { get; init; }  = string.Empty;
    [Range(1, int.MaxValue)]
    public int PassengerCount { get; init; }
    public bool HasVehicle { get; init; }
}