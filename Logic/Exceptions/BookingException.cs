namespace ConferenceBookingSystem.Exceptions;

public class BookingConflictException : Exception
{
    public BookingConflictException() : base("Booking overlaps with an existing booking") 
    {
        
     }
}
