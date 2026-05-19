using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatalogoMueblesAPI.Models;

[Table("DetalleVenta")]
public class DetalleVenta
{
    [Key]
    public int IdDetalle { get; set; }
    public int IdVenta { get; set; }
    public int IdProducto { get; set; }
    public int Cantidad { get; set; }
    public decimal PrecioUnitario { get; set; }
    public decimal Subtotal { get; set; }

    [ForeignKey("IdVenta")]
    public Venta? Venta { get; set; }

    [ForeignKey("IdProducto")]
    public Producto? Producto { get; set; }
}