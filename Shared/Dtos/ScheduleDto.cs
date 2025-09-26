using Microsoft.EntityFrameworkCore;

namespace Shared.Dtos;

public class ScheduleDto
{
    public DateTime ShowDateTime { get; set; }
    [Precision(10, 2)]
    public decimal BasePrice { get; set; }
}