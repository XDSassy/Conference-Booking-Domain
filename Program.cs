using System;
using ConferenceRoomBooking.Domain;

class Program
{
    static void Main()
    {
        ConferenceRoom room1 =
            new ConferenceRoom("A101", 10, RoomType.Standard);

        Console.WriteLine("Attempting to book room...");
        bool booked = room1.BookRoom();
        Console.WriteLine(booked ? "Booked" : "Failed");

        Console.WriteLine("Attempting second booking...");
        booked = room1.BookRoom();
        Console.WriteLine(booked ? "Booked" : "Failed");

        Console.WriteLine("Cancelling booking...");
        bool cancelled = room1.CancelBooking();
        Console.WriteLine(cancelled ? "Cancelled" : "Failed");
    }
}