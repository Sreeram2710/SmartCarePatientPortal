using Microsoft.AspNetCore.SignalR;
using SmartCarePatientPortal.Data;
using SmartCarePatientPortal.Models;

namespace SmartCarePatientPortal.Hubs
{ }

public class ChatHub : Hub
{
    private readonly ApplicationDbContext _context;

    public ChatHub(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task SendMessage(string sender, string receiver, string message, string timestamp)
    {
        var msg = new Message
        {
            Sender = sender,
            Receiver = receiver,
            Content = message,
            Timestamp = DateTime.Parse(timestamp)
        };

        _context.Messages.Add(msg);
        await _context.SaveChangesAsync();

        await Clients.User(receiver).SendAsync("ReceiveMessage", sender, message, timestamp);
        await Clients.Caller.SendAsync("ReceiveMessage", sender, message, timestamp);
    }
}
