using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Shared.Dtos;

public class ResponseScheduleDto
{
    public int Id { get; set; }
    public DateTime ShowDateTime { get; set; }
    [Precision(10, 2)]
    public decimal BasePrice { get; set; }
    public Guid MovieId { get; set; }
    public int HallId { get; set; }
}