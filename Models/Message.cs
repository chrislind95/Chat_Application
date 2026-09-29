namespace ChatStart.Models;

public class Message
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int RoomId { get; set; }
    public string Text { get; set; } = string.Empty;
    public DateTime SentAt { get; set; }
}