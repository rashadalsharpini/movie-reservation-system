using Domain.Entities;
using Service.Specifications;

namespace ServiceTest;

public class MovieSpecificationsTests
{
    [Fact]
    public void MovieSpecifications_ByGenre_ShouldIncludeGenresAndSchedules()
    {
        //Arrange
        var movies = new List<Movie>()
        {
            new Movie()
            {
                Id = Guid.NewGuid(),
                Name = "A Movie",
                Genres = new List<Genre>()
                {
                    new Genre() { Id = Guid.NewGuid(), Name = "Action" },
                    new Genre() { Id = Guid.NewGuid(), Name = "Comedy" }
                }
            },

            new Movie()
            {
                Id = Guid.NewGuid(),
                Name = "C Movie",
                Genres = new List<Genre>()
                {
                    new Genre() { Id = Guid.NewGuid(), Name = "Drama" },
                    new Genre() { Id = Guid.NewGuid(), Name = "Action" }
                }
            },

            new Movie()
            {
                Id = Guid.NewGuid(),
                Name = "Z Movie",
                Genres = new List<Genre>()
                {
                    new Genre() { Id = Guid.NewGuid(), Name = "Comedy" },
                    new Genre() { Id = Guid.NewGuid(), Name = "Drama" }
                }
            },
        };
        //Act
        var spec = new MovieSpecifications("Action");
        var orderedMovies = movies.AsQueryable().Where(spec.Criteria!).OrderBy(spec.OrderBy!);
        //Assert
        Assert.Equal(2, orderedMovies.Count());
        Assert.Equal("A Movie", orderedMovies.First().Name);
        Assert.Equal("C Movie", orderedMovies.Last().Name);
    }

    [Fact]
    public void MovieSpecifications_ById_ShouldFilterAndIncludeRelations()
    {
        // Arrange
        var targetId = Guid.NewGuid();
        var movies = new List<Movie>
        {
            new Movie
            {
                Id = targetId,
                Name = "Target Movie",
                Genres = new List<Genre> { new Genre { Id = Guid.NewGuid(), Name = "Action" } },
                Schedules = new List<Schedule> { new Schedule { Id = 555, ShowDateTime = DateTime.Now } }
            },
            new Movie
            {
                Id = Guid.NewGuid(),
                Name = "Other Movie",
                Genres = new List<Genre> { new Genre { Id = Guid.NewGuid(), Name = "Drama" } }
            }
        }.AsQueryable();

        var spec = new MovieSpecifications(targetId);

        // Act
        var filtered = movies.Where(spec.Criteria!).ToList();

        // Assert - filtering
        Assert.Single(filtered);
        Assert.Equal("Target Movie", filtered[0].Name);

        // Assert - includes
        Assert.Contains(spec.Includes!, include => include.Body.ToString().Contains("Genres"));
        Assert.Contains(spec.Includes!, include => include.Body.ToString().Contains("Schedules"));
    }
}