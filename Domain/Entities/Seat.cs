namespace Domain.Entities;

public class Seat : BaseEntity<int>
{
    public int SeatNumber { get; set; }
    public int SeatRow { get; set; }
    public bool IsAvailable { get; set; } = true;

    public int HallId { get; set; }
    public Hall Hall { get; set; } = null!;
    public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}