using System.ComponentModel.DataAnnotations;

namespace Doctor_Appointment_System.Models;

public class Specialty
{
    [Key]
    public int SpecialtyId { get; set; }
    
    [Required]
    public string SpecialtyName { get; set; } = string.Empty;
    
    public ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();
}

