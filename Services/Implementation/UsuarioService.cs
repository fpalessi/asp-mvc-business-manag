using Microsoft.EntityFrameworkCore;
using SandraConfecciones.Models;
using SandraConfecciones.Services.Interfaces;

namespace SandraConfecciones.Services.Implementation
{
    public class UsuarioService : IUsuarioService
    {
        private readonly SandraContext _context;

        public UsuarioService(SandraContext context)
        {
            _context = context;
        }

        public async Task<Usuario> GetUser(string email, string password)
        {
            Usuario userFound = await _context.Usuarios.Where(u => u.Email == email && u.Password == password)
                .FirstOrDefaultAsync();

            return userFound;
        }

        public async Task<Usuario> SaveUser(Usuario model)
        {
            _context.Usuarios.Add(model);

            await _context.SaveChangesAsync();

            return model;
        }

        public async Task<Usuario> GetUserByEmail(string email)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
