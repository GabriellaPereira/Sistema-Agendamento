using Microsoft.EntityFrameworkCore;
using Sistemadeagendamentodeconsulta.Models;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Sistemadeagendamentodeconsulta.Repositories
{
    public class AgendamentoRepository
    {
        private readonly ModelContext _context;

        public AgendamentoRepository(ModelContext context)

        {
            _context = context;

        }

        public async Task Inserir(Agendamento agendamento)

        {
            _context.Agendamento.Add(agendamento);
            await _context.SaveChangesAsync();

        }

        public async Task<Agendamento> Consultar(int id)
        {
            Agendamento agendamento = await _context.Agendamento.FindAsync(id);
            return agendamento;
        }

        public async Task <IList<Agendamento>> Listar()
        {
            return await _context.Agendamento.ToListAsync();
        }

        public async Task Excluir(int id)
        {
            Agendamento agendamento = await _context.Agendamento.FindAsync(id);

            if (agendamento != null)
            {
                _context.Agendamento.Remove(agendamento);
                await _context.SaveChangesAsync();
            }
        }

        public async Task Alterar(Agendamento agendamento)
        {
            Agendamento agendamentoExistente = await _context.Agendamento.FindAsync(agendamento.Id);

            if(agendamentoExistente != null)
            {
                agendamentoExistente.Id = agendamento.Id;
                agendamentoExistente.UsuarioId = agendamento.UsuarioId;
                agendamentoExistente.Data = agendamento.Data;
                agendamentoExistente.Horario = agendamento.Horario;

                await _context.SaveChangesAsync();
            }
        }


    }
}
