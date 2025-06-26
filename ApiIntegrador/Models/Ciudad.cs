namespace ApiIntegrador.Models
{
public class Ciudad
{
    public int IdCiudad { get; set; }
    public string Nombre { get; set; }
    public ICollection<Colonia> Colonias { get; set; }
}


}


