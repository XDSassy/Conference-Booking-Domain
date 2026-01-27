using ConferenceRoomBooking.Domain;
using ConferenceRoomBooking.Services;
// Create rooms
var rooms = new List<ConferenceRoom>
{
    new ConferenceRoom("A101", 10, RoomType.Standard),
    new ConferenceRoom("B202", 20, RoomType.Training)
};

// Create booking service
var bookingService = new BookingService(rooms);

// Create bookings
var booking1 = new Booking(
    "A101",
    new DateTime(2026, 1, 27, 9, 0, 0),
    new DateTime(2026, 1, 27, 10, 0, 0)
);

var booking2 = new Booking(
    "A101",
    new DateTime(2026, 1, 27, 9, 30, 0),
    new DateTime(2026, 1, 27, 10, 30, 0)
);

// Try bookings
Console.WriteLine("Booking 1 accepted: " +
    bookingService.TryCreateBooking(booking1));

Console.WriteLine("Booking 2 accepted: " +
    bookingService.TryCreateBooking(booking2));

// Check available rooms
Console.WriteLine("\nAvailable rooms from 09:00 to 10:00:");

var availableRooms = bookingService.GetAvailableRooms(
    new DateTime(2026, 1, 27, 9, 0, 0),
    new DateTime(2026, 1, 27, 10, 0, 0)
);

foreach (var room in availableRooms)
{
    Console.WriteLine(room.RoomNum);
}
