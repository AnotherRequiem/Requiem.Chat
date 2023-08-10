namespace Chat.Domain;

public class Message
{
    public int Id { get; set; }
    
    public string Content { get; set; }
    
    public TimeOnly TimeStamp { get; set; }
    
    public Guid UserId { get; set; }
    
    public User User { get; set; }
}