using System.Text.Json;
using ConferenceRoomBooking.Domain;

namespace ConferenceBookingSystem.IO;

public static class BookingFileHandler
{
    
    public class FileOperationException : Exception
    {
        public FileOperationException(string message, Exception innerException = null) 
            : base(message, innerException) { }
    }

    public static async Task SaveBookingsAsync(
        string filepath, 
        List<Booking> bookings, 
        CancellationToken cancellationToken = default) 
    {
        // Guard clauses
        if (string.IsNullOrWhiteSpace(filepath))
            throw new ArgumentException("File path cannot be null or empty.", nameof(filepath));
        
        if (bookings == null)
            throw new ArgumentNullException(nameof(bookings));

        try
        {
            
            var directory = Path.GetDirectoryName(filepath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            var options = new JsonSerializerOptions 
            { 
                WriteIndented = true, 
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            using var stream = File.Create(filepath);
            await JsonSerializer.SerializeAsync(stream, bookings, options, cancellationToken);
            
            Console.WriteLine($"Successfully saved {bookings.Count} bookings to '{filepath}'.");
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("Save operation was cancelled.");
            throw; 
        }
        catch (UnauthorizedAccessException ex)
        {
            throw new FileOperationException(
                $"Access denied to file '{filepath}'. Check permissions.", ex);
        }
        catch (IOException ex)
        {
            throw new FileOperationException(
                $"I/O error occurred while saving to '{filepath}'.", ex);
        }
        catch (Exception ex)
        {
            throw new FileOperationException(
                $"Unexpected error saving bookings to '{filepath}'.", ex);
        }
    }

    public static async Task<List<Booking>> LoadBookingsAsync(
        string filepath, 
        CancellationToken cancellationToken = default) 
    {
        // Guard clause
        if (string.IsNullOrWhiteSpace(filepath))
            throw new ArgumentException("File path cannot be null or empty.", nameof(filepath));

        try
        {
            if (!File.Exists(filepath))
            {
                Console.WriteLine($"File '{filepath}' not found. Returning empty list.");
                return new List<Booking>();
            }

            var options = new JsonSerializerOptions 
            { 
                PropertyNameCaseInsensitive = true 
            };

            using var stream = File.OpenRead(filepath);
            var bookings = await JsonSerializer.DeserializeAsync<List<Booking>>(
                stream, options, cancellationToken);
            
            Console.WriteLine($"Successfully loaded {bookings?.Count ?? 0} bookings from '{filepath}'.");
            return bookings ?? new List<Booking>(); 
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("Load operation was cancelled.");
            throw;
        }
        catch (JsonException ex)
        {
            throw new FileOperationException(
                $"Invalid JSON format in file '{filepath}'. File may be corrupted.", ex);
        }
        catch (IOException ex)
        {
            throw new FileOperationException(
                $"I/O error occurred while reading '{filepath}'.", ex);
        }
        catch (Exception ex)
        {
            throw new FileOperationException(
                $"Unexpected error loading bookings from '{filepath}'.", ex);
        }
    }
}