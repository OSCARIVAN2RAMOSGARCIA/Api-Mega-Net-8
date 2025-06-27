
    namespace ApiIntegrador.Models
{
public class Suscriptor
{
    public int IdSuscriptor { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public int IdColonia { get; set; }
    public DateTime FechaRegistro { get; set; }
    
    public Colonia Colonia { get; set; } = null!;
    public ICollection<Contrato> Contratos { get; set; } = new List<Contrato>();
}


}

