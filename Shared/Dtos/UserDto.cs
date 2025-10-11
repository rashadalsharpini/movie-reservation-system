using System.ComponentModel.DataAnnotations;

namespace Shared.Dtos;

public class UserDto
{
    [EmailAddress]
    public string Email { get; set; } = null!;
    public string DisplayName { get; set; } = null!;
    public string Token { get; set; } = null!;
}