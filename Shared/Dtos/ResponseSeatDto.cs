namespace Shared.Dtos;

public class ResponseSeatDto
{
    public int SeatNumber { get; set; }
    public int SeatRow { get; set; }
    public bool IsAvailable { get; set; } = true;
}