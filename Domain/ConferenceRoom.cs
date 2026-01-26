namespace ConferenceRoomBooking.Domain;

public class ConferenceRoom
{
    public string RoomNum { get; }
    public int Capacity { get; }
    public RoomType Type { get; }

    public Booking? CurrentBooking { get; private set; }

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

    public bool BookRoom()
    {
        if (CurrentBooking is not null)
            return false;

        CurrentBooking = new Booking(RoomNum);
        return true;
    }

    public bool CancelBooking()
    {
        if (CurrentBooking is null)
            return false;

        CurrentBooking.Cancel();
        CurrentBooking = null;
        return true;
    }

    public bool IsAvailable()
    {
        return CurrentBooking is null;
    }
}