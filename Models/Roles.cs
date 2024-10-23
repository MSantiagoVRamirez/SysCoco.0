using System.ComponentModel.DataAnnotations;

namespace SysCoco._0.Models
{
    public class Roles
    {
        [Key]
        public int id { get; set; }

        [Required(ErrorMessage = "El campo 'Nombre' es obligatorio.")]
        public nombre Nombre { get; set; }

        public bool Estado { get; set; }

        public enum nombre
        {
            institucion,
            moderador,
            estudiante,
            acudiente
        }
    }
}
