using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using SysCoco._0.Models;

namespace SysCoco._0.Models
{
    public class Materia
    {
        [Key]
        public int id { get; set; }

        [Required(ErrorMessage = "El campo 'nombre' es obligatorio.")]
        [StringLength(100, ErrorMessage = "El 'nombre' no puede exceder los 100 caracteres.")]
        public string nombre { get; set; }

        [StringLength(500, ErrorMessage = "La 'descripción' no puede exceder los 500 caracteres.")]
        public string descripcion { get; set; }

        [ForeignKey("usuarios")]
        [Required(ErrorMessage = "El campo 'usuariosid' es obligatorio.")]
        public int usuariosid { get; set; }

        public Usuarios? materiaUsuarios { get; set; }
    }
}