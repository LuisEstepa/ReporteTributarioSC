using ReporteTributario.Models.Entities;

namespace ReporteTributario.Servicios.Contrato
{
    public interface IEventService
    {
        Task<List<InformacionBase>> GetInformacionListAsync();
    }
}
