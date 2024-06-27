using FluentValidation;
using WAGym.Common.Model.User.Request;
using WAGym.Data.Data;

namespace WAGym.Domain.Validation
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(x => x.PersonId)
                .NotNull()
                .NotEmpty()
                .GreaterThan(0)
                .WithName("PersonId");

            RuleFor(x => x.Username)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50)
                .WithName("Usuário");

            RuleFor(x => x.Password)
                .NotNull()
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.CompanyId)
                .NotNull()
                .NotEmpty()
                .GreaterThan(0);
        }
    }
}
