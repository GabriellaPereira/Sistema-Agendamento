using Microsoft.EntityFrameworkCore;
using Sistemadeagendamentodeconsulta.Models;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
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


        public async Task Notificacao(decimal usuarioid, string status)
        {
            //cria uma mensagem
            MailMessage mail = new MailMessage();

            if (status == "confirmado")
            {
                mail.Subject = "Agendamento confirmado.";
                mail.Body = "Agendamento de sessão confirmado, para dia 24/09/2022 ás 18:30 Com Dr.Rodrigo Silva.";
            }
            else
            {
                mail.Subject = "Agendamento cancelado.";
                mail.Body = "Agendamento de sessão no dia 24/09/2022 ás 18:30, foi cancelado.";
            }

            //define os endereços
            mail.From = new MailAddress("");//email do remetente
            mail.To.Add("");//email do destinatario

            //define o conteúdo
            mail.Subject = "Agendamanto confirmado";
            mail.Body = "Agendamento de sessão confirmado, para dia 24/09/2022 ás 18:30.";

            
            //envia a mensagem
            SmtpClient smtp = new SmtpClient("smtp-mail.outlook.com",587);
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            //NetworkCredential enviaremail = new NetworkCredential("");//credenciais do usuario do remente, email e senha.
            //smtp.Credentials = enviaremail;

            smtp.Send(mail);
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
