namespace NeverLate_api.Authentication;

public static class PasswordRulesProvider
{
    public const bool RequireDigit = true;
    public const int RequiredLength = 8;
    public const bool RequireLowercase = false;
    public const bool RequireUppercase = false;
    public const bool RequireNonAlphanumeric = false;
}