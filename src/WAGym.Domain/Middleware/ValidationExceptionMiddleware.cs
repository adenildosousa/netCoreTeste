using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WAGym.Domain.Extension;

namespace WAGym.Domain.Middleware
{
    public class ValidationExceptionMiddleware
    {
        private readonly RequestDelegate _request;

        public ValidationExceptionMiddleware(RequestDelegate request)
        {
            _request = request;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _request(context);
            }
            catch (ValidationException exception)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ValidationProblemDetails error = exception.ToProblemDetails();
                await context.Response.WriteAsJsonAsync(error);
            }
        }
    }
}
