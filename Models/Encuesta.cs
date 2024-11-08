using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using SysCoco._0.Models;

namespace SysCoco._0.Models
{
    public class Encuesta
    {
        [Key]
        public int id { get; set; }

        [Required(ErrorMessage = "El campo 'título' es obligatorio.")]
        [StringLength(200, ErrorMessage = "El 'título' no puede exceder los 200 caracteres.")]
        public string titulo { get; set; }

        [Required(ErrorMessage = "El campo 'tipo de pregunta' es obligatorio.")]
        public tipoP tipoPregunta { get; set; }

        [Required(ErrorMessage = "El campo 'pregunta' es obligatorio.")]
        [StringLength(500, ErrorMessage = "La 'pregunta' no puede exceder los 500 caracteres.")]
        public string pregunta { get; set; }

        [Required(ErrorMessage = "El campo 'pregunta' es obligatorio.")]
        [StringLength(2000, ErrorMessage = "El 'texto' no puede exceder los 2000 caracteres.")]
        public string Texto { get; set; }

        [Required(ErrorMessage = "El campo 'pregunta' es obligatorio.")]
        [StringLength(1000, ErrorMessage = "La 'respuesta' no puede exceder los 1000 caracteres.")]
        public string Respuesta { get; set; }

        [ForeignKey("usuarios")]
        [Required(ErrorMessage = "El campo 'usuario' es obligatorio.")]
        public int usuarioId { get; set; }

        public Usuarios? EncuestaUsuario { get; set; }
    }

    public enum tipoP
    {
        Cerrada,
        Abierta
    }
}