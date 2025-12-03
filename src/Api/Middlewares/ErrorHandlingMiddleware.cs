using Abyat.Core.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;

namespace Abyat.Api.Middlewares;

/// <summary>
/// Middleware for handling exceptions globally within the application.
/// Converts exceptions into standardized <see cref="ProblemDetails"/> responses
/// according to RFC 7807.
/// </summary>
/// <param name="logger">The logger used for writing exception details.</param>
public class ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger) : IMiddleware
{
    private static readonly Dictionary<Type, (HttpStatusCode statusCode, string title, string type)> ExceptionMappings = new()
    {
        [typeof(NotFoundException)] = (HttpStatusCode.NotFound, "Entity not found", "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.4"),
        [typeof(UnauthorizedAccessException)] = (HttpStatusCode.Unauthorized, "Unauthorized", "https://datatracker.ietf.org/doc/html/rfc7235#section-3.1"),
        [typeof(ValidationException)] = (HttpStatusCode.BadRequest, "Validation failed", "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1"),
        [typeof(ArgumentException)] = (HttpStatusCode.BadRequest, "Invalid argument", "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1"),
        [typeof(InvalidOperationException)] = (HttpStatusCode.BadRequest, "Invalid operation", "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1")
    };

    /// <summary>
    /// Invokes the middleware to process the current HTTP request and handle any unhandled exceptions.
    /// </summary>
    /// <param name="context">The current <see cref="HttpContext"/>.</param>
    /// <param name="next">The next <see cref="RequestDelegate"/> in the pipeline.</param>
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    /// <summary>
    /// Handles different types of exceptions and converts them to appropriate HTTP responses.
    /// </summary>
    /// <param name="context">The current <see cref="HttpContext"/>.</param>
    /// <param name="exception">The exception to handle.</param>
    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var problemDetails = CreateProblemDetails(context, exception);
        var statusCode = GetStatusCodeFromProblemDetails(problemDetails);

        LogException(exception, statusCode);

        await WriteProblemDetailsAsync(context, problemDetails, statusCode);
    }

    /// <summary>
    /// Creates a <see cref="ProblemDetails"/> object based on the exception type.
    /// </summary>
    /// <param name="context">The current <see cref="HttpContext"/>.</param>
    /// <param name="exception">The exception to convert.</param>
    /// <returns>A <see cref="ProblemDetails"/> object.</returns>
    private static ProblemDetails CreateProblemDetails(HttpContext context, Exception exception)
    {
        return exception switch
        {
            NotFoundException ex => CreateTypedProblemDetails(context, ex, ExceptionMappings[typeof(NotFoundException)]),

            UnauthorizedAccessException ex => CreateTypedProblemDetails(context, ex, ExceptionMappings[typeof(UnauthorizedAccessException)],
                "You do not have permission to perform this action."),

            ValidationException ex => CreateValidationProblemDetails(context, ex),

            ArgumentNullException ex when ex.ParamName == "format" => CreateTypedProblemDetails(context, ex,
                (HttpStatusCode.BadRequest, "Validation format error", "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1")),

            ArgumentException ex => CreateTypedProblemDetails(context, ex, ExceptionMappings[typeof(ArgumentException)]),

            InvalidOperationException ex => CreateTypedProblemDetails(context, ex, ExceptionMappings[typeof(InvalidOperationException)]),

            _ => CreateGenericProblemDetails(context, exception)
        };
    }

    /// <summary>
    /// Creates a typed <see cref="ProblemDetails"/> for known exception types.
    /// </summary>
    private static ProblemDetails CreateTypedProblemDetails(HttpContext context, Exception exception, (HttpStatusCode statusCode, string title, string type) mapping, string? customDetail = null)
    {
        return new ProblemDetails
        {
            Status = (int)mapping.statusCode,
            Title = mapping.title,
            Detail = customDetail ?? exception.Message,
            Type = mapping.type,
            Instance = context.TraceIdentifier
        };
    }

    /// <summary>
    /// Creates a <see cref="ValidationProblemDetails"/> for validation exceptions.
    /// </summary>
    private static ProblemDetails CreateValidationProblemDetails(HttpContext context, ValidationException exception)
    {
        var mapping = ExceptionMappings[typeof(ValidationException)];

        return new ValidationProblemDetails
        {
            Status = (int)mapping.statusCode,
            Title = mapping.title,
            Detail = exception.Message,
            Type = mapping.type,
            Instance = context.TraceIdentifier
        };
    }

    /// <summary>
    /// Creates a generic <see cref="ProblemDetails"/> for unhandled exceptions.
    /// </summary>
    private static ProblemDetails CreateGenericProblemDetails(HttpContext context, Exception exception)
    {
        return new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = "An unexpected error occurred",
            Detail = "An internal server error has occurred. Please try again later.",
            Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1",
            Instance = context.TraceIdentifier
        };
    }

    /// <summary>
    /// Extracts the HTTP status code from a <see cref="ProblemDetails"/> object.
    /// </summary>
    private static HttpStatusCode GetStatusCodeFromProblemDetails(ProblemDetails problemDetails)
    {
        return (HttpStatusCode)(problemDetails.Status ?? StatusCodes.Status500InternalServerError);
    }

    /// <summary>
    /// Logs the exception with appropriate log level based on status code.
    /// </summary>
    private void LogException(Exception exception, HttpStatusCode statusCode)
    {
        var logLevel = statusCode switch
        {
            HttpStatusCode.InternalServerError => LogLevel.Error,
            HttpStatusCode.Unauthorized => LogLevel.Warning,
            HttpStatusCode.NotFound => LogLevel.Information,
            _ => LogLevel.Warning
        };

        logger.Log(logLevel, exception, "Exception occurred: {ExceptionType} - {Message}",
            exception.GetType().Name, exception.Message);
    }

    /// <summary>
    /// Writes a <see cref="ProblemDetails"/> object as a JSON response to the client.
    /// </summary>
    /// <param name="context">The current <see cref="HttpContext"/>.</param>
    /// <param name="problemDetails">The <see cref="ProblemDetails"/> to return.</param>
    /// <param name="statusCode">The HTTP status code for the response.</param>
    private static async Task WriteProblemDetailsAsync(HttpContext context, ProblemDetails problemDetails, HttpStatusCode statusCode)
    {
        if (context.Response.HasStarted)
            return;

        context.Response.Clear();
        context.Response.StatusCode = (int)statusCode;
        context.Response.ContentType = "application/problem+json";

        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = false
        };

        await context.Response.WriteAsJsonAsync(problemDetails, options);
    }
}