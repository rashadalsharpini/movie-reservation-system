using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configration;

public class SeatConfiguration:IEntityTypeConfiguration<Seat>
{
    public void Configure(EntityTypeBuilder<Seat> builder)
    {
        builder.HasOne(s => s.Hall)
            .WithMany(h => h.Seats)
            .HasForeignKey(s => s.HallId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}