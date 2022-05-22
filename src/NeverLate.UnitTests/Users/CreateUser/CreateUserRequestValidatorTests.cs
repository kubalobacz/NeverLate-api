using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Validators;
using FluentValidation.Validators.UnitTestExtension.Composer;
using FluentValidation.Validators.UnitTestExtension.Core;
using FluentValidation.Validators.UnitTestExtension.ValidatorVerifiers;
using NeverLate_api.Authentication;
using NeverLate_api.Users.CreateUser;
using NeverLate_api.Validation;
using NeverLate_api.Validation.Validators;
using NUnit.Framework;

namespace NeverLate.UnitTests.Users.CreateUser;

//Move this test to integration testing since they are dependant on application configuration.
[TestFixture]
public class CreateUserRequestValidatorTests
{
    private CreateUserRequestValidator _validator;

    [SetUp]
    public void Setup()
    {
        _validator = new CreateUserRequestValidator();
    }

    [Test]
    public void Validator_PasswordLengthRule_SetupShouldMatchDomainRules()
    {
        var minimumLengthValidator = _validator.GetValidatorRulesForMember(r => r.Password)
            .Select(c => c.Validator)
            .OfType<MinimumLengthValidator<CreateUserRequest>>()
            .First();

        Assert.AreEqual(PasswordRulesProvider.RequiredLength, minimumLengthValidator.Min);
    }

    [Test]
    public void Validator_PasswordHasDigitRule_SetupShouldMatchDomainRules()
    {
        var minimumLengthValidator = _validator.GetValidatorRulesForMember(r => r.Password)
            .Select(c => c.Validator)
            .OfType<MustContainDigitValidator<CreateUserRequest>>()
            .FirstOrDefault();

        if (PasswordRulesProvider.RequireDigit)
        {
            Assert.NotNull(minimumLengthValidator);
        }
        else
        {
            Assert.Null(minimumLengthValidator);
        }
    }
}