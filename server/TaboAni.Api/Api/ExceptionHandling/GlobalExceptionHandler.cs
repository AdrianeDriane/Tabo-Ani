using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using TaboAni.Api.Application.DTOs.Response;
using TaboAni.Api.Domain.Exceptions;

namespace TaboAni.Api.ExceptionHandling;

public sealed class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger = logger;

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        var (statusCode, message, errors) = exception switch
        {
            DomainException domainException => (
                (int)domainException.StatusCode,
                ResolveDomainMessage(domainException),
                new[] { $"{domainException.Code}: {domainException.Message}" }),
            DbUpdateException dbUpdateException when ResolveUniqueConstraintViolation(dbUpdateException) is { } uniqueConstraintError => (
                StatusCodes.Status409Conflict,
                "The request conflicts with existing data.",
                new[] { uniqueConstraintError }),
            ArgumentException argumentException => (
                StatusCodes.Status400BadRequest,
                "Request validation failed.",
                new[] { argumentException.Message }),
            _ => (
                StatusCodes.Status500InternalServerError,
                "An unexpected error occurred.",
                new[] { "Please try again later." })
        };

        if (statusCode >= StatusCodes.Status500InternalServerError)
        {
            _logger.LogError(exception, "Unhandled server error.");
        }
        else
        {
            _logger.LogWarning(exception, "Handled request error.");
        }

        httpContext.Response.StatusCode = statusCode;

        await httpContext.Response.WriteAsJsonAsync(new ErrorResponseDto
        {
            Success = false,
            Message = message,
            Errors = errors
        }, cancellationToken);

        return true;
    }

    private static string ResolveDomainMessage(DomainException domainException)
    {
        return (int)domainException.StatusCode switch
        {
            StatusCodes.Status403Forbidden => "You do not have access to the requested resource.",
            StatusCodes.Status404NotFound => "The requested resource was not found.",
            StatusCodes.Status409Conflict => "The request conflicts with existing data.",
            _ => "Domain validation failed."
        };
    }

    private static string? ResolveUniqueConstraintViolation(DbUpdateException dbUpdateException)
    {
        if (dbUpdateException.InnerException is not PostgresException postgresException ||
            postgresException.SqlState != PostgresErrorCodes.UniqueViolation)
        {
            return null;
        }

        return postgresException.ConstraintName switch
        {
            "uq_users_email" => "signup.duplicate_credential: Email is already in use.",
            "uq_users_mobile_number" => "signup.duplicate_credential: Mobile number is already in use.",
            _ => "data.unique_conflict: The submitted data conflicts with an existing record."
        };
    }
}
