using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Exceptions;
public class NotFoundException : System.Exception
{
  public NotFoundException(string message) : base(message)
  {
  }

  public NotFoundException(string name, object key) : base($"Entity \"{name}\" ({key}) was not found.")
  {
  }
}

//public class CustomExceptionHandler
//    (ILogger<CustomExceptionHandler> logger)
//    : IExceptionHandler
//{
//  public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
//  {
//    logger.LogError(
//        "Error Message: {exceptionMessage}, Time of occurrence {time}",
//        exception.Message, DateTime.UtcNow);

//    (string Detail, string Title, int StatusCode) details = exception switch
//    {
//      InternalServerException =>
//      (
//          exception.Message,
//          exception.GetType().Name,
//          context.Response.StatusCode = StatusCodes.Status500InternalServerError
//      ),
//      ValidationException =>
//      (
//          exception.Message,
//          exception.GetType().Name,
//          context.Response.StatusCode = StatusCodes.Status400BadRequest
//      ),
//      BadRequestException =>
//      (
//          exception.Message,
//          exception.GetType().Name,
//          context.Response.StatusCode = StatusCodes.Status400BadRequest
//      ),
//      NotFoundException =>
//      (
//          exception.Message,
//          exception.GetType().Name,
//          context.Response.StatusCode = StatusCodes.Status404NotFound
//      ),
//      _ =>
//      (
//          exception.Message,
//          exception.GetType().Name,
//          context.Response.StatusCode = StatusCodes.Status500InternalServerError
//      )
//    };

//    var problemDetails = new ProblemDetails
//    {
//      Title = details.Title,
//      Detail = details.Detail,
//      Status = details.StatusCode,
//      Instance = context.Request.Path
//    };

//    problemDetails.Extensions.Add("traceId", context.TraceIdentifier);

//    if (exception is ValidationException validationException)
//    {
//      problemDetails.Extensions.Add("ValidationErrors", validationException.Errors);
//    }

//    await context.Response.WriteAsJsonAsync(problemDetails, cancellationToken: cancellationToken);
//    return true;
//  }
//}

//public class InternalServerException : Exception
//{
//  public InternalServerException(string message) : base(message)
//  {
//  }

//  public InternalServerException(string message, string details) : base(message)
//  {
//    Details = details;
//  }

//  public string? Details { get; }
//}
//public class BadRequestException : Exception
//{
//  public BadRequestException(string message) : base(message)
//  {
//  }

//  public BadRequestException(string message, string details) : base(message)
//  {
//    Details = details;
//  }

//  public string? Details { get; }
//}
