using System;
using System.Collections.Generic;

namespace Sistemadeagendamentodeconsulta.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            Agendamento = new HashSet<Agendamento>();
            StatusEmail = new HashSet<StatusEmail>();
        }

        public decimal Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string DataNasc { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string RegistroProfissional { get; set; }

        public virtual ICollection<Agendamento> Agendamento { get; set; }
        public virtual ICollection<StatusEmail> StatusEmail { get; set; }
    }
}
