namespace Chat.Domain;

public class Message
{
    public Guid Id { get; set; }
    
    public string From { get; set; }
    
    public string To { get; set; }
    
    public string Content { get; set; }
    
    public TimeOnly TimeStamp { get; set; }
}