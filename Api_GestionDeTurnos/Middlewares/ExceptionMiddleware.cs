using Api_GestionDeTurnos.Application.Exceptions;
using Api_GestionDeTurnos.Domain.Exceptions;
using Api_GestionDeTurnos.Infrastructure.Exceptions;

namespace Api_GestionDeTurnos.Middlewares
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
            catch (DomainException ex)
            {
                await HandleException(context, ex.Message, 400);
            }
            catch (InfrastructureException ex)
            {
                await HandleException(context, ex.Message, 400);
            }
            catch (ValidationException ex)
            {
                await HandleException(context, ex.Message, 422);
            }
            catch (ConflictException ex)
            {
                await HandleException(context, ex.Message, 409);
            }
            catch (Exception)
            {
                await HandleException(context, "Error interno del servidor", 500);
            }
        }
        private static async Task HandleException( HttpContext context, string message, int statusCode)
        {
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";
            var response = new
            {
                error = message
            };
            await context.Response.WriteAsJsonAsync(response);
        }
    } 
}
