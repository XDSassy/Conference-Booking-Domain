using ConferenceBooking.Domain;
using ConferenceBooking.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ConferenceBooking.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        private readonly List<ConferenceRoom> _rooms;

        public BookingsController(IBookingService bookingService, List<ConferenceRoom> rooms)
        {
            _bookingService = bookingService;
            _rooms = rooms;
        }

        // GET: api/bookings
        [HttpGet]
        public IActionResult GetAllBookings()
        {
            // In a real app, you'd get this from a repository
            return Ok(new { message = "Get all bookings endpoint" });
        }

        // POST: api/bookings
        [HttpPost]
        public IActionResult CreateBooking([FromBody] CreateBookingRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var booking = new Booking(
                    request.RoomNum,
                    request.StartTime,
                    request.EndTime,
                    request.BookerName
                );

                var result = _bookingService.TryCreateBooking(booking);
                
                if (result.IsSuccess)
                {
                    return Ok(new 
                    { 
                        success = true, 
                        booking = result.Booking,
                        message = result.Message ?? "Booking created successfully"
                    });
                }
                else
                {
                    return BadRequest(new 
                    { 
                        success = false, 
                        error = result.Error 
                    });
                }
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new 
                { 
                    success = false, 
                    error = $"Internal server error: {ex.Message}" 
                });
            }
        }

        // GET: api/bookings/available
        [HttpGet("available")]
        public IActionResult GetAvailableRooms([FromQuery] DateTime start, [FromQuery] DateTime end)
        {
            if (start >= end)
            {
                return BadRequest(new { error = "Start time must be before end time" });
            }

            try
            {
                var availableRooms = _bookingService.GetAvailableRooms(start, end);
                return Ok(new { availableRooms });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new { error = $"Internal server error: {ex.Message}" });
            }
        }

        // DELETE: api/bookings/{roomNum}
        [HttpDelete("{roomNum}")]
        public IActionResult CancelBooking(string roomNum, [FromQuery] string bookerName, [FromQuery] DateTime bookingTime)
        {
            if (string.IsNullOrWhiteSpace(roomNum) || string.IsNullOrWhiteSpace(bookerName))
            {
                return BadRequest(new { error = "Room number and booker name are required" });
            }

            var result = _bookingService.CancelBooking(roomNum, bookerName, bookingTime);
            
            if (result.IsSuccess)
            {
                return Ok(new 
                { 
                    success = true, 
                    message = result.Message ?? "Booking cancelled successfully" 
                });
            }
            else
            {
                return BadRequest(new 
                { 
                    success = false, 
                    error = result.Error 
                });
            }
        }
    }

    // Request DTO for creating bookings
    public class CreateBookingRequest
    {
        public string RoomNum { get; set; } = string.Empty;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string BookerName { get; set; } = string.Empty;
    }
}