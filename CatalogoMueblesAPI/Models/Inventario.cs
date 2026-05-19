using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatalogoMueblesAPI.Models;

[Table("Inventario")]
public class Inventario
{
    [Key]
    public int IdInventario { get; set; }
    public int IdProducto { get; set; }
    public int Stock { get; set; }
    public int StockMinimo { get; set; }

    [ForeignKey("IdProducto")]
    public Producto? Producto { get; set; }
}