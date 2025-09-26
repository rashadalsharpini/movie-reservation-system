using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

public class Schedule : BaseEntity<int>
{
    public DateTime ShowDateTime { get; set; }
    [Precision(10, 2)]
    public decimal BasePrice { get; set; }
    public Guid MovieId { get; set; }
    public int HallId { get; set; }
    public Movie Movie { get; set; } = null!;
    public Hall Hall { get; set; } = null!;
    public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}