using System.Linq.Expressions;
using Domain.Contracts;
using Domain.Entities;
using Shared.Dtos;

namespace Service.Specifications;

public class MovieCountSpecification:BaseSpecifications<Movie,Guid>
{
    public MovieCountSpecification(MovieParameterSpecification parameterSpecification)
        : base(movie =>
            (string.IsNullOrWhiteSpace(parameterSpecification.Search) ||
             movie.Name.ToLower().Contains(parameterSpecification.Search.ToLower().Trim()))
            && string.IsNullOrWhiteSpace(parameterSpecification.Genre) || movie.Genres.Any(g =>
                g.Name.ToLower() == parameterSpecification.Genre.ToLower().Trim())
            && (!parameterSpecification.ExactReleaseDate.HasValue ||
                movie.ReleaseDate.Year == parameterSpecification.ExactReleaseDate.Value.Year)
            && (!parameterSpecification.MinReleaseDate.HasValue ||
                movie.ReleaseDate.Year <= parameterSpecification.MinReleaseDate.Value.Year)
            && (!parameterSpecification.MaxReleaseDate.HasValue ||
                movie.ReleaseDate.Year >= parameterSpecification.MaxReleaseDate.Value.Year)
            && (!parameterSpecification.Rating.HasValue ||
                movie.Rating == parameterSpecification.Rating))
    {
    }
}