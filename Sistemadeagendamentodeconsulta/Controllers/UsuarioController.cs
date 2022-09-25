using FiapSmartCityWebAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Sistemadeagendamentodeconsulta.Models;
using Sistemadeagendamentodeconsulta.ViewModel;
using System.Threading.Tasks;

namespace Sistemadeagendamentodeconsulta.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private UsuarioRepository _usuarioRepository;
        private ModelContext _context;
        private IConfiguration _configuration;

        public UsuarioController(IConfiguration configuration)
        {
            _configuration = configuration;
            _context = new ModelContext();
            _usuarioRepository = new UsuarioRepository(_context, _configuration);
            
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Autenticar([FromBody] UsuarioLoginViewModel input)
        {
            Usuario usuario = await _usuarioRepository.ConsultarPorNomeESenha(input.Email, input.Senha);

            if(usuario ==  null)
            {
                return BadRequest();
            }

            var token = Token.GenerateToken(usuario, _configuration);

            usuario.Senha = null;
            var result = new
            {
                usuario,
                token
            };

            return Ok(result);
        }

        [HttpPost]
        [Route("Create")]
        [AllowAnonymous]
        public async Task<IActionResult> CriarUsuario([FromBody] Usuario input)
        {
            Usuario usuarioInput = await _usuarioRepository.Inserir(input);

            Usuario usuarioCriado = await _usuarioRepository.Consultar(usuarioInput.Id);

            if (usuarioCriado != null)
            {
                usuarioCriado.Senha = null;
                return new OkObjectResult(usuarioCriado);
            }
            return BadRequest("Não foi possível criar o usuário");
        }
    }
}
