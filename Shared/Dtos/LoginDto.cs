using System.ComponentModel.DataAnnotations;

namespace Shared.Dtos;

public class LoginDto
{
    [EmailAddress] public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;
    public string DeviceId { get; set; } = null!;
}