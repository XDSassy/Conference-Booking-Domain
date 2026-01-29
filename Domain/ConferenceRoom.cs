using ConferenceBookingSystem.Exceptions;
namespace ConferenceRoomBooking.Domain;

public class ConferenceRoom
{
    /*public Guid Id {get; }
    public name {get; }*/
    public int Id {get,}
    public string RoomNum { get; }
    public int Capacity { get; }
    public RoomType Type { get; }

    public ConferenceRoom(int Id, string roomNum, int capacity, RoomType type)/*add room name*/
    {
        if (string.IsNullOrWhiteSpace(roomNum))
            throw new ArgumentException("Room number is required.");

        if (capacity <= 0)
            throw new ArgumentException("Capacity must be greater than zero.");

        //Id = Guid.newGuid();
        Id = ID;
        RoomNum = roomNum;
        Type = type;
        Capacity = capacity;
    }
}
