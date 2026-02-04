using System;

namespace ConferenceBooking.Domain.Exceptions
{
    public class BookingException : Exception
    {
        public BookingException(string message) : base(message) { }
        
        public BookingException(string message, Exception innerException) 
            : base(message, innerException) { }
    }
}