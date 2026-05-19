namespace CatalogoMueblesAPI.DTOs;

public class ProductoDetalleImagenDto
{
    public int IdProducto { get; set; }
    public string UrlImagen { get; set; } = string.Empty;
    public bool EsPrincipal { get; set; } = false;
    public int Orden { get; set; } = 0;
}