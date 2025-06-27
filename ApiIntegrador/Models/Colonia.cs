using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApiIntegrador.Models
{
public class Colonia
{   [Key] // 👈 CLAVE PRIMARIA
    public int IdColonia { get; set; }
    public int IdCiudad { get; set; }
    public string Nombre { get; set; } = string.Empty;
    
    public Ciudad Ciudad { get; set; } = null!;
    public ICollection<Suscriptor> Suscriptores { get; set; } = new List<Suscriptor>();
}
}