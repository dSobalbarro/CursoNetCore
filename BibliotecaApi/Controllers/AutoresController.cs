using Microsoft.AspNetCore.Mvc;
using BibliotecaApi.Entidades;
using BibliotecaApi.Controllers;
using BibliotecaApi.Datos;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaApi.Controllers
{

    [ApiController]
    [Route("api/autores")]
    public class AutoresController : ControllerBase
    {
        private readonly ApplicationDBContext context;

        public AutoresController(ApplicationDBContext context)
        {
            this.context = context;
        }


        [HttpGet]
        [HttpGet("/listado-de-autores")]
        public async Task< IEnumerable<Autor> > Get()
        {
             return await context.Autores.ToListAsync(); // Obtiene todos los autores de la base de datos de manera asíncrona
          
        }
        //aca el string no es válido/ alpha acepta solo letras en la ruta
        [HttpGet("{nombre:alpha}")]
        public async Task<IEnumerable<Autor>> Get(String nombre)
        {
            return await context.Autores.Where(x => x.Nombre.Contains(nombre)).ToListAsync();
        }


        //[HttpGet("{parametro1}/{parametro2?}")]
        //public ActionResult Get(string parametro1, string? parametro2 = "Valor por defecto") {
        //    return Ok(new {parametro1,parametro2});
        //}


        [HttpGet("primero")]// api/autores/primero -> obtiene el primer autor
        public  async Task<IEnumerable<Autor>> GetPrimerAutor()
        {
            return await context.Autores.Take(1).ToListAsync(); // Obtiene el primer autor de la base de datos de manera asíncrona
        }

        [HttpGet("{id:int}")] //api/autores/id?llave1&llave2=valor2
        public async Task<ActionResult<Autor>> Get([FromRoute]int id, bool incluirLibros)
        {
            var autor = await context.Autores
                .Include(x => x.Libros) // Incluye la información de los libros al buscar el autor
                .FirstOrDefaultAsync(x => x.Id == id); // Busca un autor por su ID de manera asíncrona
               
            if (autor == null)
            {
                return NotFound(); // Devuelve un error 404 si no se encuentra el autor(si es nulo)
            }
            return autor; // Devuelve el autor encontrado
        }

        [HttpPost]
        public async Task<ActionResult<Autor>> Post([FromBody]Autor autor)
        {
            // Aquí podrías agregar lógica para guardar el autor en la base de datos
            context.Add(autor);// marca el objeto ára ser guardado en el futuro
            await context.SaveChangesAsync(); // Guarda los cambios en la base de datos de manera asíncrona
            return Ok(); // Devuelve un resultado exitoso
        }

        [HttpPut("{id:int}")] // api/autores/1
        public async Task<ActionResult> Put(int id, Autor autor)
        {
            if (id != autor.Id) // Verifica si el ID del autor coincide con el ID en la URL
            {
                return BadRequest("El ID del autor no coincide con el ID proporcionado en la URL.");
            }
            context.Update(autor); // Marca el objeto para ser actualizado
            await context.SaveChangesAsync(); // Guarda los cambios en la base de datos de manera asíncrona
            return NoContent(); // Devuelve un resultado sin contenido (204 No Content)
        }

        [HttpDelete("{id:int}")] // api/autores/1
        public async Task<ActionResult> Delete(int id)
        {
            var autor = await context.Autores.FirstOrDefaultAsync(x => x.Id == id); // Busca el autor por su ID
            if (autor == null)
            {
                return NotFound(); // Devuelve un error 404 si no se encuentra el autor
            }
            context.Remove(autor); // Marca el objeto para ser eliminado
            await context.SaveChangesAsync(); // Guarda los cambios en la base de datos de manera asíncrona
            return NoContent(); // Devuelve un resultado sin contenido (204 No Content)
        }
    }
      
}
