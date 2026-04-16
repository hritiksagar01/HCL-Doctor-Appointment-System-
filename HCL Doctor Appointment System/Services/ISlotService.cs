using Doctor_Appointment_System.Models;

namespace Doctor_Appointment_System.Services;

public interface ISlotService
{
    Task<List<Slot>> GetAllAsync();
    Task<List<Slot>> GetByDoctorIdAsync(int doctorId);
    Task<Slot?> GetByIdAsync(int id);
    Task<Slot> CreateAsync(Slot slot);
    Task<Slot?> UpdateAsync(int id, Slot slot);
    Task<bool> DeleteAsync(int id);
}

