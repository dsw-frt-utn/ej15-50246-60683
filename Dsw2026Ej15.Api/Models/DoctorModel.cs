using System.ComponentModel.DataAnnotations;


namespace Dsw2026Ej15.Api.Models
{
    public record DoctorModel
    {

        public record Request([Required] string Name, [Required] string LicenseNumber, Guid SpecialityId);
    }
}
