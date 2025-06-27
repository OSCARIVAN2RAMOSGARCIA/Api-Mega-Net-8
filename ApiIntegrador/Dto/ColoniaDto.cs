using System.ComponentModel.DataAnnotations;
namespace ApiIntegrador.Dto
{
public class ColoniaDTO
{
    public int IdColonia { get; set; }
    public int IdCiudad { get; set; }
    public string Nombre { get; set; } = string.Empty;
}

}