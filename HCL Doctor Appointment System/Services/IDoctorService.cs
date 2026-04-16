using Doctor_Appointment_System.Models;

namespace Doctor_Appointment_System.Services;

public interface IDoctorService
{
    Task<List<Doctor>> GetAllAsync();
    Task<Doctor?> GetByIdAsync(int id);
    Task<Doctor> CreateAsync(Doctor doctor);
    Task<Doctor?> UpdateAsync(int id, Doctor doctor);
    Task<bool> DeleteAsync(int id);
}

