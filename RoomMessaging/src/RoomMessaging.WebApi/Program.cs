using MediatR;
using RoomMessaging.Application;
using RoomMessaging.Application.Features.RoomMessage.Send;
using RoomMessaging.Application.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddApplication();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapPost("api/room/{room}/message", async (string room, RoomMessageItem message, IMediator mediator, CancellationToken ct) =>
{
    await mediator.Publish(new SendRoomMessageNotification
    {
        Name = room,
        Message = message.Message
    }, ct);
    return Results.Accepted();
});

app.Run();
