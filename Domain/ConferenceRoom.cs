public class ConferenceRoom
{
    public string RoomNum { get; }
    public int Capacity { get; }
    public RoomType Type { get; }

    private readonly List<Booking> _bookings = new();
    public IReadOnlyCollection<Booking> Bookings => _bookings.AsReadOnly();

    public ConferenceRoom(string roomNum, int capacity, RoomType type)
    {
        if (string.IsNullOrWhiteSpace(roomNum))
            throw new ArgumentException("Room number is required.");

        if (capacity <= 0)
            throw new ArgumentException("Capacity must be greater than zero.");

        RoomNum = roomNum;
        Capacity = capacity;
        Type = type;
    }

    public Booking RequestBooking(
        DateTime startTime,
        DateTime endTime,
        int attendees)
    {
        if (attendees > Capacity)
            throw new InvalidOperationException("Room capacity exceeded.");

        if (!IsAvailable(startTime, endTime))
            throw new InvalidOperationException("Room is not available for this time slot.");

        var booking = new Booking(startTime, endTime, attendees);
        _bookings.Add(booking);

        return booking;
    }

    private bool IsAvailable(DateTime start, DateTime end)
    {
        return _bookings
            .Where(b => b.Status == BookingStatus.Approved)
            .All(b => end <= b.StartTime || start >= b.EndTime);
    }
}