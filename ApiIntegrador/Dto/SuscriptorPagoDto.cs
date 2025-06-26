namespace ApiIntegrador.Dto
{
public class SuscriptorPagoDTO
{
    public string Nombre { get; set; } = "";
    public string Paquete { get; set; } = "";
    public decimal PrecioOriginal { get; set; }
    public decimal Descuento { get; set; }
    public string DescripcionPromocion { get; set; } = "";
    public decimal TotalAPagar { get; set; }
    public string PorcentajeAplicado { get; set; } = "";
    public List<PromocionDetalleDTO> PromocionesDetalles { get; set; } = new();
}
}