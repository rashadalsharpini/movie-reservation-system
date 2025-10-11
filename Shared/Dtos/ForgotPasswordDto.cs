using System.ComponentModel.DataAnnotations;

namespace Shared.Dtos;

public class ForgotPasswordDto
{
    [EmailAddress]
    public string Email { get; set; } = null!;
}