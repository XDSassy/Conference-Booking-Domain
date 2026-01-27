namespace ConferenceRoomBooking.Domain;

public class ConferenceRoom
{
    public string RoomNum { get; }
    public int Capacity { get; }
    public RoomType Type { get; }

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
}
