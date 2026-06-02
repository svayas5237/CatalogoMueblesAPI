namespace CatalogoMueblesAPI.DTOs;

public class VentaDto
{
    public int IdCliente { get; set; }
    public int IdUsuario { get; set; }

    public string? FacturacionNombre { get; set; }
    public string? FacturacionApellido { get; set; }
    public string? FacturacionCedula { get; set; }
    public string? FacturacionTelefono { get; set; }
    public string? FacturacionDireccion { get; set; }

    public string? EnvioNombre { get; set; }
    public string? EnvioApellido { get; set; }
    public string? EnvioTelefono { get; set; }
    public string? EnvioDireccion { get; set; }
    public string? EnvioLinkMaps { get; set; }

    public string? UrlComprobante { get; set; }

    public List<DetalleVentaDto> Detalles { get; set; } = new();
}

