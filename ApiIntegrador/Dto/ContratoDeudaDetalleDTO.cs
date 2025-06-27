namespace ApiIntegrador.Dto
{
    public class ContratoDeudaDetalleDTO
    {
        public int IdContrato { get; set; }
        public string Paquete { get; set; } = string.Empty;
        public string TipoContrato { get; set; } = string.Empty;
        public decimal PrecioOriginal { get; set; }
        public decimal DescuentoTotal { get; set; }
        public decimal TotalAPagar { get; set; }
        public List<PromocionDetalleDTO> Promociones { get; set; } = new();
    }
}
