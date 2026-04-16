using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Doctor_Appointment_System.Models;

public class Slot
{
    [Key]
    public int SlotId { get; set; }
    
    public int DoctorId { get; set; }
    
    [ForeignKey("DoctorId")]
    public Doctor? Doctor { get; set; }
    
    public DateOnly SlotDate { get; set; }
    
    public TimeOnly StartTime { get; set; }
    
    public TimeOnly EndTime { get; set; }
    
    public bool IsBooked { get; set; } = false;
}

