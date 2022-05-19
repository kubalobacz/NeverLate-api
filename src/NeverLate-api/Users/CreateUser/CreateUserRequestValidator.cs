using FluentValidation;

namespace NeverLate_api.Users.CreateUser;

public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
{
    public CreateUserRequestValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(r => r.Email)
            .EmailAddress();
        RuleFor(r => r.Password)
            .MinimumLength(8)
            .Must(p => p.Any(char.IsDigit))
            .WithMessage("Password must contain at least 1 digit.");
    }
}