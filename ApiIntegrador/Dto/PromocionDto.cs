using System.ComponentModel.DataAnnotations;
namespace ApiIntegrador.Dto
{
// PromocionDTOs
public class PromocionDTO
{
    public int IdPromocion { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public decimal DescuentoResidencial { get; set; }
    public decimal DescuentoEmpresarial { get; set; }
    public bool AplicaNuevos { get; set; }
    public string TipoPromocion { get; set; } = string.Empty; // "Residencial", "Empresarial", "Ambos"
    public DateTime VigenciaDesde { get; set; }
    public DateTime VigenciaHasta { get; set; }
    public bool Activa { get; set; }
}
}