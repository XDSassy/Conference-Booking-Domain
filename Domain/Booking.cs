using ConferenceBookingSystem.Exceptions;
namespace ConferenceRoomBooking.Domain;

public class Booking
{
    public string RoomNum { get; }
    public DateTime StartTime { get; }
    public DateTime EndTime { get; }
    public BookingStatus Status { get; private set; } 
    public string BookerName { get; } 

    public Booking(string roomNum, DateTime startTime, DateTime endTime, string bookerName)
    {
        // Enhanced Guard Clauses with better messages
        if (string.IsNullOrWhiteSpace(roomNum))
            throw new BookingException("Room number cannot be empty.");
        
        if (string.IsNullOrWhiteSpace(bookerName))
            throw new BookingException("Booker name is required.");

        if (startTime >= endTime)
            throw new BookingException("Start time must be before end time.");

        if (startTime < DateTime.Now)
            throw new BookingException("Cannot book rooms in the past.");

        RoomNum = roomNum;
        StartTime = startTime;
        EndTime = endTime;
        BookerName = bookerName;
        Status = BookingStatus.Booked; 
    }

    
    public void Cancel()
    {
        if (Status != BookingStatus.Booked)
            throw new InvalidOperationException($"Cannot cancel booking with status: {Status}");
        
        Status = BookingStatus.Available;
    }
}