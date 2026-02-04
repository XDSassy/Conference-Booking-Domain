namespace ConferenceBooking.Domain.Services
{
    public interface IBookingService
    {
        BookingResult TryCreateBooking(Booking booking);
        List<ConferenceRoom> GetAvailableRooms(DateTime start, DateTime end);
        BookingResult CancelBooking(string roomNum, string bookerName, DateTime bookingTime);
    }
}