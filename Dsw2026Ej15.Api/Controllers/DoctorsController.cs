using Dsw2026Ej15.Api.Models;
using Dsw2026Ej15.Data.Dto;
using Dsw2026Ej15.Data.Implementations;
using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Exceptions;
using Dsw2026Ej15.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace Dsw2026Ej15.Api.Controllers
{
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
            var activeDoctors = doctors.Where(d => d.IsActive).Select(d => new
            {
                d.Id,
                d.Name,
                d.LicenseNumber,
                SpecialityName = d.Speciality?.Name ?? string.Empty
            }).ToList();

            return Ok(activeDoctors);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Doctor>> GetById(Guid Id)
        {
            var doctor = await _persistence.GetDoctorByIdAsync(Id);

            if (doctor == null || !doctor.IsActive)

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
            if (doctor == null || !doctor.IsActive)
            {
                return NotFound();
            }
            await _persistence.DeleteDoctorAsync(Id);
            return NoContent();
        }
    }
}
