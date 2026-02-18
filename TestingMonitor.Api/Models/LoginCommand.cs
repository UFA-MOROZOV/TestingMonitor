namespace TestingMonitor.Api.Models;

public sealed class LoginCommand
{
    public string Username { get; set; }
    public string Password { get; set; }
}