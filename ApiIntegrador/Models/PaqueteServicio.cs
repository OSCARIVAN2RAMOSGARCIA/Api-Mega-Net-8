using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApiIntegrador.Models
{
public class PaqueteServicio
{   [Key] // 👈 CLAVE PRIMARIA
    public int IdPaquete { get; set; }
    public int IdServicio { get; set; }
    
    public Paquete Paquete { get; set; } = null!;
    public Servicio Servicio { get; set; } = null!;
}
}
