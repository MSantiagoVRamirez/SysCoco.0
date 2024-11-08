using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SysCoco._0.Models
{
    public class Votacion
    {
        [Key]
        public int id { get; set; }

        [ForeignKey("usuarios")]
        [Required(ErrorMessage = "El campo 'usuario' es obligatorio.")]
        public int usuarioId { get; set; }

        public Usuarios? votacionUsuario { get; set; }

        [ForeignKey("encuesta")]
        [Required(ErrorMessage = "El campo 'encuesta' es obligatorio.")]
        public int encuestaId { get; set; }

        public Encuesta? Encuesta { get; set; }
    }
}