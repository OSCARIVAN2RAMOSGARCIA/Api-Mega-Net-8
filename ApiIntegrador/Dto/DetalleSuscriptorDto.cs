using System.ComponentModel.DataAnnotations;
namespace ApiIntegrador.Dto
{
public class DetalleSuscriptorDTO
{
    public int IdSuscriptor { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Colonia { get; set; } = string.Empty;
    public string Ciudad { get; set; } = string.Empty;
    public string Paquete { get; set; } = string.Empty;
    public List<string> Servicios { get; set; } = new();
    public decimal PrecioOriginal { get; set; }
    public decimal Descuento { get; set; }
    public decimal TotalAPagar { get; set; }
    public List<string> Promociones { get; set; } = new();
}
}