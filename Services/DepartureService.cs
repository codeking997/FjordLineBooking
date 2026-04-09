using FjordLineBooking.Data;
using FjordLineBooking.Models;

namespace FjordLineBooking.Services;

public class DepartureService
{
    public List<Departure> GetAll() => InMemoryData.Departures;

    public Departure? GetById(Guid id)
        => InMemoryData.Departures.FirstOrDefault(d => d.Id == id);

    public int GetRemainingCapacity(Departure departure)
        => departure.Capacity - departure.Bookings.Sum(b => b.PassengerCount);

    public bool TryAddBooking(Guid departureId, Booking booking, out string error)
    {
        var departure = GetById(departureId);
        if (departure == null)
        {
            error = "Departure not found";
            return false;
        }

        var remaining = GetRemainingCapacity(departure);
        if (booking.PassengerCount > remaining)
        {
            error = "Not enough capacity";
            return false;
        }

        departure.Bookings.Add(booking);
        error = string.Empty;
        return true;
    }

    public bool RemoveBooking(Guid departureId, Guid bookingId)
    {
        var departure = GetById(departureId);
        if (departure == null) return false;

        var booking = departure.Bookings.FirstOrDefault(b => b.BookingId == bookingId);
        if (booking == null) return false;

        departure.Bookings.Remove(booking);
        return true;
    }
}