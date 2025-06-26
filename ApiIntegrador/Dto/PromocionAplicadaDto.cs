using System.ComponentModel.DataAnnotations;
namespace ApiIntegrador.Dto
{
// PromocionAplicadaDTOs
public class PromocionAplicadaDto
{
    public int IdPromocionAplicada { get; set; }
    public int IdContrato { get; set; }
    public int IdPromocion { get; set; }
    public string PromocionNombre { get; set; }
    public DateTime FechaAplicacion { get; set; }
    public DateTime? FechaTermino { get; set; }
    public decimal DescuentoAplicado { get; set; }
}
}