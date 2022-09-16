using Microsoft.EntityFrameworkCore;
using Sistemadeagendamentodeconsulta.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FiapSmartCityWebAPI.Repository
{
    public class UsuarioRepository
    {
        private readonly ModelContext _context;


        public UsuarioRepository(ModelContext context)
        {
            _context = context;
        }

        public async Task Inserir(Usuario usuario)
        {
            _context.Usuario.Add(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task<Usuario> Consultar(int id)
        {
            Usuario usuario = await _context.Usuario.FindAsync(id);
            return usuario;
        }

        public async Task<IList<Usuario>> Listar()
        {
            return await _context.Usuario.ToListAsync();
        }

        public async Task Excluir(int id)
        {
            Usuario usuario = await _context.Usuario.FindAsync(id);

            if (usuario != null)
            {
                _context.Usuario.Remove(usuario);
                await _context.SaveChangesAsync();
            }
        }

        public async Task Alterar(Usuario usuario)
        {
            Usuario usuarioExistente = await _context.Usuario.FindAsync(usuario.Id);

            if(usuarioExistente != null)
            {
                usuarioExistente.Nome = usuario.Nome;
                usuarioExistente.Cpf = usuario.Cpf;
                usuarioExistente.Email = usuario.Email;
                usuarioExistente.Senha = usuario.Senha;
                usuarioExistente.DataNasc = usuario.DataNasc;
                usuarioExistente.RegistroProfissional = usuario.RegistroProfissional;

                await _context.SaveChangesAsync();
            }
        }
    }
}
