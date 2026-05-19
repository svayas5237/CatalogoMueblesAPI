namespace CatalogoMueblesAPI.DTOs;

public class CarritoDto
{
    public int IdCliente { get; set; }
    public List<CarritoDetalleDto> Detalles { get; set; } = new();
}

public class CarritoDetalleDto
{
    public int IdProducto { get; set; }
    public int Cantidad { get; set; }
    public decimal PrecioUnitario { get; set; }
}