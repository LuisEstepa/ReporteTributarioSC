using Microsoft.EntityFrameworkCore;
using ReporteTributario.Models;
using ReporteTributario.Models.Entities;
using ReporteTributario.Servicios.Contrato;

namespace ReporteTributario.Servicios.Implementacion
{
    public class UsuarioService : IUsuarioService
    {
        private readonly DbTtributarioContext _Context;
        public UsuarioService(DbTtributarioContext dbContext)
        {
            _Context = dbContext;
        }

        public async Task<bool> GetEmailUsuario(string correo)
        {
            Usuario usuario_encontrado = await _Context.Usuarios.Where(u => u.Correo == correo)
                 .FirstOrDefaultAsync();

            if (usuario_encontrado != null)

                return true;
            else
                return false;

        }

        public async Task<Usuario> GetUsuario(string correo, string clave)
        {
            Usuario usuario_encontrado = await _Context.Usuarios.Where(u => u.Correo == correo && u.Clave == clave)
                 .FirstOrDefaultAsync();

            return usuario_encontrado;
        }

        public async Task<bool> SaveUsuario(Usuario modelo)
        {
            _Context.Usuarios.Add(modelo);
            var ressultado = await _Context.SaveChangesAsync();

            if (ressultado == 1)
                return true;
            else
                return false;
        }

        public async Task<bool> UpdateUsuario(Usuario modelo)
        {
            Usuario UsuarioActualizar = await _Context.Usuarios.Where(l => l.Token == modelo.Token).FirstOrDefaultAsync();
            UsuarioActualizar.Clave = modelo.Clave;
            UsuarioActualizar.Restablecer = modelo.Restablecer;
            _Context.Entry(UsuarioActualizar).State = EntityState.Modified;

            if (_Context.SaveChanges() == 1)
                return true;
            else
                return false;
        }

        public async Task<bool> Confirmar(string token)
        {
            Usuario UsuarioActualizar = await _Context.Usuarios.Where(l => l.Token == token).FirstOrDefaultAsync();
            UsuarioActualizar.Confirmado = true;
            _Context.Entry(UsuarioActualizar).State = EntityState.Modified;

            if (_Context.SaveChanges() == 1)
                return true;
            else
                return false;

        }

    }
}