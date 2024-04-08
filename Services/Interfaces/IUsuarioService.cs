using Microsoft.EntityFrameworkCore;
using SandraConfecciones.Models;

namespace SandraConfecciones.Services.Interfaces
{
    public interface IUsuarioService
    {

        Task<Usuario> GetUser(string email, string password);

        Task<Usuario> SaveUser(Usuario model);

        Task<Usuario> GetUserByEmail(string email);
    }
}
