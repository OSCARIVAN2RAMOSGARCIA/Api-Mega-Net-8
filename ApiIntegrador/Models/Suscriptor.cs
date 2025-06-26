
    namespace ApiIntegrador.Models
{
    public class Suscriptor
{
    public int IdSuscriptor { get; set; }
    public string Nombre { get; set; }
    public int IdColonia { get; set; }
    public DateTime FechaRegistro { get; set; }
    public Colonia Colonia { get; set; }
    public ICollection<Contrato> Contratos { get; set; }
}

}

