using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configration;

public class SeatReservationConfiguration:IEntityTypeConfiguration<SeatReservation>
{
    public void Configure(EntityTypeBuilder<SeatReservation> builder)
    {
        // builder.HasOne(sr => sr.User)
        //     .WithMany(u => u.SeatReservations)
        //     .HasForeignKey(sr => sr.UserId);
        builder.HasOne(sr => sr.Schedule)
            .WithMany(s => s.SeatReservations)
            .HasForeignKey(sr => sr.ScheduleId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(sr=>sr.Seat)
            .WithMany(s=>s.SeatReservations)
            .HasForeignKey(sr=>sr.SeatId).OnDelete(DeleteBehavior.Restrict);
    }
}