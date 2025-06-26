using System.ComponentModel.DataAnnotations;
namespace ApiIntegrador.Dto
{
// ContratoDTOs
public class ContratoDto
{
    public int IdContrato { get; set; }
    public int IdSuscriptor { get; set; }
    public string ClienteNombre { get; set; }
    public int IdPaquete { get; set; }
    public string PaqueteNombre { get; set; }
    public DateTime FechaInicio { get; set; }
    public DateTime? FechaTermino { get; set; }
    public bool Activo { get; set; }
    public string TipoContrato { get; set; }
}

}
