using System.Text.Json;
using Domain.Entities;

namespace ServiceTest;

public class JsonSerializationTest
{
    [Fact]
    public void MovieFactory_ShouldCreateConsistentGenreIds_ForSameGenreNames()
    {
        // Arrange & Act
        var movies1 = MovieFactory.CreateFakeMovie();
        var movies2 = MovieFactory.CreateFakeMovie();

        // Assert
        var actionGenre1 = movies1[0].Genres.First(g => g.Name == "Action");
        var actionGenre2 = movies2[0].Genres.First(g => g.Name == "Action");
        
        var comedyGenre1 = movies1[0].Genres.First(g => g.Name == "Comedy");
        var comedyGenre2 = movies2[0].Genres.First(g => g.Name == "Comedy");

        // Same genre names should have same IDs for consistency
        Assert.Equal(actionGenre1.Id, actionGenre2.Id);
        Assert.Equal(comedyGenre1.Id, comedyGenre2.Id);
        
        // Well-known IDs should match the predefined constants
        Assert.Equal(MovieFactory.ActionGenreId, actionGenre1.Id);
        Assert.Equal(MovieFactory.ComedyGenreId, comedyGenre1.Id);
    }

    [Fact]
    public void MovieFactory_ShouldCreateUniqueMovieIds()
    {
        // Arrange & Act
        var movies = MovieFactory.CreateFakeMovie();

        // Assert
        Assert.Equal(2, movies.Count);
        Assert.NotEqual(movies[0].Id, movies[1].Id);
        
        // Each movie should have its own schedules
        Assert.All(movies[0].Schedules, s => Assert.Equal(movies[0].Id, s.MovieId));
        Assert.All(movies[1].Schedules, s => Assert.Equal(movies[1].Id, s.MovieId));
    }

    [Fact]
    public void MovieWithGenres_ShouldSerializeAndDeserializeWithConsistentIds()
    {
        // Arrange
        var originalMovies = MovieFactory.CreateFakeMovie();
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };

        // Act - Serialize to JSON (simulating saving to file)
        var json = JsonSerializer.Serialize(originalMovies, options);
        
        // Deserialize from JSON (simulating loading from file)  
        var deserializedMovies = JsonSerializer.Deserialize<List<Movie>>(json, options);

        // Assert
        Assert.NotNull(deserializedMovies);
        Assert.Equal(originalMovies.Count, deserializedMovies.Count);

        for (int i = 0; i < originalMovies.Count; i++)
        {
            var original = originalMovies[i];
            var deserialized = deserializedMovies[i];

            // Movie properties should be preserved
            Assert.Equal(original.Id, deserialized.Id);
            Assert.Equal(original.Name, deserialized.Name);
            Assert.Equal(original.Genres.Count, deserialized.Genres.Count);

            // Genre IDs should be preserved (this is what was broken before)
            foreach (var originalGenre in original.Genres)
            {
                var deserializedGenre = deserialized.Genres.First(g => g.Name == originalGenre.Name);
                Assert.Equal(originalGenre.Id, deserializedGenre.Id);
            }

            // Schedule relationships should be preserved
            Assert.All(deserialized.Schedules, s => Assert.Equal(deserialized.Id, s.MovieId));
        }
    }

    [Fact]
    public void GenreIdConstants_ShouldBeWellKnownValues()
    {
        // Assert - These GUIDs are deterministic and can be used in JSON files
        Assert.Equal(Guid.Parse("11111111-1111-1111-1111-111111111111"), MovieFactory.ActionGenreId);
        Assert.Equal(Guid.Parse("22222222-2222-2222-2222-222222222222"), MovieFactory.ComedyGenreId);
        Assert.Equal(Guid.Parse("33333333-3333-3333-3333-333333333333"), MovieFactory.SadGenreId);
    }
}