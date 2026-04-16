using Doctor_Appointment_System.DTOs;

namespace Doctor_Appointment_System.Services;

public interface IAuthService
{
    Task<AuthResponseDto> RegisterAsync(RegisterDto dto);
    Task<AuthResponseDto> LoginAsync(LoginDto dto);
}

