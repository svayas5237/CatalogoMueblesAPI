using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatalogoMueblesAPI.Models;

[Table("Usuario")]
public class Usuario
{
    [Key]
    [Column("id_usuario")]
    public int IdUsuario { get; set; }

    [Column("nombre")]
    public string? Nombre { get; set; }

    [Column("email")]
    public string? Email { get; set; }

    [Column("password")]
    public string? Password { get; set; }

    [Column("rol")]
    public string? Rol { get; set; } = "vendedor";

    [Column("id_rol")]
    public int? IdRol { get; set; }

    [ForeignKey("IdRol")]
    public Rol? RolNavigation { get; set; }

    public ICollection<Venta> Ventas { get; set; } = new List<Venta>();
    public ICollection<UsuarioPermiso> Permisos { get; set; } = new List<UsuarioPermiso>();
}