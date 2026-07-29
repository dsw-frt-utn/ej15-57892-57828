using Dsw2026Ej15.Api.Models;
using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Dsw2026Ej15.Api.Controllers;

public class DoctorsController : AppController
{
    private readonly IPersistence _persistence;

    public DoctorsController(IPersistence persistence)
    {
        _persistence = persistence;
    }

    [HttpPost("doctors")]
    public async Task<IActionResult> CreateDoctor(DoctorModel.Request request)
    {
        if (string.IsNullOrWhiteSpace(request.Name) || string.IsNullOrWhiteSpace(request.LicenseNumber))
        {
            return BadRequest("Nombre y Matrícula son requeridos.");
        }

        var speciality = _persistence.GetSpecialityById(request.SpecialityId);
        if (speciality is null)
        {
            return BadRequest("La especialidad no existe.");
        }

        var doctor = new Doctor(request.Name, request.LicenseNumber, speciality);
        _persistence.AddDoctor(doctor);

        return Created();
    }

    [HttpGet("doctors")]
    public async Task<IActionResult> GetDoctors()
    {
        return Ok(_persistence.GetDoctors());
    }

    [HttpGet("doctors/{id:guid}")]
    public async Task<IActionResult> GetDoctorById(Guid id)
    {
        var doctor = _persistence.GetDoctorById(id);
        if (doctor is null || !doctor.IsActive)
        {
            return NotFound("No se encuentra el doctor.");
        }

        var response = new DoctorModel.Response(doctor.Name, doctor.LicenseNumber, doctor.Speciality?.Name);

        return Ok(response);
    }

    [HttpDelete("doctors/{id:guid}")]
    public async Task<IActionResult> DeleteDoctor(Guid id)
    {
        var doctor = _persistence.GetDoctorById(id);
        if (doctor is null || !doctor.IsActive)
        {
            return NotFound("No se encuentra el doctor.");
        }

        _persistence.DeleteDoctor(id);

        return NoContent();
    }
}

