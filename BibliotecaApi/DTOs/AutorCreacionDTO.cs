using BibliotecaApi.Validaciones;
using System.ComponentModel.DataAnnotations;

namespace BibliotecaApi.DTOs
{
    public class AutorCreacionDTO
    {
        [Required(ErrorMessage = "Por favor, escriba el '{0}' del autor.")]
        [StringLength(50, ErrorMessage = "El '{0}' debe tener {1} caracteres o menos.")]
        [PrimeraLetraMayuscula]
        //[Column("Nombre")] // Para cambiar el nombre de la columna en la base de datos se debe
        // usar el atributo Column para mapear la propiedad Nombres a la columna Nombres en la base de datos.
        public required string Nombres { get; set; }

        [Required(ErrorMessage = "Por favor, escriba el '{0}' del autor.")]
        [StringLength(50, ErrorMessage = "El '{0}' debe tener {1} caracteres o menos.")]
        [PrimeraLetraMayuscula]
        public required string Apellidos { get; set; }

        [StringLength(20, ErrorMessage = "El '{0}' debe tener {1} caracteres o menos.")]
        public string? Identificacion { get; set; }
    }
}
