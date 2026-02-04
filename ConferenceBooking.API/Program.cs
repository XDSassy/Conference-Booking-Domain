using ConferenceBooking.Domain;
using ConferenceBooking.Domain.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Create rooms list first
var rooms = new List<ConferenceRoom>
{
    new ConferenceRoom("101", 10, RoomType.Standard),
    new ConferenceRoom("202", 20, RoomType.Executive),
    new ConferenceRoom("303", 30, RoomType.Training)
};

// Register services in correct order
builder.Services.AddSingleton<List<ConferenceRoom>>(rooms);
builder.Services.AddSingleton<IBookingService, BookingService>();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();