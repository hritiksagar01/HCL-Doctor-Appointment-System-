using Doctor_Appointment_System.Models;

namespace Doctor_Appointment_System.Services;

public interface ISpecialtyService
{
    Task<List<Specialty>> GetAllAsync();
    Task<Specialty?> GetByIdAsync(int id);
    Task<Specialty> CreateAsync(Specialty specialty);
    Task<Specialty?> UpdateAsync(int id, Specialty specialty);
    Task<bool> DeleteAsync(int id);
}

