namespace Persistence.Interfaces;

public interface IBaseRepository
{
    public bool AddUser(string userName);
    
    public void AddUserConnectionId(string userName, string connectionId);
    
    public string GetUserByConnectionId(string connectionId);
    
    public string GetConnectionIdByUser(string userName);
    
    public void RemoveUser(string userName);
    
    public string[] GetOnlineUsers();
}