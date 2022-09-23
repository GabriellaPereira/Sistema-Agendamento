using Microsoft.EntityFrameworkCore;
using Sistemadeagendamentodeconsulta.Models;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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

        public async Task<Agendamento> Inserir(Agendamento agendamento)

        {
            agendamento.Id = await CriarIdAgendamento();
            _context.Agendamento.Add(agendamento);
            await _context.SaveChangesAsync();

            return agendamento;

        }

        private async Task<decimal> CriarIdAgendamento()
        {
            Agendamento ultimoAgendamentoExistente = await _context.Agendamento
                .OrderByDescending(u => u.Id)
                .FirstOrDefaultAsync();

            if (ultimoAgendamentoExistente != null)
            {
                return ultimoAgendamentoExistente.Id + 1;
            }
            return 1;
        }



        public async Task<Agendamento> Consultar(decimal id)
        {
            Agendamento agendamento = await _context.Agendamento.FindAsync(id);
            return agendamento;
        }

        public async Task <List<Agendamento>> Listar()
        {
            return await _context.Agendamento.ToListAsync();
        }

        public async Task Excluir(decimal id)
        {
            Agendamento agendamento = await _context.Agendamento.FindAsync(id);

            if (agendamento != null)
            {
                _context.Agendamento.Remove(agendamento);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Agendamento> Alterar(decimal id, Agendamento agendamento)
        {
            Agendamento agendamentoExistente = await _context.Agendamento.FindAsync(id);

            if (agendamentoExistente != null)
            {
                agendamentoExistente.Data = agendamento.Data;
                agendamentoExistente.Horario = agendamento.Horario;

                await _context.SaveChangesAsync();
                return agendamento;
            }

            return null;
        }


    }
}
