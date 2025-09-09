namespace Domain.Entities;

public class BaseEntity<TKey>
{
    public required TKey Id { get; set; }
}
