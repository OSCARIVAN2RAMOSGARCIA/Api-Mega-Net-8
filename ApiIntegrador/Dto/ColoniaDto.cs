using System.ComponentModel.DataAnnotations;
namespace ApiIntegrador.Dto
{
public class ColoniaDto
{
    public int IdColonia { get; set; }
    public int IdCiudad { get; set; }
    public string Nombre { get; set; }
    public string CiudadNombre { get; set; }
}
}