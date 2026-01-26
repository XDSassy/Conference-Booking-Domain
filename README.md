# 🏢 Conference Room Booking System – Domain Model

This repository contains a C# domain model for a Conference Room Booking System.

The project focuses on modelling the **core business concepts and rules** that will later be reused when building APIs, databases, and user interfaces.

At this stage, the repository contains **domain logic only**, not a full production system.

---

## 🗂 Repository Contents

- `README.md` – Project overview  
- `Program.cs` – Small console application used to demonstrate the domain model  
- `ConferenceRoom.cs` – Conference room domain entity  
- `Booking.cs` – Booking domain entity  
- `BookingStatus.cs` – Booking status enum  
- `RoomType.cs` – Room type enum  

---

## ⚙️ Installation

- Download or clone the repository from GitHub  
- Open the project using Visual Studio or VS Code  
- Ensure **.NET 8 SDK** is installed  
- Build and run the project  

---

## 📌 Purpose of This Repository

This repository is used for:

- Practising domain modelling in C#  
- Creating a clean and reusable core model  
- Preparing a foundation for future API development  

At this stage, the focus is on **Domain Modelling**, not UI or persistence.

---

## 📋 System Context

The Conference Room Booking System is intended to manage:

- Conference rooms  
- Booking creation  
- Booking cancellation  
- Booking status  
- Room types  

Only the core rules and objects are implemented at this stage.

---

## 🧱 Domain Concepts

### ConferenceRoom

Represents a single conference room.

- Stores room number, capacity, and room type  
- Knows whether it is currently booked  
- Can create and cancel its own booking  

### Booking

Represents a single booking.

- Stores the room number  
- Stores booking status  
- Can be cancelled  

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

## 🧠 Business Rules

- A room cannot be booked if it is already booked  
- A booking cannot be cancelled if none exists  
- Room number must be provided  
- Capacity must be greater than zero  

Rules are enforced through constructors and methods, not comments.

---

## 💪 Developer Onboarding Guide

- This project is developed incrementally for training purposes  
- Features will be added in later assignments  
- Keep domain logic separate from UI and infrastructure  

Technologies used:

- C# (.NET 8)  
- Visual Studio / VS Code  
- GitHub  

---

## 🚀 Usage

Run the console application to:

- Create a room  
- Attempt to book it  
- Prevent double booking  
- Cancel a booking  

The console output is only for demonstration.

---

## 🤝 Contributing

Changes should be made using Git commits with clear messages.

---

## 📄 License

MIT License

---

## ✍️ Author

Student: XDSassy  

Created as part of Assignment 1.1 – Domain Modelling with C#