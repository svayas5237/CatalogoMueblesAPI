
namespace CatalogoMueblesAPI.DTOs;

public class InventarioDto
{
    public int IdInventario { get; set; }
    public int IdProducto { get; set; }
    public int Stock { get; set; }
    public int StockMinimo { get; set; }
    
}