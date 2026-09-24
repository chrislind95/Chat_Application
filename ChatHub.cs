using Microsoft.AspNetCore.SignalR;

public sealed class ChatHub : Hub<IChatClient>
{
    //SignalR Hub som tar emot meddlenaden från klienten
    //och skickar dem vidare till alla anslutna klienter 
    public async Task SendMessage(string username, string message)
    {
        //MessageValidator validerar om input är giltig
        MessageValidator.Validate(username, message);

        //Servern skickar meddelandet till alla anslutna klienter
        await Clients.All.ReceiveMessage(username, message);
    }
}