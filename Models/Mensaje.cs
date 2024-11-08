using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using SysCoco._0.Models;

namespace SysCoco._0.Models
{
    public class Mensaje
    {
        [Key]
        public int id { get; set; }

        [ForeignKey("usuarios")]
        [Required(ErrorMessage = "El campo 'remitente' es obligatorio.")]
        public int remitente { get; set; }

        public Usuarios? mensajeRemitente { get; set; }

        [ForeignKey("usuarios")]
        [Required(ErrorMessage = "El campo 'destinatario' es obligatorio.")]
        public int destinatario { get; set; }

        public Usuarios? mensajeDestinatario { get; set; }

        [Required(ErrorMessage = "El campo 'asunto' es obligatorio.")]
        [StringLength(200, ErrorMessage = "El 'asunto' no puede exceder los 200 caracteres.")]
        public string asunto { get; set; }

        [Required(ErrorMessage = "El campo 'contenido' es obligatorio.")]
        [StringLength(2000, ErrorMessage = "El 'contenido' no puede exceder los 2000 caracteres.")]
        public string contenido { get; set; }

        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "El campo 'fecha de creación' es obligatorio.")]
        public DateTime fechaCreacion { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime fechaEnvio { get; set; }

        [DataType(DataType.Url)]
        [StringLength(500, ErrorMessage = "El 'archivo adjunto' no puede exceder los 500 caracteres.")]
        public string archivoAdjunto { get; set; }
    }
}