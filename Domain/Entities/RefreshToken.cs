namespace Domain.Entities;

public class RefreshToken
{
    public Guid Id { get; set; }
    public string Token { get; set; } = null!;
    public DateTime Expires { get; set; }
    public User User { get; set; } = null!;
    public string UserId { get; set; } = null!;
}