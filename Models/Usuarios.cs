using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SysCoco._0.Models
{
    public class Usuarios
    {
        [Key]
        public int id { get; set; }

        [Required(ErrorMessage = "El campo 'nombres' es obligatorio.")]
        [StringLength(100, ErrorMessage = "El campo 'nombres' no puede exceder los 100 caracteres.")]
        public string nombres { get; set; }

        [Required(ErrorMessage = "El campo 'apellidos' es obligatorio.")]
        [StringLength(100, ErrorMessage = "El campo 'apellidos' no puede exceder los 100 caracteres.")]
        public string apellidos { get; set; }

        [Required(ErrorMessage = "El campo 'tipo de documento' es obligatorio.")]
        [StringLength(50, ErrorMessage = "El campo 'tipo de documento' no puede exceder los 50 caracteres.")]
        public string tipoDocumento { get; set; }

        [Required(ErrorMessage = "El campo 'número de documento' es obligatorio.")]
        [StringLength(20, ErrorMessage = "El campo 'número de documento' no puede exceder los 20 caracteres.")]
        public string numeroDocumento { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "El campo 'contraseña' es obligatorio.")]
        [StringLength(100, ErrorMessage = "El campo 'contraseña' no puede exceder los 100 caracteres.")]
        public string contraseña { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "El campo 'fecha de nacimiento' es obligatorio.")]
        public DateTime fechaNacimiento { get; set; }

        [MaxLength(10, ErrorMessage = "El campo 'número de teléfono' no puede exceder los 10 caracteres.")]
        public string numeroTelefono { get; set; }

        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "El campo 'correo' no tiene un formato válido.")]
        [Required(ErrorMessage = "El campo 'correo' es obligatorio.")]
        public string correo { get; set; }

        [ForeignKey("roles")]
        [Required(ErrorMessage = "El campo 'rolesid' es obligatorio.")]
        public int rolesid { get; set; }

        public Roles? roles { get; set; }
    }
}
