using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatalogoMueblesAPI.Models;

[Table("Categoria")]
public class Categoria
{
    [Key]
    public int IdCategoria { get; set; }
    public string? Nombre { get; set; }
    public string? Descripcion { get; set; }
    public int? IdCategoriaPadre { get; set; }

    [ForeignKey("IdCategoriaPadre")]
    public Categoria? CategoriaPadre { get; set; }

    public ICollection<Categoria> SubCategorias { get; set; } = new List<Categoria>();
    public ICollection<Producto> Productos { get; set; } = new List<Producto>();
}