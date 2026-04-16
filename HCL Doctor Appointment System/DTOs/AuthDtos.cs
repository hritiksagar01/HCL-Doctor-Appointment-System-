﻿using System.ComponentModel.DataAnnotations;

namespace Doctor_Appointment_System.DTOs;

public class RegisterDto
{
    [Required]
    public string FullName { get; set; } = string.Empty;
    
    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;
    
    [Required, MinLength(6)]
    public string Password { get; set; } = string.Empty;
    
    public string? Phone { get; set; }
}

public class LoginDto
{
    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;
    
    [Required]
    public string Password { get; set; } = string.Empty;
}

public class AuthResponseDto
{
    public string Token { get; set; } = string.Empty;
    public AuthUserDto User { get; set; } = new();
}

public class AuthUserDto
{
    public int UserId { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}

public class AppointmentResponseDto
{
    public int AppointmentId { get; set; }
    public int UserId { get; set; }
    public int DoctorId { get; set; }
    public int SlotId { get; set; }
    public string DoctorName { get; set; } = string.Empty;
    public string SpecialtyName { get; set; } = string.Empty;
    public string Mode { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public DateTime BookingDate { get; set; }
    public string SlotDate { get; set; } = string.Empty;
    public string StartTime { get; set; } = string.Empty;
    public string EndTime { get; set; } = string.Empty;
    public string? MeetingLink { get; set; }
    public string? ClinicAddress { get; set; }
}

public class CreateAppointmentDto
{
    public int DoctorId { get; set; }
    public int SlotId { get; set; }
    public string Mode { get; set; } = string.Empty;
}

public class DoctorResponseDto
{
    public int DoctorId { get; set; }
    public string DoctorName { get; set; } = string.Empty;
    public int SpecialtyId { get; set; }
    public string SpecialtyName { get; set; } = string.Empty;
    public int Experience { get; set; }
    public decimal Fees { get; set; }
    public string Mode { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? ClinicAddress { get; set; }
}

