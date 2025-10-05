namespace Shared.Dtos;

public class ResponseSeatDto
{
    public int SeatId { get; set; } 
    public int SeatNumber { get; set; }
    public int SeatRow { get; set; }
    public string Status { get; set; } = null!;
}