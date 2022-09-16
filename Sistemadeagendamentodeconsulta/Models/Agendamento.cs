using System;
using System.Collections.Generic;

namespace Sistemadeagendamentodeconsulta.Models
{
    public partial class Agendamento
    {
        public decimal Id { get; set; }
        public decimal UsuarioId { get; set; }
        public DateTime Data { get; set; }
        public DateTime? Horario { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}
