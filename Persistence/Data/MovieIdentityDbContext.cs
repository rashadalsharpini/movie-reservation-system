using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Persistence.Data.Configration;

namespace Persistence.Data;

public class MovieIdentityDbContext(DbContextOptions<MovieIdentityDbContext> opts):IdentityDbContext<User>(opts)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<User>().ToTable("Users");
        builder.Entity<IdentityRole>().ToTable("Roles");
        builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
        builder.Entity<RefreshToken>().ToTable("RefreshTokens");
        builder.ApplyConfiguration(new RefreshTokenConfiguration());
    }
}