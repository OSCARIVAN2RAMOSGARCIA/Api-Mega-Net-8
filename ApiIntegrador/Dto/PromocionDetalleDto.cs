namespace ApiIntegrador.Dto
{
    public class PromocionDetalleDTO
    {
        public string Nombre { get; set; } = string.Empty;
        public decimal DescuentoAplicado { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int DiasDuracion { get; set; }
    }
}
