namespace ApiIntegrador.Dto
{
public class SuscriptorPagoDTO
{
    public string Nombre { get; set; } = string.Empty;
    public string Paquete { get; set; } = string.Empty;
    public string TipoContrato { get; set; } = string.Empty; // ← NUEVA propiedad
    public decimal PrecioOriginal { get; set; }
    public decimal Descuento { get; set; }
    public string DescripcionPromocion { get; set; } = string.Empty;
    public decimal TotalAPagar { get; set; }
    public string PorcentajeAplicado { get; set; } = string.Empty;
    public List<PromocionDetalleDTO> PromocionesDetalles { get; set; } = new();
}

}