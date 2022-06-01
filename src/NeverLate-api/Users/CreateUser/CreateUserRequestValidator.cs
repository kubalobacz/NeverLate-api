using FluentValidation;
using Microsoft.Extensions.Options;
using NeverLate_api.Authentication;
using NeverLate_api.FluentValidation.Validators.Validators;

namespace NeverLate_api.Users.CreateUser;

public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
{
    public CreateUserRequestValidator(IOptions<PasswordRulesProvider> passwordRulesProviderOptions)
    {
        var passwordRulesProvider = passwordRulesProviderOptions.Value;
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(r => r.Email)
            .EmailAddress();
        RuleFor(r => r.Password)
            .MinimumLength(passwordRulesProvider.RequiredLength);

        if (passwordRulesProvider.RequireDigit)
        {
            RuleFor(r => r.Password)
                .SetValidator(new MustContainDigitValidator<CreateUserRequest>())
                .WithMessage("Password must contain at least 1 digit.");
        }
        if (passwordRulesProvider.RequireLowercase)
        {
            RuleFor(r => r.Password)
                .SetValidator(new MustContainLowercaseValidator<CreateUserRequest>())
                .WithMessage("Password must contain lowercase letter.");
        }
        if (passwordRulesProvider.RequireUppercase)
        {
            RuleFor(r => r.Password)
                .SetValidator(new MustContainUppercaseValidator<CreateUserRequest>())
                .WithMessage("Password must contain uppercase letter.");
        }

        if (passwordRulesProvider.RequireNonAlphanumeric)
        {
            RuleFor(r => r.Password)
                .SetValidator(new MustContainNonAlphanumericValidator<CreateUserRequest>())
                .WithMessage("Password must contain non alphanumeric sign");
        }
    }
}