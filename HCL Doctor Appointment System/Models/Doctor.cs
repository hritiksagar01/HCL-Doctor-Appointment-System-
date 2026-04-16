using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Doctor_Appointment_System.Models;

public class Doctor
{
    [Key]
    public int DoctorId { get; set; }
    
    [Required]
    public string DoctorName { get; set; } = string.Empty;
    
    public int SpecialtyId { get; set; }
    
    [ForeignKey("SpecialtyId")]
    public Specialty? Specialty { get; set; }
    
    public int Experience { get; set; }
    
    [Column(TypeName = "decimal(10,2)")]
    public decimal Fees { get; set; }
    
    public string Mode { get; set; } = string.Empty; // Online, Offline, Both
    
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    
    public string? ClinicAddress { get; set; }
    
    public ICollection<Slot> Slots { get; set; } = new List<Slot>();
    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}

