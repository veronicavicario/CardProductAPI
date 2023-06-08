using CardProductAPI.Commons.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ValidationException = FluentValidation.ValidationException;

namespace CardProductAPI.Commons.Filters;

public class CardProductExceptionFilter : ExceptionFilterAttribute
{
    private readonly ILogger<CardProductExceptionFilter> _logger;
    
    public CardProductExceptionFilter(ILogger<CardProductExceptionFilter> logger)
    {
        _logger = logger;
    }
    
    public override void OnException(ExceptionContext context)
    {
        switch (context.Exception)
        {
            case NotFoundException notFoundEx:
                HandleNotFoundException(context, notFoundEx);
                break;
            case ValidationException validationEx:
                HandleValidationException(context, validationEx);
                break;
            default:
                HandleUnknownException(context);
                break;
        }

        base.OnException(context);
    }
    
    private static void HandleValidationException(ExceptionContext context, FluentValidation.ValidationException exception)
    {
        var details = new ValidationProblemDetails()
        {
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            Title = "Validation fail",
            Detail = exception.Message
            
        };

        context.Result = new BadRequestObjectResult(details);

        context.ExceptionHandled = true;
    }
    
    private void HandleNotFoundException(ExceptionContext context, NotFoundException exception)
    {
        var details = new ProblemDetails()
        {
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
            Title = "The specified resource was not found.",
            Detail = exception.Message
        };

        context.Result = new NotFoundObjectResult(details);

        context.ExceptionHandled = true;
    }

    private void HandleUnknownException(ExceptionContext context)
    {
        var details = new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = "An error occurred while processing your request.",
            Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
            Detail = context.Exception.Message
        };

        context.Result = new ObjectResult(details)
        {
            StatusCode = StatusCodes.Status500InternalServerError
        };

        context.ExceptionHandled = true;
    }
}