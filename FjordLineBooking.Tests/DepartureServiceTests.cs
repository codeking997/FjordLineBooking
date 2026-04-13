using FjordLineBooking.Data;
using FjordLineBooking.Models;
using FjordLineBooking.Services;

namespace FjordLineBooking.Tests;

public class DepartureServiceTests
{
    [Fact]
    public void GetRemainingCapacity_ReturnsCapacityMinusBookedPassengers()
    {
        var departure = new Departure
        {
            Capacity = 100,
            Bookings =
            {
                new Booking { PassengerCount = 25 },
                new Booking { PassengerCount = 10 }
            }
        };

        var service = new DepartureService();

        var remainingCapacity = service.GetRemainingCapacity(departure);

        Assert.Equal(65, remainingCapacity);
    }

    [Fact]
    public void TryAddBooking_ReturnsFalse_WhenBookingExceedsRemainingCapacity()
    {
        var originalDepartures = InMemoryData.Departures
            .Select(d => d).ToList();
        var departure = new Departure
        {
            Id = Guid.NewGuid(),
            Capacity = 5,
            Bookings = { new Booking { Id = Guid.NewGuid(), PassengerCount = 4 } }
        };

        InMemoryData.Departures.Clear();
        InMemoryData.Departures.Add(departure);
        
        try
        {
            var booking = new Booking
            {
                Id = Guid.NewGuid(),
                Name = "Test Passenger",
                PassengerCount = 2
            };

            var service = new DepartureService();

            var added = service.TryAddBooking(departure.Id, booking, out var error);

            Assert.False(added);
            Assert.Equal("Not enough capacity", error);
            Assert.Single(departure.Bookings);
        }
        finally
        {
            InMemoryData.Departures.Clear();
            InMemoryData.Departures.AddRange(originalDepartures);
        }
    }

    [Fact]
    public void RemoveBooking_ReturnsTrue_WhenBookingExists()
    {
        var originalDepartures = InMemoryData.Departures;
        var bookingId = Guid.NewGuid();
        var departure = new Departure
        {
            Id = Guid.NewGuid(),
            Capacity = 10,
            Bookings =
            {
                new Booking
                {
                    Id = bookingId,
                    Name = "Test Passenger",
                    PassengerCount = 2
                }
            }
        };

        InMemoryData.Departures.Clear();
        InMemoryData.Departures.Add(departure);
        try
        {
            var service = new DepartureService();

            var removed = service.RemoveBooking(departure.Id, bookingId);

            Assert.True(removed);
            Assert.Empty(departure.Bookings);
        }
        finally
        {
            InMemoryData.Departures = originalDepartures;
        }
    }
    [Fact]
    public void Cancelling_Booking_Should_Restore_Capacity()
    {
        // arrange
        var service = new DepartureService();
        var departure = new Departure
        {
            Id = Guid.NewGuid(),
            Capacity = 100,
            Bookings = new List<Booking>()
        };

        InMemoryData.Departures.Clear();
        InMemoryData.Departures.Add(departure);

        var booking = new Booking
        {
            Id = Guid.NewGuid(),
            Name = "Carl",
            PassengerCount = 5,
            HasVehicle = false
        };

        service.TryAddBooking(departure.Id, booking, out _);
        var capacityAfterBooking = service.GetRemainingCapacity(departure);

        // act
        service.RemoveBooking(departure.Id, booking.Id);
        var capacityAfterCancel = service.GetRemainingCapacity(departure);

        // assert
        Assert.Equal(capacityAfterBooking + 5, capacityAfterCancel);
    }
}
