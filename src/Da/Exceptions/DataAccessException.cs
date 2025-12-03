using Microsoft.Extensions.Logging;
using Utils.FileActions;

namespace Abyat.Da.Exceptions;

/// <summary>
/// Custom exception class for data access layer exceptions with built-in logging.
/// </summary>
public class DataAccessException : Exception
{
    private readonly ILogger _logger;
    public bool IsConcurrencyConflict { get; }

    /// <summary>
    /// Initializes a new instance of the DataAccessException class.
    /// </summary>
    /// <param name="logger">The LogHelper instance for logging.</param>
    /// <param name="ex">The original exception that occurred.</param>
    /// <param name="customMessage">Custom message to include in the log.</param>
    /// <param name="isConcurrencyConflict">Flag indicating if this is a concurrency conflict</param>
    public DataAccessException(ILogger logger, Exception ex, string customMessage = "", bool isConcurrencyConflict = false) : base(customMessage, ex)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        IsConcurrencyConflict = isConcurrencyConflict;

        if (ex == null)
        {
            throw new ArgumentNullException(nameof(ex));
        }

        var methodName = new System.Diagnostics.StackTrace().GetFrame(1)?.GetMethod()?.Name ?? "UnknownMethod";

        customMessage = string.IsNullOrWhiteSpace(customMessage) ? "Data access layer exception occurred." : customMessage;

        Exception? customException = new Exception(customMessage, ex);

        _logger.LogError(methodName, customException);
        FileHelper.ErrorLogger(customException, true, true);
    }

}