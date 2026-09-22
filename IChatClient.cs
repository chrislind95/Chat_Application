public interface IChatClient
{
    Task ReceiveMessage(string username, string message);
}