using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using SysCoco._0.Models;

namespace SysCoco._0.Models
{
    public class Actividad
    {
        [Key]
        public int id { get; set; }

        [ForeignKey("usuarios")]
        [Required(ErrorMessage = "El campo 'creador' es obligatorio.")]
        public int creador { get; set; }

        public Usuarios? ActividadUsuarios { get; set; }

        [ForeignKey("cursos")]
        [Required(ErrorMessage = "El campo 'cursoid' es obligatorio.")]
        public int cursoid { get; set; }

        public Cursos? cursos { get; set; }

        [Required(ErrorMessage = "El campo 'asunto' es obligatorio.")]
        [StringLength(100, ErrorMessage = "El 'asunto' no puede exceder los 100 caracteres.")]
        public string asunto { get; set; }

        [Required(ErrorMessage = "El campo 'contenido' es obligatorio.")]
        [StringLength(1000, ErrorMessage = "El 'contenido' no puede exceder los 1000 caracteres.")]
        public string contenido { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "La 'fecha de creación' es obligatoria.")]
        public DateTime fechaCreacion { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "La 'fecha de envío' es obligatoria.")]
        public DateTime fechaenvio { get; set; }

        [DataType(DataType.Url)]
        [StringLength(200, ErrorMessage = "La URL del 'documento adjunto' no puede exceder los 200 caracteres.")]
        public string documentoAdjunto { get; set; }
    }
}