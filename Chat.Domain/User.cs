namespace Chat.Domain;

public class User
{
    public Guid Id { get; set; }
    
    public string? ConnectionId { get; set; }
    
    public string Name { get; set; }
    
    public List<Message>? PrivateMessages { get; set; }
}