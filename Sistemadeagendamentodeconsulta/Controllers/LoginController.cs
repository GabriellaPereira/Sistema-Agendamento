using FiapSmartCityWebAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Sistemadeagendamentodeconsulta.Models;
using System.Threading.Tasks;

namespace Sistemadeagendamentodeconsulta.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private UsuarioRepository _usuarioRepository;
        private ModelContext _context;
        private IConfiguration _configuration;

        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
            _context = new ModelContext();
            _usuarioRepository = new UsuarioRepository(_context, _configuration);
            
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Autenticar([FromBody] Usuario input)
        {
            Usuario usuario = await _usuarioRepository.ConsultarPorNomeESenha(input.Email, input.Senha);

            if(usuario ==  null)
            {
                return BadRequest();
            }

            var token = Token.GenerateToken(usuario);

            var result = new
            {
                usuario,
                token
            };

            return Ok(result);
        }
    }
}
