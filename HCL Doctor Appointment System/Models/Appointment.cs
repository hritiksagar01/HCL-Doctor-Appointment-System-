using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Doctor_Appointment_System.Models;

public class Appointment
{
    [Key]
    public int AppointmentId { get; set; }
    
    public int UserId { get; set; }
    
    [ForeignKey("UserId")]
    public User? User { get; set; }
    
    public int DoctorId { get; set; }
    
    [ForeignKey("DoctorId")]
    public Doctor? Doctor { get; set; }
    
    public int SlotId { get; set; }
    
    [ForeignKey("SlotId")]
    public Slot? Slot { get; set; }
    
    public string Mode { get; set; } = string.Empty; // Online, Offline
    
    public string Status { get; set; } = "Pending"; // Pending, Confirmed, Cancelled, Completed
    
    public DateTime BookingDate { get; set; } = DateTime.UtcNow;
    
    public string? MeetingLink { get; set; }
    
    public string? ClinicAddress { get; set; }
}

