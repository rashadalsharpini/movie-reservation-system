namespace Domain.Entities;

public class Schedules:BaseEntity<int>
{
    public DateTime ShowDateTime { get; set; }
    public Movie Movie { get; set; }
    public int  MovieId { get; set; }
    public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    public Hall hall { get; set; }
    public int  hallId { get; set; }
}