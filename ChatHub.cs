using Microsoft.AspNetCore.SignalR;

public sealed class ChatHub : Hub<IChatClient>
{
    //SignalR Hub som tar emot meddlenaden från klienten
    //och skickar dem vidare till alla anslutna klienter 
    public async Task SendMessage(string username, string message)
    {
        //Kontrollerar att användarnman och meddelande innehåller något
        if (string.IsNullOrWhiteSpace(username))
            throw new HubException("Användarnamn måste anges.");

        if (string.IsNullOrWhiteSpace(message))
            throw new HubException("Meddelandet får inte vara tomt.");

        //Begränsar längden på användarnamn och meddelande från klienten
        if (username.Length > 20)
            throw new HubException("Användarnamnet får vara högst 20 tecken");

        if (message.Length > 500)
            throw new HubException("Meddelandet får vara högst 500 tecken.");


        //Servern skickar meddelandet till alla anslutna klienter
        await Clients.All.ReceiveMessage(username, message);
    }
}