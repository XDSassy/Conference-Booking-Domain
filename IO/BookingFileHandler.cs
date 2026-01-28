using System.Text.Json;
using ConferenceRoomBooking.Domain;

namespace ConferenceBookingSystem.IO;

public static class BookingFileHandler
{
    public static async Task SaveBookingsAsync(string filepath, List<Booking> bookings)
    {
        try
        {
            using var stream = File.Create(filepath);
            await JsonSerializer.SerializeAsync(stream, bookings);
            Console.WriteLine("Bookings saved successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving bookings: {ex.Message}");
        }
    }

    public static async Task<List<Booking>> LoadBookingsAsync(string filepath)
    {
        try
        {
            if (!File.Exists(filepath))
                return new List<Booking>(); 
            using var stream = File.OpenRead(filepath);
            return await JsonSerializer.DeserializeAsync<List<Booking>>(stream)
                     ?? new List<Booking>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading bookings: {ex.Message}");
            return new List<Booking>();
        }
    }
}