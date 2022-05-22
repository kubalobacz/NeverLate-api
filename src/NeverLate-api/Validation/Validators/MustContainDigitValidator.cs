using FluentValidation;
using FluentValidation.Validators;

namespace NeverLate_api.Validation.Validators;

public class MustContainDigitValidator<T> : PropertyValidator<T,string>
{
    public override string Name => "MustContainDigitValidator";
    
    public override bool IsValid(ValidationContext<T> context, string value)
    {
        return value.Any(char.IsDigit);
    }
}
