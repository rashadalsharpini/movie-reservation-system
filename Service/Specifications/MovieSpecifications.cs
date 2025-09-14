using Domain.Entities;

namespace Service.Specifications;

public class MovieSpecifications : BaseSpecifications<Movie, Guid>
{
    public MovieSpecifications(string genre) : base(m => m.Genres.Any(g => g.Name == genre))
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