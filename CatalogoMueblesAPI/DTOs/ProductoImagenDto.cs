namespace CatalogoMueblesAPI.DTOs;

public class ProductoImagenDto
{
    public int IdProducto { get; set; }
    public string UrlImagen { get; set; } = string.Empty;
    public bool EsPrincipal { get; set; } = false;
}