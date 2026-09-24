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

    const usernameElement = document.createElement("strong");
    usernameElement.textContent = username;

    li.appendChild(usernameElement);
    li.append(`: ${message}`);

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

    const username = usernameEl.value.trim();
    const message = messageEl.value.trim();

    if(!username){
        alert("Du måste ange ett användarnamn.");
        return;
    }
    if(!message){
        alert("Du måste skriva ett meddelande.");
        return;
    }
    if(username.length > 20){
        alert("Användarnamnet får vara högst 20 tecken.");
        return;
    }
    if(message.length > 500){
        alert("Meddelandet får vara högst 500 tecken.");
        return;
    }

    try{
        await connection.invoke(
            "SendMessage",
            username,
            message
        );

        messageEl.value = "";
        messageEl.focus();
    } catch (err) {
        alert(err.message);
    }
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
