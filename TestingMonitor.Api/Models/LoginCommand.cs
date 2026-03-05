namespace TestingMonitor.Api.Models;

public sealed class LoginCommand
{
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
}