using BookingApp.Data;
using BookingApp.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<BookingDbContext>(options =>
    options.UseSqlite("Data Source=booking.db"));
builder.Services.AddScoped<CsvUploadService>();
builder.Services.AddGrpc();

var app = builder.Build();
app.MapGrpcService<BookingApp.Grpc.BookingServiceImpl>();
app.MapGet("/", () => "GRPC BookingApp is running.");
app.Run();