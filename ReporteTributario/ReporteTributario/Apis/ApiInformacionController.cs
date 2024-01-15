using Microsoft.AspNetCore.Mvc;
using ReporteTributario.Models.ViewModels;
using ReporteTributario.Servicios.Contrato;
using ReporteTributario.Servicios.Implementacion;

namespace ReporteTributario.Apis
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiInformacionController : Controller, IAdminInformacionService
    {        
        private readonly IAdminInformacionService _Service;

        public ApiInformacionController(IAdminInformacionService Service)
        {
            _Service = Service;
        }
        
        [HttpGet]
        public async Task<List<InformacionBaseVM>> ObtenerRegistrosAsync()
        {
            var Lstdatos = await _Service.ObtenerRegistrosAsync();

            return Lstdatos;
        }
    }
}
