namespace TestingMonitor.Api.Models;

public sealed class LoginResponse
{
    public string Token { get; set; } = null!;
    public DateTime Expires { get; set; }
    public string Username { get; set; } = null!;
}