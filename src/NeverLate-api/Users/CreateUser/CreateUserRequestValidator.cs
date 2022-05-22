using FluentValidation;
using NeverLate_api.Authentication;
using NeverLate_api.Validation.Validators;

namespace NeverLate_api.Users.CreateUser;

public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
{
    public CreateUserRequestValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(r => r.Email)
            .EmailAddress();
        RuleFor(r => r.Password)
            .MinimumLength(PasswordRulesProvider.RequiredLength)
            .SetValidator(new MustContainDigitValidator<CreateUserRequest>())
            .WithMessage("Password must contain at least 1 digit.");
    }
}