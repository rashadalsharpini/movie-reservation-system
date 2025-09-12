namespace Domain.Entities;

public class Schedules : BaseEntity<int>
{
    public DateTime ShowDateTime { get; set; }
    public Movie Movie { get; set; } = null!;
    public Guid MovieId { get; set; }
    public ICollection<Ticket> Tickets { get; set; } = [];
    public Hall Hall { get; set; } = null!;
    public int HallId { get; set; }
}