using System.ComponentModel.DataAnnotations;

namespace Doctor_Appointment_System.Models;

public class User
{
    [Key]
    public int UserId { get; set; }
    
    [Required]
    public string FullName { get; set; } = string.Empty;
    
    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;
    
    [Required]
    public string PasswordHash { get; set; } = string.Empty;
    
    public string? Phone { get; set; }
    
    [Required]
    public string Role { get; set; } = "Patient";
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}

