using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatalogoMueblesAPI.Models;

[Table("ProductoDetalleImagen")]
public class ProductoDetalleImagen
{
    [Key]
    public int IdImagenDetalle { get; set; }
    public int IdProducto { get; set; }
    public string? UrlImagen { get; set; }
    public bool EsPrincipal { get; set; } = false;
    public int Orden { get; set; } = 0;

    [ForeignKey("IdProducto")]
    public Producto? Producto { get; set; }
}