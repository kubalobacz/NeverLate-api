using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NeverLate_api.Persistence.Database;

public class NeverLateContext : IdentityDbContext<IdentityUser>
{
    public NeverLateContext(DbContextOptions<NeverLateContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Ignore<IdentityRole>();
        builder.Ignore<IdentityUserToken<string>>();
        builder.Ignore<IdentityUserRole<string>>();
        builder.Ignore<IdentityUserLogin<string>>();
        builder.Ignore<IdentityUserClaim<string>>();
        builder.Ignore<IdentityRoleClaim<string>>();
        builder.Entity<IdentityUser>().Ignore(c => c.ConcurrencyStamp)
            .Ignore(c => c.EmailConfirmed)
            .Ignore(c => c.LockoutEnabled)
            .Ignore(c => c.LockoutEnd)
            .Ignore(c => c.NormalizedEmail)
            .Ignore(c => c.PhoneNumber)
            .Ignore(c => c.SecurityStamp)
            .Ignore(c => c.AccessFailedCount)
            .Ignore(c => c.NormalizedUserName)
            .Ignore(c => c.PhoneNumberConfirmed)
            .Ignore(c => c.TwoFactorEnabled);
    }
}