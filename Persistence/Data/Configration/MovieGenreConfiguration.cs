using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configration;

public class MovieGenreConfiguration:IEntityTypeConfiguration<MovieGenre>
{
    public void Configure(EntityTypeBuilder<MovieGenre> builder)
    {
        builder.HasKey(mg => new { mg.GenreId, mg.MovieId });
        builder.HasOne(mg => mg.Movie)
            .WithMany()
            .HasForeignKey(mg => mg.MovieId);

        builder.HasOne(mg => mg.Genre)
            .WithMany()
            .HasForeignKey(mg => mg.GenreId);
        
    }
}