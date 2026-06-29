using Dsw2026Ej15.Api.DTOs;
using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Dsw2026Ej15.Api.Controllers
{
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
        public IActionResult CreateDoctor([FromBody] DoctorInsertDto dto)
        {
            var speciality = _persistence.GetSpecialityById(dto.SpecialityId);

            if (speciality == null)
            {
                throw new ValidationException("La especialidad especificada no existe.");
            }

            var newDoctor = new Doctor(dto.Name, dto.LicenseNumber, speciality);
            _persistence.AddDoctor(newDoctor);

            var response = new DoctorResponseDto
            {
                Id = newDoctor.Id,
                Name = newDoctor.Name,
                LicenseNumber = newDoctor.LicenseNumber,
                SpecialityName = speciality.Name
            };

            return CreatedAtAction(nameof(GetDoctorById), new { id = response.Id }, response);
        }

        [HttpGet]
        public IActionResult GetDoctors()
        {
            var doctors = _persistence.GetDoctors()
                .Where(d => d.IsActive) // Solo activos
                .Select(d => new DoctorResponseDto
                {
                    Id = d.Id,
                    Name = d.Name,
                    LicenseNumber = d.LicenseNumber,
                    SpecialityName = d.Speciality?.Name ?? "Sin Especialidad"
                });

            return Ok(doctors);
        }

        [HttpGet("{id:guid}")]
        public IActionResult GetDoctorById(Guid id)
        {
            var doctor = _persistence.GetDoctorById(id);

           
            if (doctor == null || !doctor.IsActive)
            {
                return NotFound($"No se encuentra el médico o no está activo.");
            }

            var response = new DoctorResponseDto
            {
                Id = doctor.Id,
                Name = doctor.Name,
                LicenseNumber = doctor.LicenseNumber,
                SpecialityName = doctor.Speciality?.Name ?? "Sin Especialidad"
            };

            return Ok(response);
        }

        [HttpDelete("{id:guid}")]
        public IActionResult DeleteDoctor(Guid id)
        {
            var doctor = _persistence.GetDoctorById(id);

            if (doctor == null || !doctor.IsActive)
            {
                return NotFound($"No se encuentra el médico o ya está de baja.");
            }

            
            doctor.Deactivate();

            
            return NoContent();
        }

    }
}
