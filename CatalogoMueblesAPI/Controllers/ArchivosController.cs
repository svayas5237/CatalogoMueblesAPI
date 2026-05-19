using Microsoft.AspNetCore.Mvc;

namespace CatalogoMueblesAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ArchivosController : ControllerBase
{
    private readonly IWebHostEnvironment _env;
    private readonly ILogger<ArchivosController> _logger;

    public ArchivosController(IWebHostEnvironment env, ILogger<ArchivosController> logger)
    {
        _env = env;
        _logger = logger;
    }

    [HttpPost("subir")]
    public async Task<IActionResult> SubirImagen(IFormFile archivo)
    {
        try
        {
            if (archivo == null || archivo.Length == 0)
                return BadRequest(new { mensaje = "No se recibió ningún archivo" });

            var extensionesPermitidas = new[] { ".jpg", ".jpeg", ".png", ".webp" };
            var extension = Path.GetExtension(archivo.FileName).ToLower();

            if (!extensionesPermitidas.Contains(extension))
                return BadRequest(new { mensaje = "Formato no permitido. Use jpg, jpeg, png o webp" });

            // Usar ruta absoluta aunque WebRootPath sea null
            var webRoot = _env.WebRootPath
                ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

            var carpetaImagenes = Path.Combine(webRoot, "imagenes");
            Directory.CreateDirectory(carpetaImagenes);

            var nombreArchivo = $"{Guid.NewGuid()}{extension}";
            var rutaDestino = Path.Combine(carpetaImagenes, nombreArchivo);

            using (var stream = new FileStream(rutaDestino, FileMode.Create))
            {
                await archivo.CopyToAsync(stream);
            }

            var urlImagen = $"/imagenes/{nombreArchivo}";
            return Ok(new { urlImagen, nombreArchivo });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al subir imagen");
            return StatusCode(500, new { mensaje = "Error interno al subir la imagen", detalle = ex.Message });
        }
    }
}