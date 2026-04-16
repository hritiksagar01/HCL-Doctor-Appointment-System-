using Doctor_Appointment_System.Models;
using Doctor_Appointment_System.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Doctor_Appointment_System.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SlotsController : ControllerBase
{
    private readonly ISlotService _service;
    public SlotsController(ISlotService service) => _service = service;

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> Get(int id)
    {
        var item = await _service.GetByIdAsync(id);
        return item == null ? NotFound() : Ok(item);
    }

    [HttpGet("doctor/{doctorId}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetByDoctor(int doctorId) =>
        Ok(await _service.GetByDoctorIdAsync(doctorId));

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create(Slot slot)
    {
        var created = await _service.CreateAsync(slot);
        return CreatedAtAction(nameof(Get), new { id = created.SlotId }, created);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(int id, Slot slot)
    {
        var updated = await _service.UpdateAsync(id, slot);
        return updated == null ? NotFound() : Ok(updated);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id) =>
        await _service.DeleteAsync(id) ? NoContent() : NotFound();
}
