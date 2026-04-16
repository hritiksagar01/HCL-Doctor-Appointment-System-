using Doctor_Appointment_System.Data;
using Doctor_Appointment_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Doctor_Appointment_System.Services;

public class SlotService : ISlotService
{
    private readonly ApplicationDbContext _context;
    public SlotService(ApplicationDbContext context) => _context = context;

    public async Task<List<Slot>> GetAllAsync() =>
        await _context.Slots.Include(s => s.Doctor).ToListAsync();

    public async Task<List<Slot>> GetByDoctorIdAsync(int doctorId) =>
        await _context.Slots.Include(s => s.Doctor)
            .Where(s => s.DoctorId == doctorId && !s.IsBooked)
            .ToListAsync();

    public async Task<Slot?> GetByIdAsync(int id) =>
        await _context.Slots.Include(s => s.Doctor).FirstOrDefaultAsync(s => s.SlotId == id);

    public async Task<Slot> CreateAsync(Slot slot)
    {
        _context.Slots.Add(slot);
        await _context.SaveChangesAsync();
        return slot;
    }

    public async Task<Slot?> UpdateAsync(int id, Slot slot)
    {
        var existing = await _context.Slots.FindAsync(id);
        if (existing == null) return null;
        existing.DoctorId = slot.DoctorId;
        existing.SlotDate = slot.SlotDate;
        existing.StartTime = slot.StartTime;
        existing.EndTime = slot.EndTime;
        existing.IsBooked = slot.IsBooked;
        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _context.Slots.FindAsync(id);
        if (entity == null) return false;
        _context.Slots.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }
}

