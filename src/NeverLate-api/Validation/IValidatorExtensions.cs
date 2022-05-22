using System.Linq.Expressions;
using FluentValidation;
using FluentValidation.Internal;

namespace NeverLate_api.Validation;

public static class IValidatorExtensions
{
    public static IEnumerable<IRuleComponent> GetValidatorRulesForMember<T, TProperty>(
        this IValidator<T> validator, Expression<Func<T, TProperty>> expression)
    {
        var descriptor = validator.CreateDescriptor();
        var expressionMemberName = expression.GetMember()?.Name;

        return descriptor.GetValidatorsForMember(expressionMemberName).Select(t => t.Options);
    }
}