using System.Linq.Expressions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Service.Specifications;

public class BookingSpecifications:BaseSpecifications<Booking,int>
{
    public BookingSpecifications(int id) : base(b => b.Id == id)
    {
        AddIncludeExpressions(query=>query.Include(b=>b.Tickets)
            .ThenInclude(t=>t.Schedule)
            .ThenInclude(s=>s.Movie));
        AddIncludeExpressions(query=>query.Include(b=>b.Tickets)
            .ThenInclude(t=>t.Schedule)
            .ThenInclude(s=>s.Hall)
            .ThenInclude(h=>h.Cinema));
        AddIncludeExpressions(query => query.Include(b => b.Tickets)
            .ThenInclude(t => t.Seat));
    }
}