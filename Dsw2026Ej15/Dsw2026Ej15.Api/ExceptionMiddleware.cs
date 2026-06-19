
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;

namespace Dsw2026Ej15.Api
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            HttpStatusCode statusCode;
            string message;

          
            if (exception is ValidationException)
            {
                statusCode = HttpStatusCode.BadRequest;
                message = exception.Message;
            }
          
            else
            {
                statusCode = HttpStatusCode.InternalServerError;
                message = "Ocurrió un error inesperado en el servidor.";
            }

            context.Response.StatusCode = (int)statusCode;

            
            var response = new { error = message };
            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
