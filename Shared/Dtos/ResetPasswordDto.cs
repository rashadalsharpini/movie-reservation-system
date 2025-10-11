using System.ComponentModel.DataAnnotations;

namespace Shared.Dtos;

public class ResetPasswordDto
{
    [EmailAddress]
    public string Email { get; set; } = null!;
    public string Token { get; set; } = null!;
    public string NewPassword { get; set; } = null!;
}