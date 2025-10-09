using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class User : IdentityUser
{
    [MaxLength(100)] public string DisplayName { get; set; } = null!;
   // public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
   // public ICollection<SeatReservation> SeatReservations { get; set; } = new List<SeatReservation>();
}