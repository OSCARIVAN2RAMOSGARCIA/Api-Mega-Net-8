namespace ApiIntegrador.Models
{
    public class Promocion
    {
        public int IdPromocion { get; set; }
        public string Descripcion { get; set; }
        public string Condicion { get; set; }

        public ICollection<PromocionAplicada> Aplicaciones { get; set; }
    }
}