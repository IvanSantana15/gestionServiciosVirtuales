using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace gestionServiciosVirtuales.Models
{
    public partial class Solicitante
    {
        [Key]
        public int SolicitanteId { get; set; }
        public string Matricula { get; set; } = null!;
        public string Nombre { get; set; } = null!;

        public string tipoUsuario { get; set; }
        public string Facultad { get; set; } = null!;
        public string Carrera { get; set; } = null!;
        public string? UsuarioId { get; set; }

        public virtual AspNetUser? Usuario { get; set; }

       // public virtual TipoUsuario? TipoUsuario { get; set; }
    }
}
