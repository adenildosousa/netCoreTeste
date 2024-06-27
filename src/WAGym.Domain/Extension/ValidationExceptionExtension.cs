using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace WAGym.Domain.Extension
{
    public static class ValidationExtensions
    {
        public static ValidationProblemDetails ToProblemDetails(this ValidationException ex)
        {
            ValidationProblemDetails error = new ValidationProblemDetails
            {
                Title = ex.Message,
                Status = (int)HttpStatusCode.BadRequest
            };

            foreach (ValidationFailure validationFailure in ex.Errors)
            {
                if (error.Errors.ContainsKey(validationFailure.PropertyName))
                {
                    error.Errors[validationFailure.PropertyName] = error.Errors[validationFailure.PropertyName].Concat(new[] { validationFailure.ErrorMessage }).ToArray();
                    continue;
                }

                error.Errors.Add(new KeyValuePair<string, string[]>(validationFailure.PropertyName, new[] { validationFailure.ErrorMessage }));
            }

            return error;
        }
    }
}
