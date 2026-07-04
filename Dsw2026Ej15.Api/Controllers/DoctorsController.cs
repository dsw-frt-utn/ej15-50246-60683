using Dsw2026Ej15.Api.Models;
using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Exceptions;
using Dsw2026Ej15.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Dsw2026Ej15.Api.Controllers;

[ApiController]
[Route("api/doctors")] 
public class DoctorsController : ControllerBase
{
    private readonly IPersistence _persistence;
    public DoctorsController(IPersistence persistence)
    {
        _persistence = persistence;
    }

    [HttpPost]
    public async Task<IActionResult> CreateDoctor([FromBody] DoctorModel.Request request)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            throw new ValidationException("Name es requerido");
        }

        if (string.IsNullOrWhiteSpace(request.LicenseNumber))
        {
            throw new ValidationException("LicenseNumber es requerido");
        }

        var speciality = await _persistence.GetSpecialityByIdAsync(request.SpecialityId);
        if (speciality == null)
        {
            throw new ValidationException("La especialidad no existe");
        }

        var doctor = new Doctor(request.Name, request.LicenseNumber, speciality);
        await _persistence.AddDoctorAsync(doctor);

        return CreatedAtAction(
            nameof(GetById),
            new { id = doctor.Id },
            new
            {
                doctor.Id,
                doctor.Name,
                doctor.LicenseNumber,
                SpecialityName = speciality.Name
            });
    }

    [HttpGet]
    public async Task<ActionResult<List<Doctor>>> GetAll()
    {
        var doctors = await _persistence.GetDoctorsAsync();

        return Ok(doctors.Select(d => new
        {
            d.Id,
            d.Name,
            d.LicenseNumber,
            SpecialityName = d.Speciality?.Name
        }));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Doctor>> GetById(Guid Id)
    {
        var doctor = await _persistence.GetDoctorByIdAsync(Id);

        if (doctor == null)
        {
            return NotFound();
        }
        return Ok(new
        {
            doctor.Name,
            doctor.LicenseNumber,
            SpecialityName = doctor.Speciality?.Name ?? string.Empty
        });
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteDoctor(Guid Id)
    {
        var doctor = await _persistence.GetDoctorByIdAsync(Id);
        if (doctor == null)
        {
            return NotFound();
        }
        doctor.Deactivate();
        await _persistence.UpdateDoctorAsync(doctor);
        return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateDoctor(Guid id,
    [FromBody] DoctorModel.Request request)
    {
        var doctor = await _persistence.GetDoctorByIdAsync(id);

        if (doctor == null)
            return NotFound();

        var speciality =
            await _persistence.GetSpecialityByIdAsync(request.SpecialityId);

        if (speciality == null)
            throw new ValidationException("La especialidad no existe");

        doctor.Update(
            request.Name,
            request.LicenseNumber,
            speciality);

        await _persistence.UpdateDoctorAsync(doctor);

        return NoContent();
    }
}
