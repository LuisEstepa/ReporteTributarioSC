using Microsoft.EntityFrameworkCore;
using ReporteTributario.Models;
using ReporteTributario.Models.ViewModels;
using ReporteTributario.Servicios.Contrato;

namespace ReporteTributario.Servicios.Implementacion
{
    public class AdminInformacionService : IAdminInformacionService
    {
        private readonly DbTtributarioContext _context;
        public AdminInformacionService(DbTtributarioContext context)
        {
            _context = context;
        }

        public async Task<List<InformacionBaseVM>> ObtenerRegistrosAsync()
        {
            List<InformacionBaseVM> Datos = new();

            Datos = await _context.InformacionBase.Select(x => new InformacionBaseVM
            {
                IdImpuesto = x.IdImpuesto,
                Impuesto = x.Impuesto,
                Ciudad = x.Ciudad,
                Departamento = x.Departamento,
                FechaLimite = x.FechaLimite,
                Responsable = x.Responsable,
                Periodo = x.Periodo,
                Periodicidad = x.Periodicidad,
                Vigente = x.Vigente

            }).ToListAsync();

            return Datos;
        }
    }
}
