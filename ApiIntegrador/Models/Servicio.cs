using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApiIntegrador.Models
{
public class Servicio
{   [Key] 
    public int IdServicio { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public decimal PrecioResidencial { get; set; }
    public decimal PrecioEmpresarial { get; set; }
    public string TipoServicio { get; set; } = string.Empty; 
    
    public ICollection<PaqueteServicio> PaqueteServicios { get; set; } = new List<PaqueteServicio>();
}

}
