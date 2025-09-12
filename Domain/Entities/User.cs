using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class User : IdentityUser
{
    [MaxLength(100)] public string DisplayName { get; set; } = null!;
}