using System.Security.Claims;
using Doctor_Appointment_System.DTOs;
using Doctor_Appointment_System.Models;
using Doctor_Appointment_System.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Doctor_Appointment_System.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AppointmentsController : ControllerBase
{
    private readonly IAppointmentService _service;
    public AppointmentsController(IAppointmentService service) => _service = service;

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAll()
    {
        var appointments = await _service.GetAllAsync();
        return Ok(appointments.Select(MapToDto));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var item = await _service.GetByIdAsync(id);
        return item == null ? NotFound() : Ok(MapToDto(item));
    }

    [HttpGet("my")]
    public async Task<IActionResult> GetMyAppointments()
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var appointments = await _service.GetByUserIdAsync(userId);
        return Ok(appointments.Select(MapToDto));
    }

    [HttpPost]
    public async Task<IActionResult> Book(CreateAppointmentDto dto)
    {
        try
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var appointment = new Appointment
            {
                UserId = userId,
                DoctorId = dto.DoctorId,
                SlotId = dto.SlotId,
                Mode = dto.Mode
            };
            var created = await _service.BookAsync(appointment);
            // Reload with includes
            var full = await _service.GetByIdAsync(created.AppointmentId);
            return CreatedAtAction(nameof(Get), new { id = created.AppointmentId }, MapToDto(full!));
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPatch("{id}/status")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateStatus(int id, [FromBody] string status)
    {
        var updated = await _service.UpdateStatusAsync(id, status);
        return updated == null ? NotFound() : Ok(updated);
    }

    [HttpPatch("{id}/cancel")]
    public async Task<IActionResult> Cancel(int id) =>
        await _service.CancelAsync(id) ? Ok(new { message = "Cancelled" }) : NotFound();

    private static AppointmentResponseDto MapToDto(Appointment a) => new()
    {
        AppointmentId = a.AppointmentId,
        UserId = a.UserId,
        DoctorId = a.DoctorId,
        SlotId = a.SlotId,
        DoctorName = a.Doctor?.DoctorName ?? "",
        SpecialtyName = a.Doctor?.Specialty?.SpecialtyName ?? "",
        Mode = a.Mode,
        Status = a.Status,
        BookingDate = a.BookingDate,
        SlotDate = a.Slot?.SlotDate.ToString("yyyy-MM-dd") ?? "",
        StartTime = a.Slot?.StartTime.ToString("HH:mm:ss") ?? "",
        EndTime = a.Slot?.EndTime.ToString("HH:mm:ss") ?? "",
        MeetingLink = a.MeetingLink,
        ClinicAddress = a.ClinicAddress
    };
}
