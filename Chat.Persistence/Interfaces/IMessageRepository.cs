namespace Persistence.Interfaces;

public interface IMessageRepository : IBaseRepository
{
    void AddPrivateMessage(string from, string to, string content);

    string[] GetPrivateMessages(string from, string to);

    void SaveChanges();
}