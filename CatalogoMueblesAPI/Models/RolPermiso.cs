using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatalogoMueblesAPI.Models;

[Table("RolPermiso")]
public class RolPermiso
{
    [Key]
    [Column("id_rol_permiso")]
    public int IdRolPermiso { get; set; }

    [Column("id_rol")]
    public int IdRol { get; set; }

    [Column("permiso")]
    public string Permiso { get; set; } = string.Empty;

    [ForeignKey("IdRol")]
    public Rol? Rol { get; set; }
}