using BibliotecaApi.Validaciones;
using System.ComponentModel.DataAnnotations;

namespace BibliotecaApi.Entidades
{
    public class Autor:IValidatableObject
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Por favor, escriba el '{0}' del autor.")]
        [StringLength(50, ErrorMessage ="El '{0}' debe tener {1} caracteres o menos.")]
        //[PrimeraLetraMayuscula]
        public required string Nombre { get; set; }
        public List<Libro> Libros { get; set; } = new List<Libro>();// Relación con los libros - uno a muchos

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!string.IsNullOrEmpty(Nombre))
            {
                var primeraletra = Nombre[0].ToString();

                if(primeraletra != primeraletra.ToUpper())
                {
                    yield return new ValidationResult("La prmera letra debe ser mayúscula(por modelo).",
                    new string[] {nameof(Nombre)}); //NameOf me permite actulizar el campo nombre en casacada.
                }
            }
        }

        //[Range(18,120, ErrorMessage ="Por favor, escriba una {0} entre 18 y 120 años.")]
        //public int Edad { get; set; }
        //[CreditCard(ErrorMessage ="Por favor, escriba un formato de número de tarjeta.")]//No valida que sea valida, sino solo el fromato
        //public string? TarjetaDeCredito { get; set; }
        //[Url(ErrorMessage = "Por favor, escriba un formato URL.")]//No valuida que exista el sitio, solo el formato
        //public string? URL { get; set; }

    }
}
 