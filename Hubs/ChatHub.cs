using Microsoft.AspNetCore.SignalR;

public sealed class ChatHub : Hub<IChatClient>
{
    public async Task JoinRoom(string roomName)
    {
        if (string.IsNullOrWhiteSpace(roomName))
            throw new HubException("Rumsnamn måste anges.");

        await Groups.AddToGroupAsync(Context.ConnectionId, roomName);
        Console.WriteLine($"Anslutning {Context.ConnectionId} gick med i rummet: {roomName}");
    }

    //SignalR Hub som tar emot meddlenaden från klienten
    //och skickar dem vidare till alla anslutna klienter 
    public async Task SendMessage(string username, string message, string roomName)
    {
        //MessageValidator validerar om input är giltig
        MessageValidator.Validate(username, message);

        if (string.IsNullOrWhiteSpace(roomName))
            throw new HubException("Rumsnamn måste anges.");

        //Servern skickar meddelandet till alla anslutna klienter
        await Clients.Group(roomName).ReceiveMessage(username, message);
    }
}