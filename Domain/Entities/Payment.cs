namespace Domain.Entities;

public class Payment:BaseEntity<Guid>
{
    
    public Booking Booking { get; set; } = null!;
    public int BookingId { get; set; }
}