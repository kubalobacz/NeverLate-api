using System.Linq;
using FluentValidation.Validators;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Options;
using NeverLate_api.Authentication;
using NeverLate_api.FluentValidation.Validators.Extensions;
using NeverLate_api.FluentValidation.Validators.Validators;
using NeverLate_api.Users.CreateUser;
using NUnit.Framework;

namespace NeverLate.IntegrationTests.Users.CreateUser;

[TestFixture]
public class CreateUserRequestValidatorTests
{
    private WebApplicationFactory<Program> _webApplicationFactory;
    private PasswordRulesProvider _passwordRulesProvider;
    private CreateUserRequestValidator _validator;

    [SetUp]
    public void Setup()
    {
        _webApplicationFactory = new WebApplicationFactory<Program>();
        var passwordRulesProviderOptions = (IOptions<PasswordRulesProvider>) _webApplicationFactory.Services.GetService(typeof(IOptions<PasswordRulesProvider>));
        _passwordRulesProvider = passwordRulesProviderOptions.Value;
        _validator = new CreateUserRequestValidator(passwordRulesProviderOptions);
    }
    
    [Test]
    public void Validator_PasswordLengthRule_SetupShouldMatchConfigurationRules()
    {
        var minimumLengthValidator = _validator.GetValidatorRulesForMember(r => r.Password)
            .Select(c => c.Validator)
            .OfType<MinimumLengthValidator<CreateUserRequest>>()
            .First();

        Assert.AreEqual(_passwordRulesProvider.RequiredLength, minimumLengthValidator.Min);
    }
    
    [Test]
    public void Validator_PasswordHasDigitRule_SetupShouldMatchConfigurationRules()
    {
        var mustContainDigitValidator = _validator.GetValidatorRulesForMember(r => r.Password)
            .Select(c => c.Validator)
            .OfType<MustContainDigitValidator<CreateUserRequest>>()
            .FirstOrDefault();

        if (_passwordRulesProvider.RequireDigit)
        {
            Assert.NotNull(mustContainDigitValidator);
        }
        else
        {
            Assert.Null(mustContainDigitValidator);
        }
    }
    
    [Test]
    public void Validator_PasswordHasLowercase_SetupShouldMatchConfigurationRules()
    {
        var mustContainLowercaseValidator = _validator.GetValidatorRulesForMember(r => r.Password)
            .Select(c => c.Validator)
            .OfType<MustContainLowercaseValidator<CreateUserRequest>>()
            .FirstOrDefault();

        if (_passwordRulesProvider.RequireLowercase)
        {
            Assert.NotNull(mustContainLowercaseValidator);
        }
        else
        {
            Assert.Null(mustContainLowercaseValidator);
        }
    }
    
    [Test]
    public void Validator_PasswordHasUppercase_SetupShouldMatchConfigurationRules()
    {
        var mustContainUppercaseValidator = _validator.GetValidatorRulesForMember(r => r.Password)
            .Select(c => c.Validator)
            .OfType<MustContainUppercaseValidator<CreateUserRequest>>()
            .FirstOrDefault();

        if (_passwordRulesProvider.RequireUppercase)
        {
            Assert.NotNull(mustContainUppercaseValidator);
        }
        else
        {
            Assert.Null(mustContainUppercaseValidator);
        }
    }
    
    [Test]
    public void Validator_PasswordHasNonAlphanumeric_SetupShouldMatchConfigurationRules()
    {
        var mustContainNonAlphanumericValidator = _validator.GetValidatorRulesForMember(r => r.Password)
            .Select(c => c.Validator)
            .OfType<MustContainNonAlphanumericValidator<CreateUserRequest>>()
            .FirstOrDefault();
        

        if (_passwordRulesProvider.RequireNonAlphanumeric)
        {
            Assert.NotNull(mustContainNonAlphanumericValidator);
        }
        else
        {
            Assert.Null(mustContainNonAlphanumericValidator);
        }
    }
}