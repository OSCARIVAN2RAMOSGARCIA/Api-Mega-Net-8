using System.ComponentModel.DataAnnotations;
namespace ApiIntegrador.Dto
{
public class PaqueteDTO
{
    public int IdPaquete { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string TipoPaquete { get; set; } = string.Empty; 
    public string? Descripcion { get; set; }
}

}