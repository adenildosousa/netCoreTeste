using Microsoft.AspNetCore.Http;
using System.Net;

namespace WAGym.Domain.Middleware
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _request;

        public GlobalExceptionMiddleware(RequestDelegate request)
        {
            _request = request;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _request(context);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsJsonAsync(ex.Message);
            }
        }
    }
}
