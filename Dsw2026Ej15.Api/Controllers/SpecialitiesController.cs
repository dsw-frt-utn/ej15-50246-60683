using Dsw2026Ej15.Domain.Interfaces;
using Dsw2026Ej15.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Dsw2026Ej15.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SpecialitiesController : ControllerBase
{
    private readonly IPersistence _persistence;
    public SpecialitiesController(IPersistence persistence)
    {
        _persistence = persistence;
    }

    [HttpGet]
    public async Task<ActionResult<List<Speciality>>> GetAll()
    {
        var specialities = await _persistence.GetSpecialitiesAsync();
        return Ok(specialities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Speciality>> GetById(Guid Id)
    {
        var speciality = await _persistence.GetSpecialityByIdAsync(Id);
        if (speciality == null)
        {
            return NotFound();
        }
        return Ok(speciality);
    }
}
