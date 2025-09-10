namespace Domain.Entities;

public class Schedules:BaseEntity<int>
{
    public DateTime ShowDateTime { get; set; }
    public Movie Movie { get; set; }
    public Guid  MovieId { get; set; }
    public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    public Hall Hall { get; set; }
    public int  HallId { get; set; }
}