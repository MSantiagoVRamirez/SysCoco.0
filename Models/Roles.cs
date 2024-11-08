using System.ComponentModel.DataAnnotations;

namespace SysCoco._0.Models
{
    public class Roles
    {
        [Key]
        public int id { get; set; }

        [Required(ErrorMessage = "El campo 'nombres' es obligatorio.")]
        [StringLength(100, ErrorMessage = "El campo 'nombres' no puede exceder los 100 caracteres.")]
        public string Nombre { get; set; }

        public bool Estado { get; set; }
    }
}