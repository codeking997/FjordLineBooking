using Microsoft.AspNetCore.Mvc;
using FjordLineBooking.Services;
using FjordLineBooking.Models;
using FjordLineBooking.DTOs;

namespace FjordLineBooking.Controllers;

[ApiController]
[Route("departures")]
public class DeparturesController : ControllerBase
{
    private readonly DepartureService _service;

    public DeparturesController(DepartureService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult GetDepartures()
    {
        var result = _service.GetAll()
            .Select(d => new
            {
                d.Id,
                d.Route,
                d.DepartureTime,
                RemainingCapacity = _service.GetRemainingCapacity(d)
            });

        return Ok(result);
    }

    [HttpPost("{id}/bookings")]
    public IActionResult CreateBooking(Guid id, [FromBody] CreateBookingRequest request)
    {
        if (request.PassengerCount <= 0)
            return BadRequest(new { error = "Passenger count must be greater than 0" });

        if (string.IsNullOrWhiteSpace(request.Name))
            return BadRequest(new { error = "Name is required" });

        var booking = new Booking
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            PassengerCount = request.PassengerCount,
            HasVehicle = request.HasVehicle
        };

        if (!_service.TryAddBooking(id, booking, out var error))
        {
            if (error == "Departure not found")
                return NotFound(new { error });

            if (error == "Not enough capacity")
                return Conflict(new { error });

            return BadRequest(new { error });
        }

        var response = new BookingResponse
        {
            Id = booking.Id,
            Name = booking.Name,
            PassengerCount = booking.PassengerCount,
            HasVehicle = booking.HasVehicle
        };

        return Ok(response);
    }

    [HttpGet("{id}/manifest")]
    public IActionResult GetManifest(Guid id)
    {
        if (id == Guid.Empty)
        {
            return BadRequest(new { error = "Invalid departure id" });
        }
        var departure = _service.GetById(id);
        if (departure == null) return NotFound();

        return Ok(departure.Bookings.Select(b => new BookingResponse
        {
            Id = b.Id,
            Name = b.Name,
            PassengerCount = b.PassengerCount,
            HasVehicle = b.HasVehicle
        }));
    }

    [HttpDelete("{id}/bookings/{bookingId}")]
    public IActionResult DeleteBooking(Guid id, Guid bookingId)
    {
        if (id == Guid.Empty)
        {
            return BadRequest(new { error = "Invalid departure id" });
        }
        var success = _service.RemoveBooking(id, bookingId);
        if (!success) return NotFound();

        return NoContent();
    }
}