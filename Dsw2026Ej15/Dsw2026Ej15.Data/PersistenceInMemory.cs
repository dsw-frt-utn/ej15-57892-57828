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
        private readonly List<Doctor> _doctors = new();
        private readonly List<Speciality> _specialities = new();
        public PersistenceInMemory()
        {
            LoadSpecialities();
        }

        private void LoadSpecialities()
        {
            try
            {
                // Busca el archivo en la carpeta de ejecución de la API
                var filePath = Path.Combine(AppContext.BaseDirectory, "specialities.json");

                if (File.Exists(filePath))
                {
                    var json = File.ReadAllText(filePath);
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    var list = JsonSerializer.Deserialize<List<Speciality>>(json, options);

                    if (list != null)
                    {
                        _specialities.AddRange(list);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cargar el JSON: {ex.Message}");
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
