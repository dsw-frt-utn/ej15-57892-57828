using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Dsw2026Ej15.Data
{
    public class PersistenceInMemory : IPersistence
    {
        private List<Speciality> _specialities = [];
        private List<Doctor> _doctors = [];

        public PersistenceInMemory()
        {
            LoadSpecialities();
        }

        private void LoadSpecialities()
        {
            try
            {
                string jsonPath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Sources",
                "specialities.json"
            );
                var json = File.ReadAllText(jsonPath);
                var specialities = JsonSerializer.Deserialize<List<SpecialityDto>>(json,
                    new JsonSerializerOptions()
                    {
                        PropertyNameCaseInsensitive = true
                    }) ?? [];
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading specialities: {ex.Message}");
            }

        }
        public void AddDoctor(Doctor doctor)
        {
            _doctors.Add(doctor);
        }

        public Doctor? GetDoctorById(Guid id)
        {
            return _doctors.FirstOrDefault(d => d.Id == id);
        }

        public IEnumerable<Doctor> GetDoctors()
        {
            return _doctors;
        }

        public Speciality? GetSpecialityById(Guid id)
        {
            return _specialities.FirstOrDefault(s => s.Id == id);
        }
    }
}
