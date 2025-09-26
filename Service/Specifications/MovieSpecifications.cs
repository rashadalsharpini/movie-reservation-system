using Domain.Entities;
using Shared.Dtos;

namespace Service.Specifications;

public class MovieSpecifications : BaseSpecifications<Movie, Guid>
{
    public MovieSpecifications(MovieParameterSpecification parameterSpecification)
        : base(movie =>
            (string.IsNullOrWhiteSpace(parameterSpecification.Search) ||
             movie.Name.ToLower().Contains(parameterSpecification.Search.ToLower().Trim()))
            && (string.IsNullOrWhiteSpace(parameterSpecification.Genre) || 
                movie.Genres.Any(g => g.Name.ToLower() == parameterSpecification.Genre.ToLower().Trim()))
            && (!parameterSpecification.ExactReleaseDate.HasValue ||
                movie.ReleaseDate.Year == parameterSpecification.ExactReleaseDate.Value.Year)
            && (!parameterSpecification.MinReleaseDate.HasValue ||
                movie.ReleaseDate.Year <= parameterSpecification.MinReleaseDate.Value.Year)
            && (!parameterSpecification.MaxReleaseDate.HasValue ||
                movie.ReleaseDate.Year >= parameterSpecification.MaxReleaseDate.Value.Year)
            && (!parameterSpecification.Rating.HasValue ||
                movie.Rating == parameterSpecification.Rating))
    {
        AddInclude(m => m.Genres);
        AddInclude(m => m.Schedules);
        if (parameterSpecification.Sort is not null)
        {
            switch (parameterSpecification.Sort)
            {
                case MovieParameterSpecification.MovieSortOptions.NameAsc:
                    AddOrderBy(mn => mn.Name);
                    break;
                default:
                    AddOrderByDescending(mn => mn.Name);
                    break;
            }
        }
    }

    public MovieSpecifications(Guid id) : base(m => m.Id == id)
    {
        AddInclude(m => m.Genres);
        AddInclude(m => m.Schedules);
    }
}