using ApiHotel.Application.Contracts;
using ApiHotel.Application.Services;
using ApiHotel.Domain.Contracts;
using ApiHotel.Infrastructure.Context;
using ApiHotel.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using RecreationalClub.Applications;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddDbContext<HotelContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionHotel")));

builder.Services.AddTransient<IHotelService, HotelService>();
builder.Services.AddTransient<IHotelDomain, HotelDomain>();
builder.Services.AddTransient<IRoomService, RoomService>();
builder.Services.AddTransient<IRoomDomain, RoomDomain>();
builder.Services.AddTransient<IReservationService, ReservationService>();
builder.Services.AddTransient<IReservationDomain, ReservationDomain>();


// Load SMTP settings from configuration
var smtpSettings = builder.Configuration.GetSection("Smtp");
var smtpServer = smtpSettings["Host"];
var smtpPort = int.Parse(smtpSettings["Port"]);
var smtpUsername = smtpSettings["Username"];
var smtpPassword = smtpSettings["Password"];

// Register EmailService with SMTP settings
builder.Services.AddSingleton(new EmailService(smtpServer, smtpPort, smtpUsername, smtpPassword));

var app = builder.Build();


//builder.WebHost.UseUrls("https://*:443", "http://*:80"); // Permite escuchar en los puertos 443 (HTTPS) y 80 (HTTP).

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors(policy =>
{
    policy.WithOrigins("http://localhost:4200") 
          .AllowAnyHeader()
          .AllowAnyMethod();
});

app.Run();
