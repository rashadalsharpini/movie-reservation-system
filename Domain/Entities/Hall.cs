namespace Domain.Entities;

public class Hall:BaseEntity<int>
{
    public string Name { get; set; }
    public int Capacity { get; set; }
    public Cinema Cinema { get; set; }
    public int cinemaId { get; set; }
}