using FluentValidation;
using FluentValidation.Validators;

namespace NeverLate_api.FluentValidation.Validators.Validators;

public class MustContainUppercaseValidator<T> : PropertyValidator<T,string>
{
    public override string Name => "MustContainUppercaseValidator";
    
    public override bool IsValid(ValidationContext<T> context, string value)
    {
        return value.Any(char.IsUpper);
    }
}