using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatalogoMueblesAPI.Models;

[Table("Carrito")]
public class Carrito
{
    [Key]
    [Column("id_carrito")]
    public int IdCarrito { get; set; }

    [Column("id_cliente")]
    public int IdCliente { get; set; }

    [Column("fecha_creacion")]
    public DateTime FechaCreacion { get; set; } = DateTime.Now;

    [Column("activo")]
    public bool Activo { get; set; } = true;

    [ForeignKey("IdCliente")]
    public Cliente? Cliente { get; set; }

    public ICollection<CarritoDetalle> Detalles { get; set; } = new List<CarritoDetalle>();
}