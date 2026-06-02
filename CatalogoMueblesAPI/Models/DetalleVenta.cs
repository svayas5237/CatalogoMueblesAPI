using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatalogoMueblesAPI.Models;

[Table("DetalleVenta")]
public class DetalleVenta
{
    [Key]
    [Column("id_detalle")] // Asegúrate de que este nombre coincida con tu columna en SQL
    public int IdDetalle { get; set; }

    [Column("id_venta")]
    public int IdVenta { get; set; }

    [Column("id_producto")]
    public int IdProducto { get; set; }

    [Column("cantidad")]
    public int Cantidad { get; set; }

    [Column("precio_unitario")]
    public decimal PrecioUnitario { get; set; }

    [Column("subtotal")]
    public decimal Subtotal { get; set; }

    // --- NUEVAS PROPIEDADES PARA EL CONTROL DE ENTREGA ---
    [Column("entregado")]
    public string? Entregado { get; set; } = "No";

    [Column("fecha_entrega")]
    public DateTime? FechaEntrega { get; set; }

    [ForeignKey("IdVenta")]
    public Venta? Venta { get; set; }

    [ForeignKey("IdProducto")]
    public Producto? Producto { get; set; }
}