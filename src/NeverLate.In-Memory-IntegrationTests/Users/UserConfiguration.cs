using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NeverLate.IntegrationTests.Users;

public class UserConfiguration : IEntityTypeConfiguration<IdentityUser>
{
    public void Configure(EntityTypeBuilder<IdentityUser> builder)
    {
        var passwordHasher = new PasswordHasher<IdentityUser>();
        var usersArray = new IdentityUser[]
        {
            new()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "test_user_1",
                Email = "test_user_1@test.com",
                NormalizedEmail = "TEST_USER_1@TEST.COM",
                PasswordHash = passwordHasher.HashPassword(null, "password123")
            }
        };
        builder.HasData(usersArray);
    }
}