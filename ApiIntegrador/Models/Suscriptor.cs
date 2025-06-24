namespace ApiIntegrador.Models
{
    public class Suscriptor
    {
        public int IdSuscriptor { get; set; }
        public string Nombre { get; set; }

        public int IdPaquete { get; set; }
        public Paquete Paquete { get; set; }

        public bool EsNuevo { get; set; }

        public int IdColonia { get; set; }
        public Colonia Colonia { get; set; }

        public ICollection<PromocionAplicada> PromocionesAplicadas { get; set; }
    }
}
