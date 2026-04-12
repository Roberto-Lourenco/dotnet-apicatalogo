using APICatalogo.WebAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace APICatalogo.Middleware;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly IHostEnvironment _env;
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(IHostEnvironment env, ILogger<GlobalExceptionHandler> logger)
    {
        _env = env;
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        _logger.LogError(exception, "Unexpected Error: {Message}", exception.Message);

        var uow = httpContext.RequestServices.GetService<IUnitOfWork>();

        if (uow != null)
        {
            try
            {
                await uow.RollbackAsync(cancellationToken);
                _logger.LogInformation("Transaction rollback executed successfully in the GlobalExceptionHandler.");
            }
            catch (Exception rollbackEx)
            {
                _logger.LogCritical(rollbackEx, "Critical failure while attempting to execute the transaction rollback in the GlobalExceptionHandler.");
            }
        }

        var problemDetails = new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = "Internal Server Error",
            Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1",
            Instance = httpContext.Request.Path
        };

        if (_env.IsDevelopment())
        {
            problemDetails.Detail = exception.Message;
            problemDetails.Extensions["traceId"] = httpContext.TraceIdentifier;
            problemDetails.Extensions["stackTrace"] = exception.StackTrace;
        }
        else
        {
            problemDetails.Detail = "An internal error occurred while processing your request. Please contact support.";
            problemDetails.Extensions["traceId"] = httpContext.TraceIdentifier;
        }

        httpContext.Response.StatusCode = problemDetails.Status.Value;

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}