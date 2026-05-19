using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatalogoMueblesAPI.Models;

[Table("CarritoDetalle")]
public class CarritoDetalle
{
    [Key]
    [Column("id_detalle")]
    public int IdDetalle { get; set; }

    [Column("id_carrito")]
    public int IdCarrito { get; set; }

    [Column("id_producto")]
    public int IdProducto { get; set; }

    [Column("cantidad")]
    public int Cantidad { get; set; }

    [Column("precio_unitario")]
    public decimal PrecioUnitario { get; set; }

    [ForeignKey("IdCarrito")]
    public Carrito? Carrito { get; set; }

    [ForeignKey("IdProducto")]
    public Producto? Producto { get; set; }
}