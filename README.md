# 🏢 Conference Room Booking System

This repository contains a project with the base code for a Conference Room Booking System.

This system will allow employees to search a list of available rooms and book a room for use.
There will be an administrator login portal giving them access to the administrator dashboard in the future.
There will also be functionality for a receptionist to book on behalf of another person and for facility managers to book a room for maintenance.
The code has been laid out with room for expansion for these features.

The current features are:
1) Booking a room
2) Canceling a booking
3) Exporting booking history as a json file
4) Loading history from a json file

This code will eventually be merged with existing API endpoints and documentation.

---

## Table of Contents

[How Failures are Handled](#How-Failures-are-Handled)
[Where Async is Used](#Where-Async-is-Used)
[🗂 Repository Contents](#🗂-Repository-Contents)
[⚙️ Installation](#⚙️-Installation)
[📌 Purpose of This Repository](#📌-Purpose-of-This-Repository)
[📋 System context](#📋-System-context)
[💪 Developer Onboarding Guide](#💪-Developer-Onboarding-Guide)
[🗎 Project Documentation](#🗎-Project-Documentation)
[Key Folders](#Key-Folders)
[🤝 Contributing](#🤝-Contributing)
[⏰ Upcoming Documentation](#⏰-Upcoming-Documentation)
[📄 License](#📄-License)
[✍️ Author](#✍️-Author)

---

## How Failures are Handled

- In the client code (`Program.cs`), all data that is taken from user input is validated before being passed into methods of other classes
- In methods (`Booking.cs`, `ConferenceRoom.cs`, `BookingService.cs`), all data is validated before further steps can be taken
- All validation follows a fail-fast approach to avoid invalid object states
- Custom exceptions (`BookingException`, `FileOperationException`) provide meaningful error messages
- The `BookingResult` class handles expected failures without throwing exceptions
- Different error handling strategies per layer: domain exceptions, service results, and I/O exceptions

---

## Where Async is Used

- This project makes use of async in creating a json file with booking history (`BookingFileHandler.SaveBookingsAsync()`)
- This project makes use of async in loading a json file with booking history (`BookingFileHandler.LoadBookingsAsync()`)
- Async operations support cancellation via `CancellationToken`
- The console application entry point uses `async Task Main()` for proper async flow
- Background auto-save operations run asynchronously after successful bookings

---

## 🗂 Repository Contents

- `README.md` – Project overview
- `Conference-Booking-Domain.csproj` – Project configuration
- `Program.cs` – Console application with interactive menu
- `Domain/` – Core business entities and enums
- `Services/` – Business logic layer
- `IO/` – File operations with async support
- `Exceptions/` – Custom exception types
- `LICENSE` – MIT License information

---

## ⚙️ Installation

- To install this project, you download the Zip file from GitHub and extract the project files to your selected location.
- Then you can use VS Code (make sure .NET 8 is installed on your device) to open the C# code and edit if you want to.
- Any changes will be made using the pull request template provided.
- Make sure to clone the GitHub repository should you indeed intend on creating pull requests.

---

## 📌 Purpose of This Repository

This repository is used for:
- Creating the code that will form the base of the Conference Room Booking System
- Working to eventually merge this work with existing API documentation
- Gradually improving project documentation over time
- Gradually improving project code over time

At this stage, the repository focuses on **Creating Robust, Production-Ready Code** with proper error handling and async operations.

---

## 📋 System context

The conference room booking system is intended to manage:
- Room Availability
- Booking Requests
- Conflict Prevention
- Administrator Access
- Administrator Conflict Resolution
- Receptionist Ability to Help Employees and Guests
- Facility Managers Ability to Schedule Rooms for Maintenance
- Data Persistence through JSON files
- Robust Error Handling for all operations

---

## 💪 Developer Onboarding Guide

- This project is developed incrementally for a Software training program
- The project will have additions made to it through assignments and will slowly build up to being a functional booking system for employees
- Documentation is organised with a pre-determined file structure, that will have files added and/or edited according to the work that is done
- Project artefacts can be found in the project folders. So far the artefacts available are:

    1) Complete C# solution with layered architecture
    2) Async file operations with error handling
    3) Defensive programming patterns throughout

- Technologies used so far:

    1) VS Code as a file editor
    2) Git for version control
    3) GitHub for repository storage
    4) .NET 8 for building and running the application

---

## 🗎 Project Documentation

This repository contains C# code and documentation for a conference room booking system.

### Key Folders

- `Domain/` – `Booking.cs`, `ConferenceRoom.cs`, `BookingStatus.cs`, `RoomType.cs`
- `Services/` – `BookingService.cs` with `BookingResult` pattern
- `IO/` – `BookingFileHandler.cs` with async operations
- `Exceptions/` – `BookingException.cs` for domain errors

---

## 🤝 Contributing

Changes to this repository are made using **Pull Requests**.

Contributors should:
- Create a feature or documentation branch
- Submit changes via a Pull Request
- Clearly describe the intent of the change and what was added or changed
- Follow existing code patterns and conventions

---

## ⏰ Upcoming Documentation

The following sections will be added as the project evolves:
- API documentations (Swagger/OpenAPI)
- Runtime instructions (Docker)
- Developer setup and contribution workflows
- Common issues reported by users with solutions or notification of the fix being in progress

---

## 📄 License

This project is licensed under the MIT License.

---

## ✍️ Author
Student: XDSassy
Created as part of professional software development training.