using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Dsw2026Ej15.Data;

public class PersistenceEf : IPersistence
{
    private readonly MedicalContext _context;

    public PersistenceEf(MedicalContext context)
    {
        _context = context;
    }

    public void AddDoctor(Doctor doctor)
    {
        _context.Doctors.Add(doctor);
        _context.SaveChanges();
    }

    public Doctor? GetDoctorById(Guid id)
    {
        return _context.Doctors
            .Include(d => d.Speciality)
            .FirstOrDefault(d => d.Id == id);
    }

    public IEnumerable<Doctor> GetDoctors()
    {
        return _context.Doctors
            .Include(d => d.Speciality)
            .ToList();
    }

    public Speciality? GetSpecialityById(Guid id)
    {
        return _context.Specialities.FirstOrDefault(s => s.Id == id);
    }

    public IEnumerable<Speciality> GetSpecialities()
    {
        return _context.Specialities.ToList();
    }
}

