namespace Domain.Entities;

public class Payment:BaseEntity<Guid>
{
    
    public Booking Booking { get; set; } = null!;
    public Guid BookingId { get; set; }
}