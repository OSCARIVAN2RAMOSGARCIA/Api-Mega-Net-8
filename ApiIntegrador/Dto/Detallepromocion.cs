using System.ComponentModel.DataAnnotations;
namespace ApiIntegrador.Dto
{
public class DetallePromocionDTO
{
    public string NombreSuscriptor { get; set; }
    public string NombrePaquete { get; set; }
    public decimal PrecioOriginal { get; set; }
    public decimal DescuentoAplicado { get; set; }
    public decimal TotalAPagar { get; set; }
    public string PorcentajeAplicado { get; set; }
}
}