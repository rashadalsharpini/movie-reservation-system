using Domain.Entities;

namespace Shared.Dtos;

public class CreateBookingDto
{
    public DateTime BookingDate { get; set; }=DateTime.UtcNow;
    public decimal TotalPrice { get; set; }
    public BookingStatus Status { get; set; }=BookingStatus.Pending;
    public string ConfirmationNumber { get; set; }
  
    public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    public ICollection<Payment> Payments { get; set; } = new List<Payment>();
}