using ConferenceBookingSystem.Exceptions;
namespace ConferenceRoomBooking.Domain;

public class Booking
{
    public string RoomNum { get; }
    public DateTime StartTime { get; }
    public DateTime EndTime { get;}
    public BookingStatus Status { get; }

    public Booking(string roomNum, DateTime startTime, DateTime endTime)
    {
        if (string.IsNullOrWhiteSpace(roomNumber))
            throw new BookingException("Room number cannot be empty.");

        if (start >= end)
            throw new BookingException("Start time must be before end time.");

        RoomNum = roomNum;
        Start = start;
        End = end;
    }
}
