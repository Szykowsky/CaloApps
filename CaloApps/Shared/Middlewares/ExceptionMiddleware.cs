using FluentValidation;
using SendGrid.Helpers.Errors.Model;
using System.Text.Json;

namespace CaloApps.Middlewares.Shared
{
    public class ExceptionMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(context, e);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            var statusCode = GetStatusCode(exception);
            var response = new
            {
                title = GetTitle(exception),
                status = statusCode,
                detail = exception.Message,
                errors = GetErrors(exception)
            };
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = statusCode;
            await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
        private static int GetStatusCode(Exception exception) =>
            exception switch
            {
                BadRequestException => StatusCodes.Status400BadRequest,
                NotFoundException => StatusCodes.Status404NotFound,
                ValidationException => StatusCodes.Status422UnprocessableEntity,
                _ => StatusCodes.Status500InternalServerError
            };
        private static string GetTitle(Exception exception) =>
            exception switch
            {
                ApplicationException applicationException => applicationException.Message,
                BadRequestException => "Bad Request",
                ValidationException => "Validate Exception",
                _ => "Server Error"
            };
        private static IDictionary<string, string> GetErrors(Exception exception)
        {
            IDictionary<string, string> errors = null;
            if (exception is ValidationException validationException)
            {
                errors = validationException.Errors.ToDictionary(x => x.PropertyName, x => x.ErrorMessage);
            }
            return errors;
        }
    }
}
