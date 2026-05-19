using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatalogoMueblesAPI.Models;

[Table("Producto")]
public class Producto
{
    [Key]
    public int IdProducto { get; set; }
    public int IdCategoria { get; set; }
    public string? Nombre { get; set; }
    public string? Descripcion { get; set; }
    public decimal Precio { get; set; }
    public bool Activo { get; set; } = true;

    [ForeignKey("IdCategoria")]
    public Categoria? Categoria { get; set; }

    public Inventario? Inventario { get; set; }
    public ICollection<ProductoImagen> Imagenes { get; set; } = new List<ProductoImagen>();
    public ICollection<MovimientoInventario> Movimientos { get; set; } = new List<MovimientoInventario>();
}
