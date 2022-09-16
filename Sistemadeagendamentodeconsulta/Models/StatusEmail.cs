using System;
using System.Collections.Generic;

namespace Sistemadeagendamentodeconsulta.Models
{
    public partial class StatusEmail
    {
        public decimal Id { get; set; }
        public decimal UsuarioId { get; set; }
        public string EmailEnviado { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}
