using System.ComponentModel.DataAnnotations;

namespace gestionServiciosVirtuales.Models
{
    public class TipoServicio
    {
        [Key]
        public int idTipoServicio { get; set; }

        public string servicioNombre { get; set; }


       public string?  descripcion { get; set; }
    }
}
