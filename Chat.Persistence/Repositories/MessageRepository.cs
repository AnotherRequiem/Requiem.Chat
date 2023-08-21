using Chat.Domain;
using Persistence.Interfaces;

namespace Persistence.Repositories;

public class MessageRepository : IMessageRepository
{
    private readonly IChatDbContext _context;

    public MessageRepository(IChatDbContext context)
    {
        _context = context;
    }
    
    public void AddPrivateMessage(string from, string to, string content)
    {
        var newMessage = new Message
        {
            Id = Guid.NewGuid(),
            From = from,
            To = to,
            Content = content,
            TimeStamp = TimeOnly.FromDateTime(DateTime.Now)
        };
        
        _context.Messages.Add(newMessage);
        SaveChanges();
    }

    public string[] GetPrivateMessages(string from, string to)
    {
        var privateMessages = _context.Messages
            .Where(message => message.From == from && message.To == to)
            .OrderBy(message => message.TimeStamp)
            .Select(message => message.Content)
            .ToArray();

        return privateMessages;
    }

    public void SaveChanges()
    {
        _context.SaveChangesAsync(CancellationToken.None);
    }
}