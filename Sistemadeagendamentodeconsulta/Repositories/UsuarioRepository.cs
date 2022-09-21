using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Sistemadeagendamentodeconsulta.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FiapSmartCityWebAPI.Repository
{
    public class UsuarioRepository
    {
        private readonly ModelContext _context;
        private readonly IConfiguration _configuration;


        public UsuarioRepository(ModelContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<Usuario> Inserir(Usuario usuario)
        {
            usuario.Senha = VerificarSenhaCriptografada(usuario.Senha);

            _context.Usuario.Add(usuario);
            await _context.SaveChangesAsync();

            return usuario;
        }

        private string VerificarSenhaCriptografada(string senha)
        {
            var sha = SHA256.Create();
            byte[] byteArray = Encoding.Default.GetBytes(senha);
            byte[] hashedPassword = sha.ComputeHash(byteArray);

            return Convert.ToBase64String(hashedPassword);
        }

        public async Task<Usuario> Consultar(int id)
        {
            Usuario usuario = await _context.Usuario.FindAsync(id);
            return usuario;
        }

        public async Task<Usuario> ConsultarPorNomeESenha(string email, string senha)
        {
            var senhaCriptografada = VerificarSenhaCriptografada(senha);

            Usuario usuario = await _context.Usuario
                .FirstOrDefaultAsync(u => u.Email == email && u.Senha == senhaCriptografada);

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
