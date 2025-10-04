namespace Domain.Entities;

public class Seat : BaseEntity<int>
{
    public int SeatNumber { get; set; }
    public int SeatRow { get; set; }

    public int HallId { get; set; }
    public Hall Hall { get; set; } = null!;
    public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    public ICollection<SeatReservation> SeatReservations { get; set; } = new List<SeatReservation>();
}