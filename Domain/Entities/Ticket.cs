using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

public class Ticket : BaseEntity<Guid>
{
    [Precision(10, 2)] public decimal Price { get; set; }
    public int SeatId { get; set; }
    public Seat Seat { get; set; } = null!;
    public int ScheduleId { get; set; }
    public Schedule Schedule { get; set; } = null!;
    public Booking Booking { get; set; } = null!;
    public int BookingId { get; set; }
    
}