using FjordLineBooking.Models;
namespace FjordLineBooking.Data;

public static class InMemoryData
{
    public static List<Departure> Departures = new()
    {
        new Departure
        {
            Id = Guid.NewGuid(),
            Route = new List<string> { "Bergen", "Stavanger", "Hirtshals", "Kristiansand" },
            DepartureTime = DateTime.UtcNow.AddHours(5),
            Capacity = 100,
        },
        new Departure
        {
            Id = Guid.NewGuid(),
            Route = new List<string> { "Bergen", "Stavanger", "Hirtshals", "Kristiansand" },
            DepartureTime = DateTime.UtcNow.AddHours(10),
            Capacity = 80,
        }
    };
}