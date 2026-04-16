using Doctor_Appointment_System.Data;
using Doctor_Appointment_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Doctor_Appointment_System.Services;

public class AppointmentService : IAppointmentService
{
    private readonly ApplicationDbContext _context;
    public AppointmentService(ApplicationDbContext context) => _context = context;

    public async Task<List<Appointment>> GetAllAsync() =>
        await _context.Appointments
            .Include(a => a.User)
            .Include(a => a.Doctor).ThenInclude(d => d!.Specialty)
            .Include(a => a.Slot)
            .ToListAsync();

    public async Task<Appointment?> GetByIdAsync(int id) =>
        await _context.Appointments
            .Include(a => a.User)
            .Include(a => a.Doctor).ThenInclude(d => d!.Specialty)
            .Include(a => a.Slot)
            .FirstOrDefaultAsync(a => a.AppointmentId == id);

    public async Task<List<Appointment>> GetByUserIdAsync(int userId) =>
        await _context.Appointments
            .Include(a => a.Doctor).ThenInclude(d => d!.Specialty)
            .Include(a => a.Slot)
            .Where(a => a.UserId == userId)
            .OrderByDescending(a => a.BookingDate)
            .ToListAsync();

    public async Task<Appointment> BookAsync(Appointment appointment)
    {
        var slot = await _context.Slots.FindAsync(appointment.SlotId);
        if (slot == null || slot.IsBooked)
            throw new InvalidOperationException("Slot is not available.");

        var doctor = await _context.Doctors.FindAsync(appointment.DoctorId);

        slot.IsBooked = true;
        appointment.BookingDate = DateTime.UtcNow;
        appointment.Status = "Confirmed";

        if (appointment.Mode == "Online")
        {
            var slug = doctor?.DoctorName.ToLower().Replace(" ", "-") ?? "doctor";
            appointment.MeetingLink = $"https://meet.stoiccare.app/{slug}-{slot.SlotId}";
        }
        else if (appointment.Mode == "Offline" && doctor != null)
        {
            appointment.ClinicAddress = doctor.ClinicAddress;
        }

        _context.Appointments.Add(appointment);
        await _context.SaveChangesAsync();
        return appointment;
    }

    public async Task<Appointment?> UpdateStatusAsync(int id, string status)
    {
        var existing = await _context.Appointments.FindAsync(id);
        if (existing == null) return null;
        existing.Status = status;
        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> CancelAsync(int id)
    {
        var appointment = await _context.Appointments.FindAsync(id);
        if (appointment == null) return false;

        appointment.Status = "Cancelled";
        var slot = await _context.Slots.FindAsync(appointment.SlotId);
        if (slot != null) slot.IsBooked = false;

        await _context.SaveChangesAsync();
        return true;
    }
}

