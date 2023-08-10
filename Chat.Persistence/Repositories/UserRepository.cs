using Chat.Domain;
using Persistence.Interfaces;

namespace Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IChatDbContext _context;

    public UserRepository(IChatDbContext context)
    {
        _context = context;
    }

    public bool AddUser(string userName)
    {
        foreach (var user in _context.Users)
        {
            if (user.Name.ToLower() == userName.ToLower())
            {
                return false;
            }
        }

        User newUser = new User
        {
            Id = new Guid(),
            Name = userName,
            ConnectionId = null,
            PrivateMessages = null
        };

        _context.Users.Add(newUser);
        SaveChanges();
        return true;
    }

    public void AddUserConnectionId(string userName, string connectionId)
    {
        var existingUser = _context.Users.FirstOrDefault(us => us.Name == userName);
        if (existingUser != null)
        {
            existingUser.ConnectionId = connectionId;
        }
        SaveChanges();
    }

    public string GetUserByConnectionId(string connectionId)
    {
        var user = _context.Users.Where(x => x.ConnectionId == connectionId).Select(x => x.Name).FirstOrDefault();
        return user;
    }

    public string GetConnectionIdByUser(string userName)
    {
        var connectionId = _context.Users.Where(x => x.Name == userName).Select(x => x.ConnectionId).FirstOrDefault();
        return connectionId;
    }

    public void RemoveUser(string userName)
    {
        var user = _context.Users.FirstOrDefault(x => x.Name == userName);
        _context.Users.Remove(user);
        SaveChanges();
    }

    public string[] GetOnlineUsers()
    {
        return _context.Users.OrderBy(x => x.Name).Select(x => x.Name).ToArray();
    }

    public void SaveChanges()
    {
        _context.SaveChangesAsync(CancellationToken.None);
    }
}