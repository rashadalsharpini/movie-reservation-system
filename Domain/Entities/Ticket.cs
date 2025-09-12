namespace Domain.Entities;

public class Ticket:BaseEntity<Guid>
{
    public decimal Price { get; set; }
    public bool IsPaid { get; set; }
    public Seat Seat { get; set; }= null!;
    public int SeatId { get; set; }
    public User User { get; set; }= null!;
    public string UserId { get; set; } = null!;
}