using System;
using System.Collections.Generic;
using System.Linq;
using ConferenceRoomBooking.Domain;
using ConferenceBookingSystem.Exceptions; 

namespace ConferenceRoomBooking.Services
{
    public class BookingService
    {
        private readonly List<ConferenceRoom> _rooms;
        private readonly Dictionary<string, List<Booking>> _bookings;

        public BookingService(List<ConferenceRoom> rooms)
        {
            
            if (rooms == null)
                throw new ArgumentNullException(nameof(rooms));
            
            if (rooms.Count == 0)
                throw new ArgumentException("Room list cannot be empty.", nameof(rooms));

            _rooms = rooms;
            _bookings = new Dictionary<string, List<Booking>>();
        }

        public BookingResult TryCreateBooking(Booking booking) 
        {
            // Guard Clauses
            if (booking == null)
                return BookingResult.Failure("Booking cannot be null.");
            
            if (_rooms == null || _rooms.Count == 0)
                return BookingResult.Failure("No rooms available in the system.");
            
            var room = _rooms.FirstOrDefault(r => r.RoomNum == booking.RoomNum);
            if (room == null)
                return BookingResult.Failure($"Room '{booking.RoomNum}' does not exist.");

            
            if (!_bookings.ContainsKey(booking.RoomNum))
                _bookings[booking.RoomNum] = new List<Booking>();

            var roomBookings = _bookings[booking.RoomNum];

            
            bool hasConflict = roomBookings.Any(existingBooking =>
                booking.StartTime < existingBooking.EndTime &&
                booking.EndTime > existingBooking.StartTime &&
                existingBooking.Status == BookingStatus.Booked 
            );

            if (hasConflict)
                return BookingResult.Failure($"Room '{booking.RoomNum}' is already booked for the requested time.");

            roomBookings.Add(booking);
            return BookingResult.Success(booking);
        }

        public List<ConferenceRoom> GetAvailableRooms(DateTime start, DateTime end)
        {
            
            if (start >= end)
                throw new ArgumentException("Start time must be before end time.");
            
            if (start < DateTime.Now)
                throw new ArgumentException("Cannot check availability for past dates.");

            return _rooms.Where(room =>
            {
                
                if (!_bookings.TryGetValue(room.RoomNum, out var bookingsForRoom))
                    return true;

                
                return !bookingsForRoom.Any(b => 
                    b.Status == BookingStatus.Booked &&
                    start < b.EndTime && 
                    end > b.StartTime
                );
            }).ToList();
        }

        public BookingResult CancelBooking(string roomNum, string bookerName, DateTime bookingTime)
        {
            
            if (string.IsNullOrWhiteSpace(roomNum))
                return BookingResult.Failure("Room number is required.");
            
            if (string.IsNullOrWhiteSpace(bookerName))
                return BookingResult.Failure("Booker name is required.");

            
            if (!_bookings.TryGetValue(roomNum, out var roomBookings))
                return BookingResult.Failure($"No bookings found for room '{roomNum}'.");

            
            var bookingToCancel = roomBookings.FirstOrDefault(b =>
                b.BookerName.Equals(bookerName, StringComparison.OrdinalIgnoreCase) &&
                b.StartTime <= bookingTime &&
                b.EndTime >= bookingTime &&
                b.Status == BookingStatus.Booked
            );

            if (bookingToCancel == null)
                return BookingResult.Failure("No active booking found matching the criteria.");

            try
            {
                bookingToCancel.Cancel(); 
                return BookingResult.Success(bookingToCancel, "Booking cancelled successfully.");
            }
            catch (InvalidOperationException ex)
            {
                return BookingResult.Failure($"Cannot cancel booking: {ex.Message}");
            }
        }
    }

    public class BookingResult
    {
        public bool IsSuccess { get; }
        public Booking? Booking { get; }  // Add ? to make nullable
        public string? Message { get; }   // Add ? 
        public string? Error { get; }     // Add ?

         private BookingResult(bool isSuccess, Booking? booking, string? message, string? error)
        {
        IsSuccess = isSuccess;
        Booking = booking;
        Message = message;
        Error = error;
         }

        public static BookingResult Success(Booking booking, string? message = null)
        => new BookingResult(true, booking, message, null);

        public static BookingResult Failure(string error)
        => new BookingResult(false, null, null, error);  // Now valid
    }   
}

/* Classwork
public class BokingManager //all bussines rules are here
{
    //properties
    private readonly List<Booking> _bookings
    
    //Methods
    public IReadonlyList<Booking> GetBooking()
    {
        return _booking.ToList();
    }
    public Booking CreateBooking(BookingRequest request)
    {
        //Guard Cluases
        if(request.Room == null)
        {
            throw new ArgumentException("Room must exist");
        }
        if (request.Start >= request.End)
        {
            throw new ArgumentException("Invalid time range")
        }

        bool overlaps = _bookings.Any(b => b.Room == request.Room && 
        b.Status == BookingStatus.Confirmed && requst.Start < b.End &&
        request.End > b.Start );

        if (overlaps)
        {
            throw new BookingConflictException()
        }
        Booking booking = new Booking(request.Room, request.Start, request.End);

        booking.Confrim();
        _booking.Add(booking);

        return booking;
        
    }
}