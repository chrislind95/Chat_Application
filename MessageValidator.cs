using System.Diagnostics;

public static class MessageValidator
{
    public static void Validate(string username, string message)
    {
        if (string.IsNullOrWhiteSpace(username))
            throw new ArgumentException("Användarnamn måste anges.");

        if (string.IsNullOrWhiteSpace(message))
            throw new ArgumentException("Meddelanadet får inte vara tomt.");

        if (username.Length > 20)
            throw new ArgumentException("Användarnamnet får vara högst 20 tecken.");

        if (message.Length > 500)
            throw new ArgumentException("Meddelandet får vara högst 500 tecken.");
    }
}