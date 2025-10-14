using System.ComponentModel.DataAnnotations;

namespace Shared.Dtos;

public class UserDto
{
    [EmailAddress] public string Email { get; set; } = null!;
    public string DisplayName { get; set; } = null!;
    public string AccessToken { get; set; } = null!;
    public string RefreshToken { get; set; } = null!;
}