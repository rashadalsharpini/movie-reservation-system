using Domain.Entities;
using Shared.Dtos;

namespace Service.Specifications;

public class GenreSpecification : BaseSpecifications<Genre,Guid>
{
    public GenreSpecification(MovieParameterSpecification parameterSpecification) 
        :base(movie=>(string.IsNullOrWhiteSpace(parameterSpecification.Search)||movie.Name.ToLower().Contains(parameterSpecification.Search.ToLower().Trim())))
    {
        AddInclude(g=>g.Movies);
    }
}