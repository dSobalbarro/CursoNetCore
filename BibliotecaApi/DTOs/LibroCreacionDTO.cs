using BibliotecaApi.Entidades;
using System.ComponentModel.DataAnnotations;

namespace BibliotecaApi.DTOs
{
    public class LibroCreacionDTO
    {
        [Required]
        [StringLength(50, ErrorMessage = "El campo {0} debe tener {1} caracteres o menos.")]
        public required string Titulo { get; set; }
        public int AutorId { get; set; } // Relación con el autor - llave foránea
        public Autor? Autor { get; set; } // Relación con el autor - navegación opcional

    }
}
