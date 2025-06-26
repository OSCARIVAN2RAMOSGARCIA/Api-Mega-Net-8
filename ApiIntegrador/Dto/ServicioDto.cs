using System.ComponentModel.DataAnnotations;
namespace ApiIntegrador.Dto
{
public class ServicioDto
{
    public int IdServicio { get; set; }
    public string Nombre { get; set; }
    public decimal PrecioResidencial { get; set; }
    public decimal PrecioEmpresarial { get; set; }
    public string TipoServicio { get; set; }
}
}