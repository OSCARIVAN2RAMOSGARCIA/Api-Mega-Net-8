using System.ComponentModel.DataAnnotations;
namespace ApiIntegrador.Dto
{

public class ContratoCreateDto
{
    [Required]
    public int IdCliente { get; set; }
    [Required]
    public int IdPaquete { get; set; }
    [Required]
    public DateTime FechaInicio { get; set; }
    public string TipoContrato { get; set; }
}
}