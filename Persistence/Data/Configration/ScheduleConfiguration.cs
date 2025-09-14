using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configration;

public class ScheduleConfiguration:IEntityTypeConfiguration<Schedule>
{
    public void Configure(EntityTypeBuilder<Schedule> builder)
    {
        builder.HasOne(s => s.Movie)
            .WithMany(m => m.Schedules)
            .HasForeignKey(s => s.MovieId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(s => s.Hall)
            .WithMany(h => h.Schedules)
            .HasForeignKey(s => s.HallId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}