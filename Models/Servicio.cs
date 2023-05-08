using System;
using System.Collections.Generic;

namespace gestionServiciosVirtuales.Models
{
    public partial class Servicio
    {
        public int ServicioId { get; set; }
        public string ServicioTitulo { get; set; } = null!;
        public string ServicioDescripcion { get; set; } = null!;
        public int? ServicioEstado { get; set; }
        public DateTime? ServicioFechaCreacion { get; set; }
        public string? UsuarioId { get; set; }

        public virtual ServicioEstado? ServicioEstadoNavigation { get; set; }
        public virtual AspNetUser? Usuario { get; set; }
    }
}
