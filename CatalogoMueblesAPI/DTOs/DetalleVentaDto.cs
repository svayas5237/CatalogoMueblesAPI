namespace CatalogoMueblesAPI.DTOs
{
    public class DetalleVentaDto
    {
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal { get; set; }
        public int IdDetalle { get; set; }
        public string? Entregado { get; set; }
        public DateTime? FechaEntrega { get; set; }

    }
}
