using Microsoft.EntityFrameworkCore;
using ReporteTributario.Models;
using ReporteTributario.Models.Entities;
using ReporteTributario.Servicios.Contrato;

namespace ReporteTributario.Servicios.Implementacion
{
    public class EventService : IEventService
    {
        private readonly DbTtributarioContext _Context;
        public EventService(DbTtributarioContext dbContext)
        {
            _Context = dbContext;
        }
        public async Task<List<InformacionBase>> GetInformacionListAsync()
        {
            List<InformacionBase> _event = await _Context.InformacionBase.Where(x => x.Vigente == true).ToListAsync();
            return _event;
        }
    }
}
