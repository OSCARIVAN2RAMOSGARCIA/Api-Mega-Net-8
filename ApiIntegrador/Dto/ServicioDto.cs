using System.ComponentModel.DataAnnotations;
namespace ApiIntegrador.Dto
{
public class ServicioDTO
{
    public int IdServicio { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public decimal PrecioResidencial { get; set; }
    public decimal PrecioEmpresarial { get; set; }
    public string TipoServicio { get; set; } = string.Empty; 
}
}