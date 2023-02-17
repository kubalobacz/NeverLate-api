using Microsoft.EntityFrameworkCore;
using NeverLate_api.Persistence.Database;

namespace NeverLate.IntegrationTests;

public class TestNeverLateContext : NeverLateContext
{
    public TestNeverLateContext(DbContextOptions<NeverLateContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}