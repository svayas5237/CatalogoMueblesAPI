using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatalogoMueblesAPI.Models;

[Table("UsuarioPermiso")]
public class UsuarioPermiso
{
    [Key]
    [Column("id_permiso")]
    public int IdPermiso { get; set; }

    [Column("id_usuario")]
    public int IdUsuario { get; set; }

    [Column("permiso")]
    public string Permiso { get; set; } = string.Empty;

    [ForeignKey("IdUsuario")]
    public Usuario? Usuario { get; set; }
}