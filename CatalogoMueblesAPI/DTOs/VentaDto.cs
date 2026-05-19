namespace CatalogoMueblesAPI.DTOs;

public class VentaDto
{
    public int IdCliente { get; set; }
    public int IdUsuario { get; set; }
    public List<DetalleVentaDto> Detalles { get; set; } = new();
}

public class DetalleVentaDto
{
    public int IdProducto { get; set; }
    public int Cantidad { get; set; }
    public decimal PrecioUnitario { get; set; }
    public decimal Subtotal { get; set; }
}