using Shared;

namespace Domain.Entities;

public class Ticket:BaseEntity<Guid>
{
    public decimal FinalPrice { get; set; }
    public bool IsPaid { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public TicketStatus Status { get; set; } = TicketStatus.Pending;
    
    public int SeatId { get; set; }
    public Seat Seat { get; set; } = null!;
    public string UserId { get; set; } = null!;
    public User User { get; set; } = null!;
    public int ScheduleId { get; set; }
    public Schedule Schedule { get; set; } = null!;
}