using System.Net;
using System.Text.Json;

namespace TherapyCenter2.Middleware
{
    public class GlobalExceptionMiddleware
    {

        private readonly RequestDelegate _next;

        public GlobalExceptionMiddleware(RequestDelegate next)
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

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;

            if (ex.Message.Contains("not found", StringComparison.OrdinalIgnoreCase))
            {
                statusCode = HttpStatusCode.NotFound;
            }
            else if (ex.Message.Contains("invalid", StringComparison.OrdinalIgnoreCase) ||
                     ex.Message.Contains("exists", StringComparison.OrdinalIgnoreCase))
            {
                statusCode = HttpStatusCode.BadRequest;
            }

            var response = new
            {
                success = false,
                message = ex.Message,
                statusCode = (int)statusCode
            };

            var jsonResponse = JsonSerializer.Serialize(response);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            return context.Response.WriteAsync(jsonResponse);
        }
    }
}
