using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatalogoMueblesAPI.Models;

[Table("Venta")]
public class Venta
{
    [Key]
    public int IdVenta { get; set; }
    public int IdCliente { get; set; }
    public int IdUsuario { get; set; }
    public DateTime Fecha { get; set; } = DateTime.Now;
    public decimal Total { get; set; }

    [ForeignKey("IdCliente")]
    public Cliente? Cliente { get; set; }

    [ForeignKey("IdUsuario")]
    public Usuario? Usuario { get; set; }

    public ICollection<DetalleVenta> Detalles { get; set; } = new List<DetalleVenta>();
}