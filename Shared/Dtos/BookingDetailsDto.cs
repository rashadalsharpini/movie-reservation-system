using Domain.Entities;

namespace Shared.Dtos;

public class BookingDetailsDto
{
    
    public int BookingId { get; set; }
    public string ConfirmationNumber { get; set; } = null!;
    public decimal TotalPrice { get; set; }
    public BookingStatus Status { get; set; }
    public DateTime BookingDate { get; set; }
    
  
    public DateTime ShowDateTime { get; set; }
    public string MovieName { get; set; } = null!;
    public string MovieDescription { get; set; } = null!;
    public int DurationMinutes { get; set; }
    

    public string CinemaName { get; set; } = null!;
    public string CinemaLocation { get; set; } = null!;
    public string HallName { get; set; } = null!;
    public List<TicketDto> Tickets { get; set; } = new List<TicketDto>();
}

public class TicketDto
{
    public Guid TicketId { get; set; }
    public int SeatNumber { get; set; }
    public int SeatRow { get; set; }
    public decimal Price { get; set; }
}