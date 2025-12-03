using Microsoft.Extensions.Logging;
using System;

namespace Abyat.Core.Exceptions;

/// <summary>
/// Exception that is thrown when an entity cannot be found in the data source.
/// Provides additional contextual details such as the entity name, identifier,
/// schema, and table when available.
/// </summary>
[Serializable]
public class NotFoundException : Exception
{
    private readonly ILogger _logger;
    public bool IsConcurrencyConflict { get; }

    /// <summary>
    /// Gets the name of the entity that was not found.
    /// </summary>
    public string EntityName { get; }

    /// <summary>
    /// Gets the identifier of the entity that was not found.
    /// </summary>
    public object? EntityId { get; }

    /// <summary>
    /// Gets the schema where the entity was expected.
    /// </summary>
    public string? Schema { get; }

    /// <summary>
    /// Gets the table where the entity was expected.
    /// </summary>
    public string? Table { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="NotFoundException"/> class
    /// with a default error message ("Entity not found.").
    /// </summary>
    public NotFoundException() : base("Entity not found.") { }

    /// <summary>
    /// Initializes a new instance of the <see cref="NotFoundException"/> class
    /// with a specified error message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public NotFoundException(string? message) : base(message) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="NotFoundException"/> class
    /// with a specified entity name and identifier.
    /// </summary>
    /// <param name="name">The name of the entity that was not found.</param>
    /// <param name="id">The identifier of the entity that was not found.</param>
    public NotFoundException(string name, object id)
        : base($"Entity '{name}' with Id '{id}' was not found.")
    {
        EntityName = name;
        EntityId = id;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="NotFoundException"/> class
    /// with a specified entity name, identifier, schema, and table.
    /// </summary>
    /// <param name="name">The name of the entity that was not found.</param>
    /// <param name="id">The identifier of the entity that was not found.</param>
    /// <param name="schema">The schema where the entity was expected.</param>
    /// <param name="table">The table where the entity was expected.</param>
    public NotFoundException(string name, object id, string schema, string table)
        : base($"Entity '{name}' with Id '{id}' was not found in schema '{schema}', table '{table}'.")
    {
        EntityName = name;
        EntityId = id;
        Schema = schema;
        Table = table;
    }

    public NotFoundException(ILogger logger, Exception ex, string customMessage = "", bool isConcurrencyConflict = false) : base(customMessage, ex)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        IsConcurrencyConflict = isConcurrencyConflict;

        if (ex == null)
        {
            throw new ArgumentNullException(nameof(ex));
        }

        var methodName = new System.Diagnostics.StackTrace().GetFrame(1)?.GetMethod()?.Name ?? "UnknownMethod";

        customMessage = string.IsNullOrWhiteSpace(customMessage) ? "Data access layer exception occurred." : customMessage;

        var customException = new Exception(customMessage, ex);

        _logger.LogError(methodName, customException);

    }

    /// <summary>
    /// Initializes a new instance of the <see cref="NotFoundException"/> class
    /// with a specified error message and a reference to the inner exception
    /// that is the cause of this exception.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">The exception that caused the current exception.</param>
    public NotFoundException(string? message, Exception? innerException)
        : base(message, innerException) { }
}