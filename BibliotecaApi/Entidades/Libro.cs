using System.ComponentModel.DataAnnotations;

namespace BibliotecaApi.Entidades
{
    public class Libro
    {

        public int Id { get; set; }
        [Required]
        public required string Titulo { get; set; }
        public int AutorId { get; set; } // Relación con el autor - llave foránea
        public Autor? Autor { get; set; } // Relación con el autor - navegación opcional

    }
}
