namespace Persistence.Interfaces;

public interface IUserRepository : IBaseRepository
{
    bool AddUser(string userName);
    
    void AddUserConnectionId(string userName, string connectionId);
    
    string GetUserByConnectionId(string connectionId);
    
    string GetConnectionIdByUser(string userName);
    
    void RemoveUser(string userName);
    
    string[] GetOnlineUsers();
}