using System;
using System.Collections.Generic;

namespace gestionServiciosVirtuales.Models
{
    public partial class ServicioEstado
    {
        public ServicioEstado()
        {
            Servicios = new HashSet<Servicio>();
        }

        public int ServicioEstadoId { get; set; }
        public string ServicioEstadoDescripcion { get; set; } = null!;

        public virtual ICollection<Servicio> Servicios { get; set; }
    }
}
