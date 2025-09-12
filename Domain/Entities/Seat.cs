namespace Domain.Entities;

public class Seat:BaseEntity<int>
{
    public int SeatNumber { get; set; }
    public int SeatRow { get; set; }
    public Hall Hall { get; set; }= null!;
    public int HallId { get; set; }
}