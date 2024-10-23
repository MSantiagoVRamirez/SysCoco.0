using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SysCoco._0.Models
{
        public class Cursos
        {
            [Key]
            public int id { get; set; }

            [ForeignKey("materia")]
            [Required(ErrorMessage = "El campo 'materiaid' es obligatorio.")]
            public int materiaid { get; set; }

            public Materia? materia { get; set; }
        }
    }
