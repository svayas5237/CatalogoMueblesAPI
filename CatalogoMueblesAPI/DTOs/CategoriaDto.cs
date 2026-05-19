namespace CatalogoMueblesAPI.DTOs;

public class CategoriaDto
{
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public int? IdCategoriaPadre { get; set; }
}