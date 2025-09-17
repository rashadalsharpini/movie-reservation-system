using Domain.Entities;
using Shared.Dtos;

namespace Service.Specifications;

public class MovieSpecifications : BaseSpecifications<Movie,Guid>
{
    public MovieSpecifications(MovieParameterSpecification parameterSpecification) 
        :base(movie=>(string.IsNullOrWhiteSpace(parameterSpecification.Search)||movie.Name.ToLower().Contains(parameterSpecification.Search.ToLower().Trim())))
    {
        AddInclude(m => m.Genres);
        AddInclude(m => m.Schedules);
        AddOrderBy(m => m.Genres.OrderBy(g => g.Name).FirstOrDefault()!.Name);
    }

    public MovieSpecifications(Guid id) : base(m => m.Id == id)
    {
        AddInclude(m => m.Genres);
        AddInclude(m => m.Schedules);
    }
}