public class Booking
{
    public DateTime StartTime { get; }
    public DateTime EndTime { get; }
    public int Attendees { get; }
    public BookingStatus Status { get; private set; }

    public Booking(DateTime startTime, DateTime endTime, int attendees)
    {
        if (endTime <= startTime)
            throw new ArgumentException("End time must be after start time.");

        if (attendees <= 0)
            throw new ArgumentException("Attendees must be greater than zero.");

        StartTime = startTime;
        EndTime = endTime;
        Attendees = attendees;
        Status = BookingStatus.Requested;
    }

    public void Approve()
    {
        if (Status != BookingStatus.Requested)
            throw new InvalidOperationException("Only requested bookings can be approved.");

        Status = BookingStatus.Approved;
    }

    public void Cancel()
    {
        if (Status == BookingStatus.Cancelled)
            return;

        Status = BookingStatus.Cancelled;
    }
}