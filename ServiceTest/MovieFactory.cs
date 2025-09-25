using Domain.Entities;

namespace ServiceTest;

public static class MovieFactory
{
    // Predefined genre IDs that can be reused for consistent relationships
    public static readonly Guid ActionGenreId = Guid.Parse("11111111-1111-1111-1111-111111111111");
    public static readonly Guid ComedyGenreId = Guid.Parse("22222222-2222-2222-2222-222222222222");
    public static readonly Guid SadGenreId = Guid.Parse("33333333-3333-3333-3333-333333333333");

    public static List<Movie> CreateFakeMovie(Guid? id = null)
    {
        // Create a single movie if a specific ID is provided
        if (id.HasValue)
        {
            return new List<Movie>() { CreateSingleMovie(id.Value) };
        }

        // Create multiple movies with different IDs
        var movie1Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
        var movie2Id = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb");

        var fakeMovie = new List<Movie>()
        {
            CreateSingleMovie(movie1Id, "Movie 1", "Description 1", 120, 9.3M, new DateTime(2000, 1, 1), new[] { "Action", "Comedy" }),
            CreateSingleMovie(movie2Id, "Movie 2", "Description 2", 140, 8.3M, new DateTime(2001, 1, 1), new[] { "Sad", "Comedy" })
        };
        return fakeMovie;
    }

    private static Movie CreateSingleMovie(Guid movieId, string name = "Test Movie", string description = "Test Description", 
        int durationMinutes = 120, decimal rating = 8.0M, DateTime? releaseDate = null, string[]? genreNames = null)
    {
        genreNames ??= new[] { "Action", "Comedy" };
        releaseDate ??= new DateTime(2000, 1, 1);

        var movie = new Movie()
        {
            Id = movieId,
            Name = name,
            Description = description,
            DurationMinutes = durationMinutes,
            Rating = rating,
            ReleaseDate = releaseDate.Value,
            Genres = CreateGenresFromNames(genreNames),
            Schedules = new List<Schedule>()
            {
                new Schedule()
                {
                    Id = 0, // Let EF Core generate the ID
                    BasePrice = 150M,
                    MovieId = movieId,
                    ShowDateTime = new DateTime(2024, 1, 1, 12, 0, 0)
                },
                new Schedule()
                {
                    Id = 0, // Let EF Core generate the ID
                    BasePrice = 150M,
                    MovieId = movieId,
                    ShowDateTime = new DateTime(2024, 1, 2, 11, 0, 0)
                },
            }
        };

        return movie;
    }

    private static List<Genre> CreateGenresFromNames(string[] genreNames)
    {
        var genres = new List<Genre>();
        foreach (var genreName in genreNames)
        {
            var genreId = genreName.ToLower() switch
            {
                "action" => ActionGenreId,
                "comedy" => ComedyGenreId,
                "sad" => SadGenreId,
                _ => Guid.NewGuid() // For unknown genres, still generate a new GUID
            };

            genres.Add(new Genre()
            {
                Id = genreId,
                Name = genreName
            });
        }
        return genres;
    }
}