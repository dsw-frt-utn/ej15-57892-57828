using System.ComponentModel.DataAnnotations;

namespace Dsw2026Ej15.Api.DTOs
{
    public class DoctorInsertDto
    {
        
        
            [Required(ErrorMessage = "El nombre es obligatorio.")]
            public string Name { get; set; } = string.Empty;

            [Required(ErrorMessage = "La matrícula es obligatoria.")]
            public string LicenseNumber { get; set; } = string.Empty;

            [Required(ErrorMessage = "El Id de la especialidad es obligatorio.")]
            public Guid SpecialityId { get; set; }
        

    }
}
