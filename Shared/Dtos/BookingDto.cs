using Domain.Entities;

namespace Shared.Dtos;

public class BookingDto
{
    
        public int BookingId { get; set; }
        public string ConfirmationNumber { get; set; } = null!;
        public decimal TotalPrice { get; set; }
        public BookingStatus Status { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime ShowDateTime { get; set; }
        public string MovieName { get; set; } = null!;
        public string CinemaName { get; set; } = null!;
        public string HallName { get; set; } = null!;
        public int TicketCount { get; set; }
        public string? UserEmail { get; set; } 
}