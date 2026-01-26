namespace ConferenceRoomBooking.Domain;

public class Booking
{
    public string RoomNum { get; private set; }
    public BookingStatus Status { get; private set; }

    public Booking(string roomNum)
    {
        RoomNum = roomNum;
        Status = BookingStatus.Booked;
    }

    public void Cancel()
    {
        Status = BookingStatus.Available;
    }
}