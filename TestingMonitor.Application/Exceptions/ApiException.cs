using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingMonitor.Application.Exceptions;

/// <summary>
/// Ошибка api.
/// </summary>
public sealed class ApiException(string message) : Exception(message);
