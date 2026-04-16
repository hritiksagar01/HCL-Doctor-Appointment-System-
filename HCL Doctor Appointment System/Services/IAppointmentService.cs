using Doctor_Appointment_System.Models;

namespace Doctor_Appointment_System.Services;

public interface IAppointmentService
{
    Task<List<Appointment>> GetAllAsync();
    Task<Appointment?> GetByIdAsync(int id);
    Task<List<Appointment>> GetByUserIdAsync(int userId);
    Task<Appointment> BookAsync(Appointment appointment);
    Task<Appointment?> UpdateStatusAsync(int id, string status);
    Task<bool> CancelAsync(int id);
}

