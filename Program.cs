using ConferenceBookingSystem.Domain;
using ConferenceBookingSystem.Exceptions;
using ConferenceBookingSystem.IO;

class Program
{
    static async Task Main(string[] args)
    {
        List<Booking> bookings = new();

        Console.WriteLine("===================================");
        Console.WriteLine("Welcome to the Booking System");
        Console.WriteLine("1: Book a Conference Room");
        Console.WriteLine("2: Cancel a Booking");
        Console.WriteLine("3: Export Booking history as json file");
        Console.WriteLine("4: Load history from file");
        Console.WriteLine("===================================");
        Console.Write("Select option: ");

        if (!int.TryParse(Console.ReadLine(), out int input))
        {
            Console.WriteLine("Invalid menu selection.");
            return;
        }

        try
        {
            switch (input)
            {
                // -------------------------
                case 1: // BOOK ROOM
                // -------------------------
                    Console.WriteLine("Enter room number:");
                    string roomNumber = Console.ReadLine()!;

                    Console.WriteLine("Enter start time (yyyy-MM-dd HH:mm):");
                    DateTime start = DateTime.Parse(Console.ReadLine()!);

                    Console.WriteLine("Enter end time (yyyy-MM-dd HH:mm):");
                    DateTime end = DateTime.Parse(Console.ReadLine()!);

                    Booking newBooking = new Booking(roomNumber, start, end);
                    bookings.Add(newBooking);

                    Console.WriteLine("Room successfully booked.");
                    break;

                // -------------------------
                case 2: // CANCEL BOOKING
                // -------------------------
                    Console.WriteLine("Enter room number:");
                    string cancelRoom = Console.ReadLine()!;

                    var bookingToRemove =
                        bookings.FirstOrDefault(b => b.RoomNumber == cancelRoom);

                    if (bookingToRemove == null)
                    {
                        Console.WriteLine("No booking found for that room.");
                    }
                    else
                    {
                        bookings.Remove(bookingToRemove);
                        Console.WriteLine("Booking cancelled.");
                    }
                    break;

                // -------------------------
                case 3: // SAVE
                // -------------------------
                    await BookingFileHandler.SaveBookingsAsync(
                        "bookings.json",
                        bookings);

                    break;

                // -------------------------
                case 4: // LOAD
                // -------------------------
                    bookings = await BookingFileHandler.LoadBookingsAsync(
                        "bookings.json");

                    Console.WriteLine($"Loaded {bookings.Count} bookings.");
                    break;

                default:
                    Console.WriteLine("Invalid selection.");
                    break;
            }
        }
        catch (BookingException ex)
        {
            Console.WriteLine($"Booking error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}
