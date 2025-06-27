using System.ComponentModel.DataAnnotations;
namespace ApiIntegrador.Dto
{
public class CiudadDTO
{
    public int IdCiudad { get; set; }
    public string Nombre { get; set; } = string.Empty;
}
}