using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configration;

public class GenreConfigration:IEntityTypeConfiguration<Genre>
{
    public void Configure(EntityTypeBuilder<Genre> builder)
    {
        builder.HasMany(g => g.Movies)
            .WithMany(m => m.Genres)
            .UsingEntity<MovieGenre>(
                j => j
                    .HasOne(mg => mg.Movie)
                    .WithMany()
                    .HasForeignKey(mg => mg.MovieId),
                j => j
                    .HasOne(mg => mg.Genre)
                    .WithMany()
                    .HasForeignKey(mg => mg.GenreId),
                j =>
                {
                    j.HasKey(mg => new { mg.MovieId, mg.GenreId });
                    j.ToTable("MovieGenres");
                }
            );
    }
}