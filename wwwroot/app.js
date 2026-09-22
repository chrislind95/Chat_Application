const statusEl = document.getElementById("status");
const messagesEl = document.getElementById("messages");
const form = document.getElementById("send-form");
const usernameEl = document.getElementById("username");
const messageEl = document.getElementById("message");

//Skapar en SignalR-anslutning till ChatHub på servern
const connection = new signalR.HubConnectionBuilder()
    .withUrl("/hubs/chat")
    .build();

// Används från konsolen i lab 2 när du har skapat anslutningen ovan.
window.connection = connection;

//Tar emot meddelanden från servern och visar dem i chatten
connection.on("ReceiveMessage", (username, message) => {
    addMessage(username, message);
});

function addMessage(username, message) {
    const li = document.createElement("li");
    // Bygger raden som HTML så att namnet kan visas i fetstil.
    li.innerHTML = `<strong>${username}</strong>: ${message}`;
    messagesEl.appendChild(li);
    li.scrollIntoView();
}

function setStatus(text, connected) {
    statusEl.textContent = text;
    statusEl.className = connected ? "status status--on" : "status status--off";
}

//Skickar användarnamn och meddelande till SignalR-hubben
form.addEventListener("submit", async (event) => {
    event.preventDefault();

    await connection.invoke(
        "SendMessage",
        usernameEl.value,
        messageEl.value
    );

    messageEl.value = "";
    messageEl.focus();
});

async function start() {
    if (!connection) {
        setStatus("Fyll i TODO:erna för att ansluta", false);
        return;
    }

    try {
        await connection.start();

        const connected = connection.state === signalR.HubConnectionState.Connected;
        setStatus(connected ? "Ansluten" : "Anslutningen är inte startad ännu", connected);
    } catch (err) {
        setStatus("Anslutning misslyckades", false);
        console.error(err);
    }
}

start();
