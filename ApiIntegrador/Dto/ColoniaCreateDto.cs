using System.ComponentModel.DataAnnotations;
namespace ApiIntegrador.Dto
{
public class ColoniaCreateDto
{
    [Required]
    public int IdCiudad { get; set; }
    [Required]
    public string Nombre { get; set; }
}
}