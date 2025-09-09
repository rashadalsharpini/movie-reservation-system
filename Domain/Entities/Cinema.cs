namespace Domain.Entities;

public class Cinema:BaseEntity<Guid>
{
    public string Name { get; set; }
    public string Location { get; set; }
}