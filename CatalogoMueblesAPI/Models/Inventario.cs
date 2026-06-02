using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatalogoMueblesAPI.Models;

[Table("Inventario")]
public class Inventario
{
    [Key]
    [Column("id_inventario")]
    public int IdInventario { get; set; }

    [Column("id_producto")]
    public int IdProducto { get; set; }

    [Column("stock")]
    public int Stock { get; set; }

    [Column("stock_minimo")]
    public int StockMinimo { get; set; }

    [ForeignKey("IdProducto")]
    public Producto? Producto { get; set; }
}