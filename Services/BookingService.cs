using System;
using System.Collections.Generic;
using System.Linq;
using ConferenceRoomBooking.Domain;

namespace ConferenceRoomBooking.Services
{
    public class BookingService
    {
        private readonly List<ConferenceRoom> _rooms;
        private readonly Dictionary<string, List<Booking>> _bookings;

        public BookingService(List<ConferenceRoom> rooms)
        {
            _rooms = rooms;
            _bookings = new Dictionary<string, List<Booking>>();
        }

        public bool TryCreateBooking(Booking booking)
        {
            // Check room exists
            if (!_rooms.Any(r => r.RoomNum == booking.RoomNum))
                return false;

            // Create list for room if not exists
            if (!_bookings.ContainsKey(booking.RoomNum))
                _bookings[booking.RoomNum] = new List<Booking>();

            var roomBookings = _bookings[booking.RoomNum];

            // Check overlapping bookings
            bool hasConflict = roomBookings.Any(b =>
                booking.StartTime < b.EndTime &&
                booking.EndTime > b.StartTime
            );

            if (hasConflict)
                return false;

            roomBookings.Add(booking);
            return true;
        }

        public List<ConferenceRoom> GetAvailableRooms(DateTime start, DateTime end)
        {
            return _rooms.Where(room =>
            {
                if (!_bookings.ContainsKey(room.RoomNum))
                    return true;

                return !_bookings[room.RoomNum].Any(b =>
                    start < b.EndTime && end > b.StartTime
                );
            }).ToList();
        }
    }
}