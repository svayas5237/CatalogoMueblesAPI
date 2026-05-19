namespace CatalogoMueblesAPI.DTOs;

public class MovimientoInventarioDto
{
    public int IdProducto { get; set; }
    public string Tipo { get; set; } = string.Empty; // COMPRA / VENTA / AJUSTE
    public int Cantidad { get; set; }
}