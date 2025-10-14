using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configration;

public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.HasKey(r => r.Id);
        builder.Property(r => r.Token).HasMaxLength(200).IsRequired();
        builder.HasIndex(r => r.Token).IsUnique();
        builder.HasOne(r => r.User).WithMany(u => u.RefreshTokens).HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}