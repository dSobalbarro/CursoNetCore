using BibliotecaApi.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaApi.Controllers
{
    [ApiController]
    [Route("api/valores")]
    public class ValoresController : ControllerBase
    {
        //Principio de inversión de dependencias para depender de la interface
        private readonly IRepositorioValores repositorioValores;
        private readonly ServicioTransient transient1;
        private readonly ServicioTransient transient2;
        private readonly ServicioScoped scoped1;
        private readonly ServicioScoped scoped2;
        private readonly ServicioSingleton singleton;

        public ValoresController(IRepositorioValores repositorioValores,
            ServicioTransient transient1,
            ServicioTransient transient2,
            ServicioScoped scoped1,
            ServicioScoped scoped2,
            ServicioSingleton singleton
            )
        {
            this.repositorioValores = repositorioValores;
            this.transient1 = transient1;
            this.transient2 = transient2;
            this.scoped1 = scoped1;
            this.scoped2 = scoped2;
            this.singleton = singleton;
        }
        [HttpGet]
        [HttpGet("servicios-tiempo-de-vida")]
        public IActionResult GetServiciostiempoDeVida()
        {
            return Ok(new
            {
                Transients = new
                {
                    transient1 = transient1.ObtenerGuid,
                    transiend2 = transient2.ObtenerGuid,

                },
                Scopeds = new
                {
                    scoped1 = scoped1.ObtenerGuid,
                    scoped2 = scoped2.ObtenerGuid
                },
                Singlentons = new
                {
                    singleton = singleton.ObtenerGuid
                }
            });
        }

        public IEnumerable<Valor> Get()
        {
            //Acoplamiento fuerte y su desventajas
            //var repositorioValores = new RepositorioValores();
            return repositorioValores.ObtenerValores();
        }

        [HttpPost]
        public IActionResult Post(Valor valor)
        {
            repositorioValores.InsertarValor(valor);

            return Ok();
        }

    }
}