using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApiIntegrador.Models
{
public class Contrato
{   [Key]
    public int IdContrato { get; set; }
    public int IdSuscriptor { get; set; }
    public int IdPaquete { get; set; }
    public DateTime FechaInicio { get; set; }
    public DateTime? FechaTermino { get; set; }
    public bool Activo { get; set; } = true;
    public string TipoContrato { get; set; } = string.Empty; // "Residencial", "Empresarial"
    
    public Suscriptor Suscriptor { get; set; } = null!;
    public Paquete Paquete { get; set; } = null!;
    public ICollection<PromocionAplicada> PromocionesAplicadas { get; set; } = new List<PromocionAplicada>();
}

}