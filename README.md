# 🏢 Conference Room Booking System – Assignment 1.2

This repository contains a C# implementation of a Conference Room Booking System, extended from Assignment 1.1 to include **working business logic, collections, and booking rules**.  

The project demonstrates **how bookings are processed, validated, and enforced** using C# collections and LINQ, while maintaining a clean separation between domain models, business logic, and program orchestration.

---

## 🗂 Repository Contents

- `README.md` – Project overview  
- `Program.cs` – Console application demonstrating booking logic  
- `Domain/ConferenceRoom.cs` – Conference room domain entity  
- `Domain/Booking.cs` – Booking domain entity  
- `Domain/BookingStatus.cs` – Booking status enum  
- `Domain/RoomType.cs` – Room type enum  
- `Services/BookingService.cs` – Business logic for handling bookings  

---

## ⚙️ Installation

- Open the project using Visual Studio or VS Code.  
- Ensure **.NET 8 SDK** is installed.  
- Build and run the project.  

---

## 📌 Purpose of This Repository

This project demonstrates:

- Domain modelling with C#  
- Business logic implementation using **collections** and LINQ  
- Enforcement of real-world booking rules  
- Separation of concerns between domain models, business logic, and program orchestration  

---

## 📋 System Features

- Create conference rooms and bookings  
- Submit booking requests through `BookingService`  
- Reject overlapping bookings (double-booking prevention)  
- Reject bookings for non-existent rooms  
- Cancel bookings and update booking status  
- Query available rooms for a given time range  

---

## 🧱 Domain Concepts

### ConferenceRoom

Represents a single conference room.

- Stores room number, capacity, and room type  
- Pure domain model; no internal booking logic  

### Booking

Represents a single booking.

- Stores room number, start time, end time, and status  
- Can be cancelled  
- Domain model only; booking logic is handled in `BookingService`  

### BookingStatus (Enum)

Defines valid booking states:

- Available  
- Booked  
- UnderMaintenance  

### RoomType (Enum)

Defines room categories:

- Standard  
- Executive  
- Training  

---

## 🧠 Business Rules Enforced

- **No double-booking:** overlapping bookings for the same room are rejected  
- **Valid room references only:** bookings must be for existing rooms  
- **Valid booking states:** bookings can only transition through allowed statuses  
- **Fail-fast:** invalid booking requests are rejected immediately  

---

## 💡 Implementation Highlights

**Collections Used:**

- `List<ConferenceRoom>` – stores all rooms  
- `Dictionary<string, List<Booking>>` – stores bookings per room for fast lookup  

**LINQ Used:**

- `Any()` – check for conflicting bookings  
- `Where() + ToList()` – query available rooms  

**Separation of Concerns:**

- Domain models (`Booking`, `ConferenceRoom`) contain data only  
- `BookingService` contains all business logic  
- `Program.cs` orchestrates sample bookings and displays results  

---

## 🚀 Usage

Run the console application to:

- Create sample rooms and bookings  
- Submit booking requests  
- See which bookings are accepted or rejected  
- View available rooms for a time range  

---

## 🤝 Contributing

- Use Git commits with **clear, meaningful messages**  
- Follow logical units of work (domain, service, program, README)  

---

## 📄 License

MIT License  

---

## ✍️ Author

Student: XDSassy  

Created as part of **Assignment 1.2 – Business Logic & Collections with C#**