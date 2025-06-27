using System.ComponentModel.DataAnnotations;
namespace ApiIntegrador.Dto
{
public class SuscriptorDTO
{
    public int IdSuscriptor { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public int IdColonia { get; set; }
    public DateTime FechaRegistro { get; set; }
}
}