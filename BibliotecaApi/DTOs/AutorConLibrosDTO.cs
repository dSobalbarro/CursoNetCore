namespace BibliotecaApi.DTOs
{
    public class AutorConLibrosDTO: AutorDTO
    {
        public int Id { get; set; }
        public List<LibroDTO> Libros { get; set; } = [];
    }
}
