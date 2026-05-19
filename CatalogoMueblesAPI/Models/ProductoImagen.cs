using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatalogoMueblesAPI.Models;

[Table("ProductoImagen")]
public class ProductoImagen
{
    [Key]
    public int IdImagen { get; set; }
    public int IdProducto { get; set; }
    public string? UrlImagen { get; set; }
    public bool EsPrincipal { get; set; } = false;

    [ForeignKey("IdProducto")]
    public Producto? Producto { get; set; }
}