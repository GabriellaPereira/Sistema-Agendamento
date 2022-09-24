using FiapSmartCityWebAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Sistemadeagendamentodeconsulta.Models;
using Sistemadeagendamentodeconsulta.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sistemadeagendamentodeconsulta.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AgendamentoController : ControllerBase
    {
        private AgendamentoRepository _agendamentoRepository;
        private StatusEmailRepository _statusEmailRepository;
        private ModelContext _context;

        public AgendamentoController()
        {
            _context = new ModelContext();
            _agendamentoRepository = new AgendamentoRepository(_context);
            _statusEmailRepository = new StatusEmailRepository(_context);

        }

        [HttpPost]
        [Route("criar")]
        [Authorize]
        public async Task<IActionResult> InserirAgendamento([FromBody] Agendamento input)
        {
            Agendamento agendamentoInput = await _agendamentoRepository.Inserir(input);

            Agendamento agendamentoCriado = await _agendamentoRepository.Consultar(agendamentoInput.Id);

            if (agendamentoCriado != null)
            {
                _statusEmailRepository.EnviarEmail(agendamentoCriado.UsuarioId,"confirmado");
                return new OkObjectResult(agendamentoCriado);
            }

            return BadRequest("Não foi possivel criar o agendamento");
        }

        [HttpGet]
        [Route("consultar")]
        [Authorize]
        public async Task<IActionResult> ListarTodosAgendamentos()
        {
            List<Agendamento> listaDeAgendamentos = await _agendamentoRepository.Listar();
            
            return new OkObjectResult(listaDeAgendamentos);
        }

        [HttpGet]
        [Route("consultar/{id}")]
        [Authorize]
        public async Task<IActionResult> ListarAgendamentoPorId(decimal id)
        {
            Agendamento agendamento = await _agendamentoRepository.Consultar(id);

            return new OkObjectResult(agendamento);
        }

        [HttpPut]
        [Route("consultar/{id}")]
        [Authorize]
        public async Task<IActionResult> AlterarAgendamentoPorId(decimal id, [FromBody] Agendamento input)
        {
            Agendamento agendamentoAlterado = await _agendamentoRepository.Alterar(id, input);

            if(agendamentoAlterado != null)
            {
                return new OkObjectResult(agendamentoAlterado);
            }

            return BadRequest("Não foi possivel alterar o agendamento");
        }


        [HttpDelete]
        [Route("excluir/{id}")]
        [Authorize]
        public async Task<IActionResult> Excluir(decimal id)
        {
            await _agendamentoRepository.Excluir(id);

            _statusEmailRepository.EnviarEmail(id, "cancelado");
            return NoContent();
        }
    }
}