namespace Dsw2026Ej15.Api.Models
{
    public record DoctorModel
    {
        //representa la estructura de datos que se espera recibir en la solicitud HTTP
        public record Request(string Name, string LicenseNumber, Guid SpecialityId);
        public record Response(string Name, string LicenseNumber, string? SpecialityName);

    }
}
