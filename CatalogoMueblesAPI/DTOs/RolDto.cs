namespace CatalogoMueblesAPI.DTOs;

public class RolDto
{
    public string Nombre { get; set; } = string.Empty;
    public string? Descripcion { get; set; }
    public List<string> Permisos { get; set; } = new();
}