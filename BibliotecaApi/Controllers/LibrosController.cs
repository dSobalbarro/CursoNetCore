using BibliotecaApi.Datos;
using Microsoft.AspNetCore.Mvc;
using BibliotecaApi.Entidades;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaApi.Controllers
{

    [ApiController]
    [Route("api/libros")]
    public class LibrosController: ControllerBase
    {
        private readonly ApplicationDBContext context;

        public LibrosController(ApplicationDBContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task< IEnumerable<Libro> > Get()
        {
             return await context.Libros.ToListAsync(); // Obtiene todos los libros de la base de datos de manera asíncrona

        }

        [HttpGet("{id:int}")] //api/libros/1 -> obtiene un libro por su ID
        public async Task<ActionResult<Libro>> Get(int id)
        {
            var libro = await context.Libros
                .Include(x => x.Autor) // Incluye la información del autor al buscar el libro
                .FirstOrDefaultAsync(x => x.Id == id); // Busca un libro por su ID de manera asíncrona
            if (libro == null)
            {
                return NotFound(); // Devuelve un error 404 si no se encuentra el libro(si es nulo)
            }
            return libro; // Devuelve el libro encontrado
        }

        [HttpPost]
        public async Task<ActionResult<Libro>> Post(Libro libro)
        {
            var existeAutor = await context.Autores.AnyAsync(x => x.Id == libro.AutorId);
            if (!existeAutor)
            {
                return BadRequest($"No existe el autor con Id {libro.AutorId}"); // Devuelve un error 400 si no existe el autor
            }

            // Aquí podrías agregar lógica para guardar el libro en la base de datos
            context.Add(libro);// marca el objeto ára ser guardado en el futuro
            await context.SaveChangesAsync(); // Guarda los cambios en la base de datos de manera asíncrona
            return Ok(); // Devuelve un resultado exitoso
        }

        [HttpPut("{id:int}")] // api/libros/1
        public async Task<ActionResult> Put(int id, Libro libro)
        {
            if (id != libro.Id) // Verifica si el ID del libro coincide con el ID en la URL
            {
                return BadRequest("El ID del libro no coincide con el ID proporcionado en la URL.");
            }

            var existeAutor = await context.Autores.AnyAsync(x => x.Id == libro.AutorId);

            if (!existeAutor)
            {
                return BadRequest($"No existe el autor con Id {libro.AutorId}"); // Devuelve un error 400 si no existe el autor
            }

            context.Update(libro); // Marca el objeto para ser actualizado
            await context.SaveChangesAsync(); // Guarda los cambios en la base de datos de manera asíncrona
            return NoContent(); // Devuelve un resultado sin contenido (204 No Content)
        }

        [HttpDelete("{id:int}")] // api/libros/1
        public async Task<ActionResult> Delete(int id)
        {
            var libro = await context.Libros.FirstOrDefaultAsync(x => x.Id == id); // Busca un libro por su ID de manera asíncrona
            if (libro == null)
            {
                return NotFound(); // Devuelve un error 404 si no se encuentra el libro(si es nulo)
            }
            context.Remove(libro); // Marca el objeto para ser eliminado
            await context.SaveChangesAsync(); // Guarda los cambios en la base de datos de manera asíncrona
            return NoContent(); // Devuelve un resultado sin contenido (204 No Content)

        }




    }
}
