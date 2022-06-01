namespace NeverLate_api.Authentication;

public class PasswordRulesProvider
{
    public bool RequireDigit { get; set; }
    public int RequiredLength { get; set; }
    public bool RequireLowercase { get; set; }
    public bool RequireUppercase { get; set; }
    public bool RequireNonAlphanumeric { get; set; }
}