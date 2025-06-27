using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApiIntegrador.Models
{
public class Promocion
{   [Key] // 👈 CLAVE PRIMARIA
    public int IdPromocion { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public decimal DescuentoResidencial { get; set; }
    public decimal DescuentoEmpresarial { get; set; }
    public bool AplicaNuevos { get; set; } = false;
    public string TipoPromocion { get; set; } = string.Empty; // "Residencial", "Empresarial", "Ambos"
    public DateTime VigenciaDesde { get; set; }
    public DateTime VigenciaHasta { get; set; }
    public bool Activa { get; set; } = true;
    
    public ICollection<PromocionConfiguracion> Configuraciones { get; set; } = new List<PromocionConfiguracion>();
    public ICollection<PromocionAplicada> PromocionesAplicadas { get; set; } = new List<PromocionAplicada>();
}
}