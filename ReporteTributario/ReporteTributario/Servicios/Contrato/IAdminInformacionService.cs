using ReporteTributario.Models.ViewModels;

namespace ReporteTributario.Servicios.Contrato
{
    public interface IAdminInformacionService
    {
        Task<List<InformacionBaseVM>> ObtenerRegistrosAsync();
    }
}
