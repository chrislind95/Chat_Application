using System.Reflection;
using System.Reflection.Metadata;
using Xunit;

public class MessageValidatorTests
{
    [Fact]
    public void Validate_EmptyUsername_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() =>
            MessageValidator.Validate("", "Hej"));
    }

    [Fact]
    public void Validate_UsernameTooLong_ThrowsArgumentException()
    {
        string username = new string('a', 21);

        Assert.Throws<ArgumentException>(() =>
            MessageValidator.Validate(username, "Hej"));
    }

    [Fact]
    public void Validate_MessageTooLong_ThrowsArgumentException()
    {
        string message = new string('a', 501);

        Assert.Throws<ArgumentException>(() =>
            MessageValidator.Validate("Alice", message));
    }

    [Fact]
    public void Validate_ValidInput_DoesNotThrow()
    {
        MessageValidator.Validate("Christian", "Hejsan");
    }
}