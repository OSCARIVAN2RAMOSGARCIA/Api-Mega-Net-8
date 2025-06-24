namespace ApiIntegrador.Models
{
    public class Servicio
    {
        public int IdServicio { get; set; }
        public string Nombre { get; set; }
        public decimal PrecioMensual { get; set; }
        public decimal PrecioContratacion { get; set; }

        public ICollection<PaqueteServicio> PaqueteServicios { get; set; }
    }
}
