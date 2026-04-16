using Doctor_Appointment_System.DTOs;
using Doctor_Appointment_System.Models;
using Doctor_Appointment_System.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Doctor_Appointment_System.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DoctorsController : ControllerBase
{
    private readonly IDoctorService _service;
    public DoctorsController(IDoctorService service) => _service = service;

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAll([FromQuery] string? mode, [FromQuery] int? specialtyId)
    {
        var doctors = await _service.GetAllAsync();

        if (!string.IsNullOrEmpty(mode))
            doctors = doctors.Where(d => d.Mode.Equals(mode, StringComparison.OrdinalIgnoreCase)).ToList();

        if (specialtyId.HasValue)
            doctors = doctors.Where(d => d.SpecialtyId == specialtyId.Value).ToList();

        var result = doctors.Select(d => new DoctorResponseDto
        {
            DoctorId = d.DoctorId,
            DoctorName = d.DoctorName,
            SpecialtyId = d.SpecialtyId,
            SpecialtyName = d.Specialty?.SpecialtyName ?? "",
            Experience = d.Experience,
            Fees = d.Fees,
            Mode = d.Mode,
            Email = d.Email,
            ClinicAddress = d.ClinicAddress
        }).ToList();

        return Ok(result);
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> Get(int id)
    {
        var d = await _service.GetByIdAsync(id);
        if (d == null) return NotFound();
        return Ok(new DoctorResponseDto
        {
            DoctorId = d.DoctorId,
            DoctorName = d.DoctorName,
            SpecialtyId = d.SpecialtyId,
            SpecialtyName = d.Specialty?.SpecialtyName ?? "",
            Experience = d.Experience,
            Fees = d.Fees,
            Mode = d.Mode,
            Email = d.Email,
            ClinicAddress = d.ClinicAddress
        });
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create(Doctor doctor)
    {
        var created = await _service.CreateAsync(doctor);
        return CreatedAtAction(nameof(Get), new { id = created.DoctorId }, created);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(int id, Doctor doctor)
    {
        var updated = await _service.UpdateAsync(id, doctor);
        return updated == null ? NotFound() : Ok(updated);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id) =>
        await _service.DeleteAsync(id) ? NoContent() : NotFound();
}
