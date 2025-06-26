namespace ApiIntegrador.Models
{
public class Colonia
{
    public int IdColonia { get; set; }
    public int IdCiudad { get; set; }
    public string Nombre { get; set; }
    public Ciudad Ciudad { get; set; }
    public ICollection<Suscriptor> Suscriptores { get; set; }
}

}