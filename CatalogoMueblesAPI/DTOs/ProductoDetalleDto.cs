namespace CatalogoMueblesAPI.DTOs;

public class ProductoDetalleDto
{
    public int IdProducto { get; set; }
    public string Alto { get; set; } = string.Empty;
    public string Ancho { get; set; } = string.Empty;
    public string Profundidad { get; set; } = string.Empty;
    public string Material { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
    public string Duracion { get; set; } = string.Empty;
    public string Caracteristicas { get; set; } = string.Empty;
}