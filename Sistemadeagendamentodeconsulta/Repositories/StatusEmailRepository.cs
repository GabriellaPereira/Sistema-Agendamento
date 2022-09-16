using Microsoft.EntityFrameworkCore;
using Sistemadeagendamentodeconsulta.Models;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Sistemadeagendamentodeconsulta.Repositories
{
    public class StatusEmailRepository
    {
        private readonly ModelContext _context;

        public StatusEmailRepository(ModelContext context)
        {
            _context = context;
        }

        public async Task Inserir(StatusEmail statusemail)
        {
            _context.StatusEmail.Add(statusemail);
            await _context.SaveChangesAsync();
        }

        public async Task<StatusEmail> Consultar(int id)
        {
            StatusEmail statusemail = await _context.StatusEmail.FindAsync(id);
            return statusemail;
        }
        public async Task<IList<StatusEmail>> Listar()
        {
            return await _context.StatusEmail.ToListAsync();
        }

        public async Task Excluir(int id)
        {
            StatusEmail statusEmail = await _context.StatusEmail.FindAsync(id);

            if (statusEmail != null)
            {
                _context.StatusEmail.Remove(statusEmail);
                await _context.SaveChangesAsync();
            }


        }
        public async Task Alterar(StatusEmail statusemail)
        {
            StatusEmail statusemailExistente= await _context.StatusEmail.FindAsync(statusemail.Id);

            if(statusemailExistente != null)
            {
                statusemailExistente.Id=statusemail.Id;
                statusemailExistente.UsuarioId = statusemail.UsuarioId;
                statusemailExistente.EmailEnviado = statusemail.EmailEnviado;

                await _context.SaveChangesAsync();

            }
        }

    }


}
