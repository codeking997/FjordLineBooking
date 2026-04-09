using FjordLineBooking.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSingleton<DepartureService>();

var app = builder.Build();
app.MapControllers();
app.Run();