namespace ApiIntegrador.Models
{
// Promocion.cs
public class Promocion
{
    public int IdPromocion { get; set; }
    public string Nombre { get; set; }
    public decimal DescuentoResidencial { get; set; }
    public decimal DescuentoEmpresarial { get; set; }
    public bool AplicaNuevos { get; set; } = false;
    public string TipoPromocion { get; set; } // "Residencial", "Empresarial", "Ambos"
    public DateTime VigenciaDesde { get; set; }
    public DateTime VigenciaHasta { get; set; }
    public bool Activa { get; set; } = true;
    public ICollection<PromocionAplicada> PromocionesAplicadas { get; set; }
}
}