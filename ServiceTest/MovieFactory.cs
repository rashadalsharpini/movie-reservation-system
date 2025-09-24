using Domain.Entities;

namespace ServiceTest;

public static class MovieFactory
{
    public static List<Movie> CreateFakeMovie(Guid? id = null)
    {
        var movieId = id ?? Guid.NewGuid();
        var fakeMovie = new List<Movie>()
        {
            new Movie()
            {
                Id = movieId,
                Name = "Movie 1",
                Description = "Description 1",
                DurationMinutes = 120,
                Genres = new List<Genre>()
                {
                    new Genre()
                    {
                        Id = Guid.NewGuid(),
                        Name = "Action"
                    },
                    new Genre()
                    {
                        Id = Guid.NewGuid(),
                        Name = "Comedy"
                    }
                },
                Rating = 9.3M,
                ReleaseDate = new DateTime(2000, 1, 1),
                Schedules = new List<Schedule>()
                {
                    new Schedule()
                    {
                        BasePrice = 150M,
                        Id = 1,
                        MovieId = movieId,
                        ShowDateTime = new DateTime(2024, 1, 1, 12, 0, 0)
                    },
                    new Schedule()
                    {
                        BasePrice = 150M,
                        Id = 2,
                        MovieId = movieId,
                        ShowDateTime = new DateTime(2024, 1, 2, 11, 0, 0)
                    },
                }
            },
            
            new Movie()
            {
                Id = movieId,
                Name = "Movie 2",
                Description = "Description 2",
                DurationMinutes = 140,
                Genres = new List<Genre>()
                {
                    new Genre()
                    {
                        Id = Guid.NewGuid(),
                        Name = "Sad"
                    },
                    new Genre()
                    {
                        Id = Guid.NewGuid(),
                        Name = "Comedy"
                    }
                },
                Rating = 8.3M,
                ReleaseDate = new DateTime(2001, 1, 1),
                Schedules = new List<Schedule>()
                {
                    new Schedule()
                    {
                        BasePrice = 150M,
                        Id = 3,
                        MovieId = movieId,
                        ShowDateTime = new DateTime(2024, 1, 1, 12, 0, 0)
                    },
                    new Schedule()
                    {
                        BasePrice = 150M,
                        Id = 4,
                        MovieId = movieId,
                        ShowDateTime = new DateTime(2024, 1, 2, 11, 0, 0)
                    },
                }
            },
        };
        return fakeMovie;
    }
}