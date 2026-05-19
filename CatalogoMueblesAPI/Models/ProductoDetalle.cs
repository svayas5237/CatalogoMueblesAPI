using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatalogoMueblesAPI.Models;

[Table("ProductoDetalle")]
public class ProductoDetalle
{
    [Key]
    public int IdDetalleProducto { get; set; }
    public int IdProducto { get; set; }
    public string? Alto { get; set; }
    public string? Ancho { get; set; }
    public string? Profundidad { get; set; }
    public string? Material { get; set; }
    public string? Color { get; set; }
    public string? Duracion { get; set; }
    public string? Caracteristicas { get; set; }

    [ForeignKey("IdProducto")]
    public Producto? Producto { get; set; }
}