using Microsoft.AspNetCore.Server.Kestrel.Core;
var builder = WebApplication.CreateBuilder(args);

//Använder HTTP/1.1 för att göra WebSocket-handshaken tydlig under utveckling och testning
builder.WebHost.ConfigureKestrel(kestrel =>
    kestrel.ConfigureEndpointDefaults(endpoint => endpoint.Protocols = HttpProtocols.Http1));

//Registrerar SignalR i applikationens dependecy injection-container
builder.Services.AddSignalR();

var app = builder.Build();

app.UseDefaultFiles();   // Gör att / serverar wwwroot/index.html
app.UseStaticFiles();    // Serverar wwwroot/ (html, js, css)

//Gör ChatHub tillgänglig för SignalR-klienter på denna URL
app.MapHub<ChatHub>("/hubs/chat");

app.Run();
