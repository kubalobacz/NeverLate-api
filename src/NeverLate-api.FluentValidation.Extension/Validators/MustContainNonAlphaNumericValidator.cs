using FluentValidation;
using FluentValidation.Validators;

namespace NeverLate_api.FluentValidation.Validators.Validators;

public class MustContainNonAlphanumericValidator<T> : PropertyValidator<T,string>
{
    public override string Name => "MustContainNonAlphanumericValidator";
    
    public override bool IsValid(ValidationContext<T> context, string value)
    {
        return value.Any(c => !char.IsLetterOrDigit(c));
    }
}