using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configration;

public class ScheduleConfiguration:IEntityTypeConfiguration<Schedules>
{
    public void Configure(EntityTypeBuilder<Schedules> builder)
    {
        builder.HasOne(s => s.Movie).WithMany();
        builder.HasOne(s => s.hall).WithMany();
        builder.HasMany(t => t.Tickets).WithOne();
    }
}