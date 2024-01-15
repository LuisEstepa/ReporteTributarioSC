using Microsoft.AspNetCore.Mvc;
using ReporteTributario.Models.Entities;
using ReporteTributario.Models.ViewModels;
using ReporteTributario.Recursos;
using ReporteTributario.Servicios.Contrato;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ReporteTributario.Apis
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiEventosController : ControllerBase
    {
        IEventService _eventService;
        public ApiEventosController(IEventService eventService)
        {
            _eventService = eventService;
        }

        // GET: api/<ApiEventosController>
        [HttpGet]
        //[Route("Get/open")]
        public async Task<List<VMEventos>> GetAllEventos()
        {
            List<VMEventos> eventos = new List<VMEventos>();
            var LstEven = await _eventService.GetInformacionListAsync();
            foreach (var item in LstEven)
            {
                VMEventos vMEventos = new VMEventos();
                vMEventos.id = item.IdImpuesto;
                vMEventos.title = item.Impuesto;
                vMEventos.start = item.FechaLimite.ToString("yyyy-MM-dd HH:mm:ss");
                vMEventos.end = item.FechaLimite.ToString("yyyy-MM-dd HH:mm:ss");
                eventos.Add(vMEventos);
            }
            return eventos;
        }
        //[HttpGet]
        //[Route("Get/open")]
        //public async Task<Usuario> GetUsuario1(string correo, string clave)
        //{
        //    Usuario usuario_encontrado = await _usuarioServicio.GetUsuario(correo, Utilidades.EncriptarClave(clave));
        //    return usuario_encontrado;
        //}
        //// GET api/<ApiEventosController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<ApiEventosController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<ApiEventosController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<ApiEventosController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
