namespace TestingMonitor.Application.Exceptions;

/// <summary>
/// Exception inside an api.
/// </summary>
public sealed class ApiException(string message) : Exception(message);
