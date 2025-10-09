namespace Domain.Entities;

public class Booking:BaseEntity<int>
{
    public DateTime BookingDate { get; set; }=DateTime.UtcNow;
    public decimal TotalPrice { get; set; }
    public BookingStatus Status { get; set; }=BookingStatus.Pending;
    public string ConfirmationNumber { get; set; }
    public User User { get; set; } = null!;
    public string UserId { get; set; } 
    public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    public ICollection<Payment> Payments { get; set; } = new List<Payment>();
}

public enum BookingStatus
{
    Pending,
    Confirmed,
    Cancelled,
    Expired
}