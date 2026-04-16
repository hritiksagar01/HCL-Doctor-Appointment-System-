using Doctor_Appointment_System.Data;
using Doctor_Appointment_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Doctor_Appointment_System.Services;

public class SpecialtyService : ISpecialtyService
{
    private readonly ApplicationDbContext _context;
    public SpecialtyService(ApplicationDbContext context) => _context = context;

    public async Task<List<Specialty>> GetAllAsync() => await _context.Specialties.ToListAsync();
    public async Task<Specialty?> GetByIdAsync(int id) => await _context.Specialties.FindAsync(id);

    public async Task<Specialty> CreateAsync(Specialty specialty)
    {
        _context.Specialties.Add(specialty);
        await _context.SaveChangesAsync();
        return specialty;
    }

    public async Task<Specialty?> UpdateAsync(int id, Specialty specialty)
    {
        var existing = await _context.Specialties.FindAsync(id);
        if (existing == null) return null;
        existing.SpecialtyName = specialty.SpecialtyName;
        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _context.Specialties.FindAsync(id);
        if (entity == null) return false;
        _context.Specialties.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }
}

