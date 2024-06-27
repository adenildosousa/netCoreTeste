using Microsoft.AspNetCore.Http;
using System.Net;
using WAGym.Common.Exception;

namespace WAGym.Domain.Middleware
{
    public class BusinessExceptionMiddleware
    {
        private readonly RequestDelegate _request;

        public BusinessExceptionMiddleware(RequestDelegate request)
        {
            _request = request;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _request(context);
            }
            catch(BusinessException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await context.Response.WriteAsJsonAsync(ex.Message);
            }
        }
    }
}
