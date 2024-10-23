using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SysCoco._0.Models
{
    public class Asistencia
    {
        [Key]
        public int id { get; set; }

        [ForeignKey("Curso")]
        [Required(ErrorMessage = "El campo 'curso' es obligatorio.")]
        public int CursoId { get; set; }
        public Cursos? Curso { get; set; }

        [ForeignKey("FkUsuarios")]
        [Required(ErrorMessage = "El campo 'usuario' es obligatorio.")]
        public int usuariosId { get; set; }

        public Usuarios? FkUsuarios { get; set; }

        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "La 'fecha de asistencia' es obligatoria.")]
        public DateTime fechaAsistencia { get; set; }

        [Required(ErrorMessage = "El campo 'estado' es obligatorio.")]
        public bool estado { get; set; }
    }
}
