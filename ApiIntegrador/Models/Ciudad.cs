namespace ApiIntegrador.Models
{
public class Ciudad
{
    public int IdCiudad { get; set; }  // <- Esta propiedad es la clave primaria
    public string NombreCiudad { get; set; }

    public ICollection<Colonia> Colonias { get; set; }
}

}


