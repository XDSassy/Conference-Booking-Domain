using System.Text.Json;
using ConferenceRoomBooking.Domain;
using ConferenceBookingSystem.Exceptions;
using ConferenceBookingSystem.IO;
using ConferenceRoomBooking.Services;

class Program
{
    private const string BookingsFilePath = "data/bookings.json";
    private static readonly List<ConferenceRoom> rooms = new()
    {
        new ConferenceRoom("101", 10, RoomType.Standard),
        new ConferenceRoom("202", 20, RoomType.Executive),
        new ConferenceRoom("303", 30, RoomType.Training)
    };
    private static BookingService bookingService = new BookingService(rooms);
    private static List<Booking> currentBookings = new();

    static async Task Main(string[] args)
    {
        Console.WriteLine("================================================");
        Console.WriteLine("Conference Room Booking System - Assignment 1.3");
        Console.WriteLine("Robustness, Failures & Asynchronous Operations");
        Console.WriteLine("================================================");
        Console.WriteLine("\nNOTE: This program demonstrates:");
        Console.WriteLine("- Valid booking scenarios (Option 1)");
        Console.WriteLine("- Invalid booking scenarios (Options 1 & 2 - try errors)");
        Console.WriteLine("- Failure handling (Try invalid inputs)");
        Console.WriteLine("- Async file operations (Options 3 & 4)");
        Console.WriteLine("================================================");

        await LoadExistingBookings();

        bool exit = false;
        while (!exit)
        {
            ShowMainMenu();
            
            if (!int.TryParse(Console.ReadLine(), out int choice))
            {
                ShowError("Invalid input. Please enter a number 1-5.");
                continue;
            }

            switch (choice)
            {
                case 1:
                    await BookRoom();
                    break;
                case 2:
                    await CancelBooking();
                    break;
                case 3:
                    await SaveBookingsToFile();
                    break;
                case 4:
                    await LoadBookingsFromFile();
                    break;
                case 5:
                    exit = true;
                    Console.WriteLine("\nExiting system. Goodbye!");
                    break;
                default:
                    ShowError("Invalid choice. Please select 1-5.");
                    break;
            }
        }
    }

    static void ShowMainMenu()
    {
        Console.WriteLine("\n=== MAIN MENU ===");
        Console.WriteLine("1. Book a Conference Room");
        Console.WriteLine("2. Cancel a Booking");
        Console.WriteLine("3. Save Bookings to File");
        Console.WriteLine("4. Load Bookings from File");
        Console.WriteLine("5. Exit");
        Console.Write("Select option (1-5): ");
    }

    static async Task BookRoom()
    {
        Console.WriteLine("\n=== BOOK A ROOM ===");
        Console.WriteLine("Available rooms:");
        foreach (var room in rooms)
        {
            Console.WriteLine($"- Room {room.RoomNum}: {room.Type} (Capacity: {room.Capacity})");
        }

        try
        {
            Console.Write("\nEnter room number: ");
            string roomNum = Console.ReadLine()?.Trim() ?? "";
            if (string.IsNullOrWhiteSpace(roomNum))
            {
                ShowError("Room number cannot be empty.");
                return;
            }
            Console.Write("Enter your name: ");
            string bookerName = Console.ReadLine()?.Trim() ?? "Anonymous";
            if (string.IsNullOrWhiteSpace(bookerName))
                bookerName = "Anonymous";

            Console.Write("Enter start time (yyyy-MM-dd HH:mm): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime startTime))
            {
                ShowError("Invalid date format. Please use yyyy-MM-dd HH:mm");
                return;
            }

            Console.Write("Enter end time (yyyy-MM-dd HH:mm): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime endTime))
            {
                ShowError("Invalid date format. Please use yyyy-MM-dd HH:mm");
                return;
            }
            var booking = new Booking(roomNum, startTime, endTime, bookerName);
            var result = bookingService.TryCreateBooking(booking);

            if (result.IsSuccess)
            {
                currentBookings.Add(booking);
                Console.WriteLine($"\n✅ SUCCESS: Room {roomNum} booked for {bookerName}");
                Console.WriteLine($"   Time: {startTime:yyyy-MM-dd HH:mm} to {endTime:HH:mm}");
                await AutoSaveBookings();
            }
            else
            {
                ShowError($"Booking failed: {result.Error}");
                Console.WriteLine("\n💡 TIP: This demonstrates the system's validation rules.");
                Console.WriteLine("   Common failures:");
                Console.WriteLine("   - Room doesn't exist");
                Console.WriteLine("   - Time conflict with existing booking");
                Console.WriteLine("   - Invalid time range (start >= end)");
            }
        }
        catch (BookingException ex)
        {
            ShowError($"Booking error: {ex.Message}");
            Console.WriteLine("💡 This is a BookingException - thrown by domain validation.");
        }
        catch (Exception ex)
        {
            ShowError($"Unexpected error: {ex.Message}");
        }
    }

    static async Task CancelBooking()
    {
        Console.WriteLine("\n=== CANCEL A BOOKING ===");
        
        if (currentBookings.Count == 0)
        {
            ShowError("No bookings to cancel.");
            Console.WriteLine("💡 Try booking a room first using option 1.");
            return;
        }

        Console.WriteLine("Your current bookings:");
        for (int i = 0; i < currentBookings.Count; i++)
        {
            var b = currentBookings[i];
            Console.WriteLine($"{i+1}. Room {b.RoomNum} - {b.BookerName} ({b.StartTime:MM/dd HH:mm} to {b.EndTime:HH:mm})");
        }

        Console.Write("\nEnter booking number to cancel (or 0 to go back): ");
        if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 0 || choice > currentBookings.Count)
        {
            ShowError("Invalid selection.");
            return;
        }

        if (choice == 0) return;

        var bookingToCancel = currentBookings[choice - 1];
        var result = bookingService.CancelBooking(
            bookingToCancel.RoomNum, 
            bookingToCancel.BookerName, 
            bookingToCancel.StartTime
        );

        if (result.IsSuccess)
        {
            currentBookings.RemoveAt(choice - 1);
            Console.WriteLine($"\n✅ Booking cancelled successfully.");
            Console.WriteLine($"   Room {bookingToCancel.RoomNum} is now available.");
            
            await AutoSaveBookings();
        }
        else
        {
            ShowError($"Cancellation failed: {result.Error}");
            Console.WriteLine("\n💡 This demonstrates the system's consistency checks.");
        }
    }

    static async Task SaveBookingsToFile()
    {
        Console.WriteLine("\n=== SAVE BOOKINGS TO FILE ===");
        Console.WriteLine($"Saving {currentBookings.Count} bookings to file...");
        
        try
        {
            Console.Write("Enter filename (default: bookings.json): ");
            string filename = Console.ReadLine()?.Trim() ?? "bookings.json";
            if (string.IsNullOrWhiteSpace(filename))
                filename = "bookings.json";

            Console.WriteLine("Saving...");
            await BookingFileHandler.SaveBookingsAsync(filename, currentBookings);
            Console.WriteLine($"\n✅ Saved {currentBookings.Count} bookings to '{filename}'");
            
            if (File.Exists(filename))
            {
                var fileInfo = new FileInfo(filename);
                Console.WriteLine($"   File size: {fileInfo.Length} bytes");
                Console.WriteLine($"   Location: {Path.GetFullPath(filename)}");
            }
        }
        catch (BookingFileHandler.FileOperationException ex)
        {
            ShowError($"File save failed: {ex.Message}");
            Console.WriteLine("\n💡 This demonstrates handling of I/O errors:");
            Console.WriteLine("   - Permission denied");
            Console.WriteLine("   - Disk full");
            Console.WriteLine("   - Invalid path");
        }
        catch (Exception ex)
        {
            ShowError($"Unexpected error: {ex.Message}");
        }
    }

    static async Task LoadBookingsFromFile()
    {
        Console.WriteLine("\n=== LOAD BOOKINGS FROM FILE ===");
        
        try
        {
            Console.Write("Enter filename to load from (default: bookings.json): ");
            string filename = Console.ReadLine()?.Trim() ?? "bookings.json";
            
            if (!File.Exists(filename))
            {
                ShowError($"File '{filename}' not found.");
                Console.WriteLine("\n💡 Try saving bookings first using option 3.");
                Console.WriteLine("   Or create a sample file with valid JSON.");
                return;
            }

            Console.WriteLine("Loading...");
            
            var loadedBookings = await BookingFileHandler.LoadBookingsAsync(filename);
            
            if (loadedBookings.Count == 0)
            {
                Console.WriteLine("\nℹ️  File loaded but contains no bookings.");
            }
            else
            {
                currentBookings = loadedBookings;
                Console.WriteLine($"\n✅ Loaded {loadedBookings.Count} bookings from '{filename}'");
                Console.WriteLine($"   Sample: Room {loadedBookings[0].RoomNum} - {loadedBookings[0].BookerName}");
                
                bookingService = new BookingService(rooms);
                foreach (var booking in currentBookings)
                {
                    bookingService.TryCreateBooking(booking);
                }
            }
        }
        catch (JsonException ex)
        {
            ShowError($"Invalid JSON format: {ex.Message}");
            Console.WriteLine("\n💡 This demonstrates handling of corrupted/malformed data files.");
        }
        catch (BookingFileHandler.FileOperationException ex)
        {
            ShowError($"File load failed: {ex.Message}");
        }
        catch (Exception ex)
        {
            ShowError($"Unexpected error: {ex.Message}");
        }
    }

    static async Task LoadExistingBookings()
    {
        try
        {
            if (File.Exists(BookingsFilePath))
            {
                Console.WriteLine("Loading existing bookings...");
                currentBookings = await BookingFileHandler.LoadBookingsAsync(BookingsFilePath);
                Console.WriteLine($"Loaded {currentBookings.Count} existing booking(s).");
                
                foreach (var booking in currentBookings)
                {
                    bookingService.TryCreateBooking(booking);
                }
            }
            else
            {
                Console.WriteLine("No existing bookings found. Starting fresh.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Note: Could not load existing bookings: {ex.Message}");
            Console.WriteLine("Starting with empty booking list.");
        }
    }

    static async Task AutoSaveBookings()
    {
        try
        {
            await Task.Delay(100); 
            await BookingFileHandler.SaveBookingsAsync(BookingsFilePath, currentBookings);
        }
        catch (Exception)
        {
         
        }
    }

    static void ShowError(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"\n❌ ERROR: {message}");
        Console.ResetColor();
    }
}