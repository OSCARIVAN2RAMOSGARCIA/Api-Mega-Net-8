using System.ComponentModel.DataAnnotations;
namespace ApiIntegrador.Dto
{
public class PaqueteDto
{
    public int IdPaquete { get; set; }
    public string Nombre { get; set; }
    public string TipoPaquete { get; set; }
    public string Descripcion { get; set; }
    public List<ServicioDto> Servicios { get; set; }
}
}