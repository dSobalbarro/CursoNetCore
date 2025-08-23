using Microsoft.EntityFrameworkCore;
using BibliotecaApi.Entidades;//Para no tener que poner el namespace completo(Entidades.Autor)
namespace BibliotecaApi.Datos
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions options) : base(options)
        {


        }

        public DbSet<Autor> Autores { get; set; }// Representa la tabla de Autores en la base de datos
        public DbSet<Libro> Libros { get; set; } // Representa la tabla de Libros en la base de datos
    }
}
