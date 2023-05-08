using System.ComponentModel.DataAnnotations;

namespace gestionServiciosVirtuales.Models
{
    public class TipoUsuario
    {
        [Key]
        public int idTipoUsuario { get; set; }

        public string tipoUsuarioNombre { get; set; }

    }
}
