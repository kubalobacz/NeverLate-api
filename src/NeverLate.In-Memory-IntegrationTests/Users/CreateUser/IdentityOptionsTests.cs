using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Options;
using NeverLate_api.Authentication;
using NUnit.Framework;

namespace NeverLate.IntegrationTests.Users.CreateUser;

[TestFixture]
public class IdentityOptionsTests
{
    private WebApplicationFactory<Program> _webApplicationFactory;
    private IdentityOptions _identityOptions;
    private PasswordRulesProvider _passwordRulesProvider;

    [SetUp]
    public void Setup()
    {
        _webApplicationFactory = new WebApplicationFactory<Program>();
        _identityOptions = ((IOptions<IdentityOptions>) _webApplicationFactory.Services.GetService(typeof(IOptions<IdentityOptions>))!).Value;
        var passwordRulesProviderOptions = (IOptions<PasswordRulesProvider>) _webApplicationFactory.Services.GetService(typeof(IOptions<PasswordRulesProvider>))!;
        _passwordRulesProvider = passwordRulesProviderOptions.Value;
    }

    [Test]
    public void IdentityOptions_PasswordHasDigitRule_ShouldMatchConfigurationRules()
    {
        var identityRequireDigit = _identityOptions.Password.RequireDigit;
        Assert.AreEqual(_passwordRulesProvider.RequireDigit, identityRequireDigit);
    }
    
    [Test]
    public void IdentityOptions_PasswordLengthRule_ShouldMatchConfigurationRules()
    {
        var identityRequiredLength = _identityOptions.Password.RequiredLength;
        Assert.AreEqual(_passwordRulesProvider.RequiredLength, identityRequiredLength);
    }
    
    [Test]
    public void IdentityOptions_PasswordHasLowercaseRule_ShouldMatchConfigurationRules()
    {
        var identityRequireLowercase = _identityOptions.Password.RequireLowercase;
        Assert.AreEqual(_passwordRulesProvider.RequireLowercase, identityRequireLowercase);
    }
    
    [Test]
    public void IdentityOptions_PasswordHasUppercaseRule_ShouldMatchConfigurationRules()
    {
        var identityRequireUppercase = _identityOptions.Password.RequireLowercase;
        Assert.AreEqual(_passwordRulesProvider.RequireUppercase, identityRequireUppercase);
    }
    
    [Test]
    public void IdentityOptions_PasswordHasNonAlphanumericRule_ShouldMatchConfigurationRules()
    {
        var identityRequireNonAlphanumeric = _identityOptions.Password.RequireNonAlphanumeric;
        Assert.AreEqual(_passwordRulesProvider.RequireNonAlphanumeric, identityRequireNonAlphanumeric);
    }
}