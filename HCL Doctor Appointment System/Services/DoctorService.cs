using Doctor_Appointment_System.Data;
using Doctor_Appointment_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Doctor_Appointment_System.Services;

public class DoctorService : IDoctorService
{
    private readonly ApplicationDbContext _context;
    public DoctorService(ApplicationDbContext context) => _context = context;

    public async Task<List<Doctor>> GetAllAsync() =>
        await _context.Doctors.Include(d => d.Specialty).ToListAsync();

    public async Task<Doctor?> GetByIdAsync(int id) =>
        await _context.Doctors.Include(d => d.Specialty).FirstOrDefaultAsync(d => d.DoctorId == id);

    public async Task<Doctor> CreateAsync(Doctor doctor)
    {
        _context.Doctors.Add(doctor);
        await _context.SaveChangesAsync();
        return doctor;
    }

    public async Task<Doctor?> UpdateAsync(int id, Doctor doctor)
    {
        var existing = await _context.Doctors.FindAsync(id);
        if (existing == null) return null;
        existing.DoctorName = doctor.DoctorName;
        existing.SpecialtyId = doctor.SpecialtyId;
        existing.Experience = doctor.Experience;
        existing.Fees = doctor.Fees;
        existing.Mode = doctor.Mode;
        existing.Email = doctor.Email;
        existing.ClinicAddress = doctor.ClinicAddress;
        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _context.Doctors.FindAsync(id);
        if (entity == null) return false;
        _context.Doctors.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }
}

