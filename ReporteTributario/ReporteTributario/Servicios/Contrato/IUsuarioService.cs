using ReporteTributario.Models.Entities;

namespace ReporteTributario.Servicios.Contrato
{
    public interface IUsuarioService
    {
        Task<Usuario> GetUsuario(string correo, string clave);
        Task<bool> GetEmailUsuario(string correo);
        //Task<Usuario> SaveUsuario(Usuario modelo);
        Task<bool> SaveUsuario(Usuario modelo);
        Task<bool> UpdateUsuario(Usuario modelo);
        Task<bool> Confirmar(string token);
    }
}
