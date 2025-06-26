using System.ComponentModel.DataAnnotations;
namespace ApiIntegrador.Dto
{
public class SuscriptorDto
{
    public int IdSuscriptor { get; set; }
    public string Nombre { get; set; }
    public int IdColonia { get; set; }
    public string ColoniaNombre { get; set; }
    public string CiudadNombre { get; set; }
    public DateTime FechaRegistro { get; set; }
}
}