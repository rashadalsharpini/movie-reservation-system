namespace Domain.Entities;

public class SeatReservation:BaseEntity<Guid>
{
    public string TemporaryId { get; set; } = null!;
    public DateTime ReservationDate { get; set; }
    public DateTime ExpirationDate { get; set; }
    public Seat Seat { get; set; } = null!;
    public int SeatId { get; set; }
    public Schedule Schedule { get; set; } = null!;
    public int ScheduleId { get; set; }
    public User User { get; set; } = null!;
    public string UserId { get; set; }
    
}