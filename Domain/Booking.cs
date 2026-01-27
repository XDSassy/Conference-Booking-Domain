namespace ConferenceRoomBooking.Domain;

public class Booking
{
    public string RoomNum { get; private set; }
    public DateTime StartTime { get; private set; }
    public DateTime EndTime { get; private set; }
    public BookingStatus Status { get; private set; }

    public Booking(string roomNum, DateTime startTime, DateTime endTime)
    {
        RoomNum = roomNum;
        StartTime = startTime;
        EndTime = endTime;
        Status = BookingStatus.Booked;
    }

    public void Cancel()
    {
        Status = BookingStatus.Available;
    }
}
